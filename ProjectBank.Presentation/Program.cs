using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Customers.Customers;
using ProjectBank.BusinessLogic.Features.Transactions.Transactions;
using ProjectBank.BusinessLogic.MappingProfiles;
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
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.BusinessLogic.Security.Card;
using ProjectBank.BusinessLogic.Security.CVV;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Handlers;
using ProjectBank.BusinessLogic.Security.Validation;
using ProjectBank.BusinessLogic.Services;
using ProjectBank.BusinessLogic.Features.Authentication.Validator.Login;
using ProjectBank.BusinessLogic.Features.Authentication.Validators;
using ProjectBank.Presentation.ExceptionHandling;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.BusinessLogic.Features.Accounts.Service;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Service;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Features.Transactions.Service;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;
using ProjectBank.BusinessLogic.Features.Transactions.Validator;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.BusinessLogic.Features.Credits.Service;

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
                .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<RegisterCommandHandler>()
                    .AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>)) 
            );

            builder.Services.AddTransient<IPipelineBehavior<LoginCommand, string>, ValidationBehavior<LoginCommand, string>>();
            builder.Services.AddTransient<IPipelineBehavior<RegisterCommand, string>, ValidationBehavior<RegisterCommand, string>>();
            builder.Services.AddTransient<IPipelineBehavior<GetByIdQuery, AccountDto>,  ValidationBehavior<GetByIdQuery, AccountDto>>();
            builder.Services.AddTransient<IPipelineBehavior<CreateTransactionCommand, Guid>, ValidationBehavior<CreateTransactionCommand, Guid>>();


            //Automapper
            builder.Services.AddAutoMapper(typeof(AuthenticationProfile));


            //Services and validators for each entity
            //Card
            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddTransient<ICardLogicService, CardLogicService>();

            //Currency
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();


            //Credit
            builder.Services.AddScoped<ICreditService, CreditService>();
            builder.Services.AddScoped<ICreditLogicService, CreditLogicService>();



            //Customer
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<AbstractValidator<Customer>, CustomerValidator>();
            builder.Services.AddScoped<ICustomerValidationService, CustomerValidationService>();

            //Account
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddTransient<IAccountLogicService, AccountLogicService>();

            //Transaction
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddTransient<IValidator<CreateTransactionCommand>, CreateTransactionValidator>();

            builder.Services.AddTransient<ITransactionLogicService,  TransactionLogicService>();
            builder.Services.AddTransient<IValidator<GetTransactionQuery>, GetTransactionValidator>();


            //Authentication

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IAuthenticationValidationService, AuthenticationValidationService>();

            builder.Services.AddTransient<IValidator<LoginCommand>, LoginValidator>();

            builder.Services.AddTransient<IValidator<RegisterCommand>, RegisterValidator>();

            builder.Services.AddTransient<IValidator<AddCardCommand>, CardValidator>();



            //Secure service
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

            builder.Services.AddSingleton<IJwtHandler, JwtHandler>();

            builder.Services.AddScoped<ICreditCardGenerator, CreditCardGenerator>();

            builder.Services.AddSingleton<ICVVGenerator, CVVGenerator>();

            builder.Services.AddSingleton<ICurrencyHandler, CurrencyHandler>();


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
            builder.Services.AddTransient<JwtHandler>();


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

            app.UseMiddleware<GlobalExceptionHandler>();

            // Exception handling based on environment
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


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