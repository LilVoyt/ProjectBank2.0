using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.BusinessLogic.Validators.Accounts;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Authentication.Handlers
{
    public class LoginCommandHandler
        (IAccountService accountService, IPasswordHasher passwordHasher, IJwtHandler jwtHandler)
        : IRequestHandler<LoginCommand, string>
    {
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await accountService.GetByLoginAsync(request.Login) ?? throw new KeyNotFoundException();

            bool isPasswordValid = passwordHasher.Verify(request.Password, account.Password);
            if (!isPasswordValid)
            {
                throw new KeyNotFoundException();
            }
            string Jwt = jwtHandler.Handle(account);

            return Jwt;
        }
    }
}
