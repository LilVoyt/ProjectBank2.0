using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectBank.Application.Features.Credits.Commands;
using ProjectBank.Application.Features.Credits.Validator;
using ProjectBank.BusinessLogic.CardManagement;
using ProjectBank.BusinessLogic.ChainOfResponsibility;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Handlers;
using ProjectBank.BusinessLogic.Features.Authentication.Validator.Login;
using ProjectBank.BusinessLogic.Features.Authentication.Validators;
using ProjectBank.BusinessLogic.Features.Cards.Cards;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Customers.Customers;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;
using ProjectBank.BusinessLogic.Features.Transactions.Transactions;
using ProjectBank.BusinessLogic.Features.Transactions.Validator;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.BusinessLogic.MappingProfiles;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.BusinessLogic.Security.Card;
using ProjectBank.BusinessLogic.Security.CVV;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.BusinessLogic.Security.Validation;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Customers;
using ProjectBank.DataAcces.Services.Transactions;
using ProjectBank.Infrastructure.Services.Cards;
using ProjectBank.Infrastructure.Services.Transactions;
using ProjectBank.Presentation.ExceptionHandling;
using ProjectBank.Presentation.GraphQL.Models;
using ProjectBank.Presentation.GraphQL.Mutations;
using ProjectBank.Presentation.GraphQL.Queries;
using System.Text;

namespace ProjectBank.Presentation
{
    public class Program
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
            //builder.Services.AddTransient<IPipelineBehavior<GetByIdQuery, AccountDto>,  ValidationBehavior<GetByIdQuery, AccountDto>>();
            builder.Services.AddTransient<IPipelineBehavior<CreateTransactionCommand, Guid>, ValidationBehavior<CreateTransactionCommand, Guid>>();
            builder.Services.AddTransient<IPipelineBehavior<CreateCreditCommand, CreditApprovalResult>, ValidationBehavior<CreateCreditCommand, CreditApprovalResult>>();
            builder.Services.AddTransient<IPipelineBehavior<CreditMonthlyPaymentCommand, Guid>, ValidationBehavior<CreditMonthlyPaymentCommand, Guid>>();


            //Automapper
            builder.Services.AddAutoMapper(typeof(AuthenticationProfile));

            //Services and validators for each entity
            //Card
            builder.Services.AddScoped<ICardService, CardService>();

            //Currency
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();

            //Credit
            builder.Services.AddScoped<ICreditService, CreditService>();

            //Customer
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            //Account
            builder.Services.AddScoped<IAccountService, AccountService>();

            //Transaction
            builder.Services.AddScoped<ITransactionService, TransactionService>();

            //Authentication
            builder.Services.AddScoped<IAuthenticationValidationService, AuthenticationValidationService>();

            //Validators
            builder.Services.AddTransient<IValidator<LoginCommand>, LoginValidator>();

            builder.Services.AddTransient<IValidator<RegisterCommand>, RegisterValidator>();

            builder.Services.AddTransient<IValidator<AddCardCommand>, CardValidator>();

            builder.Services.AddTransient<IValidator<CreateCreditCommand>, CreateCreditCommandValidator>();

            builder.Services.AddTransient<IValidator<CreditMonthlyPaymentCommand>, CreditMonthlyPaymentCommandValidator>();

            builder.Services.AddTransient<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();

            builder.Services.AddTransient<IValidator<CreateTransactionCommand>, CreateTransactionValidator>();
            builder.Services.AddTransient<IValidator<GetTransactionQuery>, GetTransactionValidator>();



            //Secure service
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

            builder.Services.AddSingleton<IJwtHandler, JwtHandler>();

            builder.Services.AddScoped<ICreditCardGenerator, CreditCardGenerator>();

            builder.Services.AddSingleton<ICVVGenerator, CVVGenerator>();

            builder.Services.AddSingleton<ICurrencyHandler, CurrencyHandler>();


            //Business logic
            builder.Services.AddScoped<IMoneyTransferService, MoneyTransferService>();
            builder.Services.AddScoped<ICreditManagementService, CreditManagementService>();
            builder.Services.AddScoped<ICardManagementService, CardManagementService>();
            builder.Services.AddScoped<ICreditApproval, CreditApproval>();

            builder.Services.AddScoped<ActionQueue>();

            //Sql and dbContext
            builder.Services.AddScoped<IDataContext, DataContext>();

            builder.Services.AddDbContext<IDataContext, DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            //UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
                .AddPolicy("UserPolicy", policy => {
                    policy.RequireRole(UserRole.User.ToString(), UserRole.Admin.ToString());
                    }
                );


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