using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ProjectBank.BusinessLogic.Features.Accounts.Handlers
{
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<Account> _validator;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IAccountService accountService, IValidator<Account> validator, IMapper mapper)
        {
            _accountService = accountService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = _mapper.Map<Account>(request);

            var validationResult = await _validator.ValidateAsync(account, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _accountService.Post(account);
            return account;

        }
    }
}
