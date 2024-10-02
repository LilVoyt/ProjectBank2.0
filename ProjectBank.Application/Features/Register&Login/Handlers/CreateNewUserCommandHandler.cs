using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Register_Login.Commands;
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
using System.Web.WebPages;

namespace ProjectBank.BusinessLogic.Features.Register_Login.Handlers
{
    public class CreateNewUserCommandHandler : IRequestHandler<CreateNewUserCommand, Account>
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<Account> _accountValidator;

        private readonly ICustomerService _customerService;
        private readonly IValidator<Customer> _customerValidator;
        
        private readonly IMapper _mapper;

        public CreateNewUserCommandHandler(IMapper mapper, IValidator<Account> accountValidator, IAccountService accountService, ICustomerService customerService, IValidator<Customer> customerValidator)
        {
            _mapper = mapper;
            _accountValidator = accountValidator;
            _accountService = accountService;
            _customerService = customerService;
            _customerValidator = customerValidator;
        }

        public async Task<Account> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {

            var customer = _mapper.Map<Customer>(request);

            var customerValidationResult = await _customerValidator.ValidateAsync(customer, cancellationToken);
            if (!customerValidationResult.IsValid)
            {
                var errorMessages = string.Join("; ", customerValidationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            await _customerService.Post(customer);

            var account = _mapper.Map<Account>(request);
            account.CustomerID = customer.Id;
            account.Token = CreateJwt.Handle(account);

            var accountValidationResult = await _accountValidator.ValidateAsync(account, cancellationToken);
            if (!accountValidationResult.IsValid)
            {
                var errorMessages = string.Join("; ", accountValidationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            await _accountService.Post(account);

            return account;
        }
    }
}
