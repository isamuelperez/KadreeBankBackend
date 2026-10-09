using KadreeBankApplication;
using KadreeBankApplication.Interfaces;
using KadreeBankApplication.Interfaces.Repositories;
using KadreeBankInfrastructure.Persistence;
using KadreeBankInfrastructure.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<KadreeBankDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("KadreeBank")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    try
    {
        var contex = scope.ServiceProvider.GetRequiredService<KadreeBankDbContext>();
        contex.Database.Migrate();

    }
    catch (Exception)
    {

        throw;
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
