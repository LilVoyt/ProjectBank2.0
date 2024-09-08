using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Features.Customers.Commands;
using ProjectBank.Application.Validators.Customers;
using ProjectBank.Infrastructure.Data;
using ProjectBank.Infrastructure.Entities;
using ProjectBank.Infrastructure.Services.Customers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddTransient<IValidator<Customer>, CustomerValidator>();
builder.Services.AddScoped<AbstractValidator<Customer>, CustomerValidator>();
builder.Services.AddScoped<ICustomerValidationService, CustomerValidationService>();




builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
