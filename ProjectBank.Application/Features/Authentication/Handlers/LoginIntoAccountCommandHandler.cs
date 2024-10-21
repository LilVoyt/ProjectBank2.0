using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
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
        (IAccountService accountService, IValidator<Account> validator, IMapper mapper, IPasswordHasher passwordHasher, 
        IJwtHandler jwt)
        : IRequestHandler<LoginCommand, Account>
    {
        public async Task<Account> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await accountService.GetByLoginAndPassword(request.Login) ?? throw new KeyNotFoundException("Account not found.");
            bool isVerify = passwordHasher.Verify(request.Password, account.Password);

            if (!isVerify)
            {
                throw new ArgumentException("Password is not correct.");
            }

            account.Token = jwt.Handle(account);

            return account;
        }
    }
    public class LoginValidator : AbstractValidator<LoginCommand>
    {

        public LoginValidator()
        {
            RuleFor(a => a.Login)
                .NotEmpty()
                .WithMessage("Name cannot be empty.")
                .MaximumLength(20)
                .WithMessage($"Name is too long!");
        }
    }
}
