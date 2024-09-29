using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Register_Login.Commands;
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
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IValidator<Account> _validator;
        private readonly IValidator<Customer> _customerValidator;

        public CreateNewUserCommandHandler(IMapper mapper, IValidator<Account> validator, IAccountService accountService, ICustomerService customerService, IValidator<Customer> customerValidator)
        {
            _mapper = mapper;
            _validator = validator;
            _accountService = accountService;
            _customerService = customerService;
            _customerValidator = customerValidator;
        }

        public async Task<Account> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {

            var customer = _mapper.Map<Customer>(request);

            var validationResult = await _customerValidator.ValidateAsync(customer, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _customerService.Post(customer);

            var account = _mapper.Map<Account>(request);
            account.CustomerID = customer.Id;

            await _accountService.Post(account);
            return account;
        }
    }
}
