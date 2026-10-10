using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using KadreeBankAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KadreeBankAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ApiResponse
            {
                Status = statusCode,
                Message = exception.Message,
                Data = null
            };

            // Special handling for FluentValidation.ValidationException (avoid adding FluentValidation package here)
            var exType = exception.GetType();
            if (exType.FullName == "FluentValidation.ValidationException")
            {
                statusCode = StatusCodes.Status400BadRequest;
                response.Status = statusCode;
                response.Message = "Validation failed";

                // Try to read Errors property via reflection
                var errorsProp = exType.GetProperty("Errors", BindingFlags.Public | BindingFlags.Instance);
                if (errorsProp != null)
                {
                    if (errorsProp.GetValue(exception) is System.Collections.IEnumerable errorsEnum)
                    {
                        var messages = new List<string>();
                        foreach (var err in errorsEnum)
                        {
                            var msgProp = err.GetType().GetProperty("ErrorMessage");
                            if (msgProp != null)
                            {
                                var val = msgProp.GetValue(err)?.ToString();
                                if (!string.IsNullOrEmpty(val)) messages.Add(val);
                            }
                        }

                        response.Data = messages;
                    }
                }
            }

            _logger.LogError(exception, "Unhandled exception occurred while processing request");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var payload = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(payload);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
