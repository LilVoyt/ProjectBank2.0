using FluentValidation;
using HotChocolate.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Features.Customers.Commands;
using ProjectBank.Application.Validators.Accounts;
using ProjectBank.Application.Validators.Customers;
using ProjectBank.Infrastructure.Data;
using ProjectBank.Infrastructure.Entities;
using ProjectBank.Infrastructure.Services.Accounts;
using ProjectBank.Infrastructure.Services.Customers;
using ProjectBank.Presentation.GraphQL.Models;
using ProjectBank.Presentation.GraphQL.Mutations;
using ProjectBank.Presentation.GraphQL.Queries;

namespace ProjectBank.Presentation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IMediator, Mediator>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<AbstractValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<ICustomerValidationService, CustomerValidationService>();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddTransient<IValidator<Account>, AccountValidator>();
            builder.Services.AddScoped<AbstractValidator<Account>, AccountValidator>();
            builder.Services.AddScoped<IAccountValidationService, AccountValidationService>();

            builder.Services.AddTransient<CustomerQuery>();
            builder.Services.AddTransient<CustomerMutation>();



            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services
                .AddGraphQLServer()
                .AddDefaultTransactionScopeHandler()
                .AddMutationConventions()
                .AddQueryType<CustomerQuery>()
                .AddMutationType<CustomerMutation>()
                .AddType<CustomerInputType>();


            builder.Services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAllOrigins");

            app.MapControllers();
            //app.MapGraphQL("/graphql");

            app.Run();
        }
    }
}