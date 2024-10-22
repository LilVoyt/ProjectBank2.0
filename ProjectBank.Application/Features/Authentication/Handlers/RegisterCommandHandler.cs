using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.BusinessLogic.Services;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;


namespace ProjectBank.BusinessLogic.Features.Authentication.Handlers
{
    public class RegisterCommandHandler(IAuthenticationService authenticationService)
        : IRequestHandler<RegisterCommand, Account>
    {
        public async Task<Account> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await authenticationService.RegisterAsync(request);
        }
    }
}
