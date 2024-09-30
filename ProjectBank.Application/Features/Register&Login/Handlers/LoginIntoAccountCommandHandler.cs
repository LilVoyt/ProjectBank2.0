using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.BusinessLogic.Features.Register_Login.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Register_Login.Handlers
{
    public class LoginIntoAccountCommandHandler : IRequestHandler<LoginIntoAccountCommand, Account>
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<Account> _validator;
        private readonly IMapper _mapper;
        public LoginIntoAccountCommandHandler(IAccountService accountService, IValidator<Account> validator, IMapper mapper)
        {
            _accountService = accountService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Account> Handle(LoginIntoAccountCommand request, CancellationToken cancellationToken)
        {
            var account = _accountService.GetByLoginAndPassword(request.Login, request.Password);
            account.Token = CreateJwt.Handle(account);
            if (account == null)
            {
                throw new KeyNotFoundException();
            }
            return account;
        }
    }
}
