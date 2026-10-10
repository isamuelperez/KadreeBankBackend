using KadreeBankAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KadreeBankAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult<ApiResponse> Success(object data = null, string message = "OK", int statusCode = 200)
        {
            var response = new ApiResponse { Status = statusCode, Message = message, Data = data };
            return StatusCode(statusCode, response);
        }

        protected ActionResult<ApiResponse> CreatedResponse(string location, object data = null, string message = "Created")
        {
            var response = new ApiResponse { Status = 201, Message = message, Data = data };
            return Created(location, response);
        }

        protected ActionResult<ApiResponse> CreatedAtActionResponse(string actionName, object routeValues, object data = null, string message = "Created")
        {
            var response = new ApiResponse { Status = 201, Message = message, Data = data };
            return CreatedAtAction(actionName, routeValues, response);
        }

        protected ActionResult<ApiResponse> Error(string message = "Error", int statusCode = 500, object data = null)
        {
            var response = new ApiResponse { Status = statusCode, Message = message, Data = data };
            return StatusCode(statusCode, response);
        }

        protected ActionResult<ApiResponse> NotFoundResponse(string message = "Not Found", object data = null)
        {
            var response = new ApiResponse { Status = 404, Message = message, Data = data };
            return NotFound(response);
        }
    }
}
