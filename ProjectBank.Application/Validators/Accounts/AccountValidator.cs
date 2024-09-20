using FluentValidation;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Validators.Accounts
{
    public class AccountValidator : AbstractValidator<Account>
    {
        private readonly IAccountValidationService _validationService;

        public AccountValidator(IAccountValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty.")
                .MaximumLength(20)
                .WithMessage($"Name is too long!")
                .MustAsync(_validationService.IsNameUnique)
                .WithMessage("Name is used before (it must be unique)!");

            RuleFor(a => a.EmployeeID)
                .MustAsync(_validationService.IsEmployeeExistsOrNull).WithMessage("Employee with this id not exist!");

            RuleFor(a => a.CustomerID)
                .MustAsync(_validationService.IsCustomerExists).WithMessage("Customer with this id not exist!")
                .MustAsync(_validationService.IsNotAlreadyRegisteredCustomer).WithMessage("Customer is already registered!");
        }
    }
}