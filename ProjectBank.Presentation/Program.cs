using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Customers.Customers;
using ProjectBank.BusinessLogic.Features.Authentication;
using ProjectBank.BusinessLogic.Features.Transactions.Transactions;
using ProjectBank.BusinessLogic.MappingProfiles;
using ProjectBank.BusinessLogic.Security;
using ProjectBank.BusinessLogic.Validators.Accounts;
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
using System.Text.Json.Serialization;
using ProjectBank.Infrastructure.Services.Cards;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.BusinessLogic.Features.Cards.Cards;

namespace ProjectBank.Presentation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Mediator
            builder.Services.AddScoped<IMediator, Mediator>();
            builder.Services.AddTransient<CustomerQuery>();
            builder.Services.AddTransient<CustomerMutation>();

            builder.Services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));


            //Automapper
            builder.Services.AddAutoMapper(typeof(AuthenticationProfile));


            //Services and validators for each entity
            //Card
            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddTransient<IValidator<Card>, CardValidator>();
            builder.Services.AddScoped<AbstractValidator<Card>, CardValidator>();
            builder.Services.AddScoped<ICardValidationService, CardValidationService>();

            //Customer
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<AbstractValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<ICustomerValidationService, CustomerValidationService>();

            //Account
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddTransient<IValidator<Account>, AccountValidator>();
            builder.Services.AddScoped<AbstractValidator<Account>, AccountValidator>();
            builder.Services.AddScoped<IAccountValidationService, AccountValidationService>();

            //Transaction
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddTransient<IValidator<Transaction>, TransactionValidator>();
            builder.Services.AddScoped<AbstractValidator<Transaction>, TransactionValidator>();
            builder.Services.AddScoped<ITransactionValidationService, TransactionValidationService>();


            //Secure service
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();


            //Sql and dbContext
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


            //Cors
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


            //Authentication and jwt
            builder.Services.AddTransient<CreateJwt>();


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


            //Authorization and policy
            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("AdminPolicy", policy => policy.RequireRole(UserRole.Admin.ToString()))
                .AddPolicy("UserPolicy", policy => policy.RequireRole(UserRole.User.ToString()));


            //Building
            var app = builder.Build();


            //Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //Cors
            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

            //Authentication
            app.UseAuthentication();

            //Authorization
            app.UseAuthorization();

            //Use Controllers
            app.MapControllers();

            app.Run();
        }
    }
}