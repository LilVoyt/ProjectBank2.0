using FluentValidation;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Validators.Accounts
{
    public class AccountValidator : AbstractValidator<GetByIdQuery>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id must not be empty.")
                .Must(id => Guid.TryParse(id.ToString(), out _))
                .WithMessage("Invalid Id format.");
        }
    }
}