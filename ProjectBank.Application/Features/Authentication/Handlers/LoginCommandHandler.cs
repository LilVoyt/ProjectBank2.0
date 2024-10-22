using AutoMapper;
using MediatR;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.BusinessLogic.Services;
using ProjectBank.BusinessLogic.Validators.Accounts;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Authentication.Handlers
{
    public class LoginCommandHandler
        (IAuthenticationService authenticationService)
        : IRequestHandler<LoginCommand, Account>
    {
        public async Task<Account> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Account account = await authenticationService.AuthenticateAsync(request.Login, request.Password);

            return account;
        }
    }
}
