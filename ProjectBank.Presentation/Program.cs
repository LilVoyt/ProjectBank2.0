using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.MappingProfiles;
using ProjectBank.BusinessLogic.Validators.Accounts;
using ProjectBank.BusinessLogic.Validators.Customers;
using ProjectBank.BusinessLogic.Validators.Transactions;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;
using ProjectBank.DataAcces.Services.Transactions;
using ProjectBank.Infrastructure.Services.Transactions;
using ProjectBank.Presentation.GraphQL.Models;
using ProjectBank.Presentation.GraphQL.Mutations;
using ProjectBank.Presentation.GraphQL.Queries;
using System.Text;

namespace ProjectBank.Presentation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IMediator, Mediator>();
            builder.Services.AddAutoMapper(typeof(RegistrationProfile));

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<AbstractValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<ICustomerValidationService, CustomerValidationService>();

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddTransient<IValidator<Account>, AccountValidator>();
            builder.Services.AddScoped<AbstractValidator<Account>, AccountValidator>();
            builder.Services.AddScoped<IAccountValidationService, AccountValidationService>();

            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddTransient<IValidator<Transaction>, TransactionValidator>();
            builder.Services.AddScoped<AbstractValidator<Transaction>, TransactionValidator>();
            builder.Services.AddScoped<ITransactionValidationService, TransactionValidationService>();

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



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myHardSecret7asdasdasdasd7777777777")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("AllowAllOrigins");

            app.MapControllers();
            //app.MapGraphQL("/graphql");

            app.Run();
        }
    }
}