using FluentValidation;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Credits.Validator
{
    public class CreateCreditCommandValidator : AbstractValidator<CreateCreditCommand>
    {
        public CreateCreditCommandValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .Matches("^[0-9]{16}$").WithMessage("Card number must be exactly 16 digits.");

            RuleFor(x => x.Principal)
                .GreaterThan(0).WithMessage("Principal amount must be greater than zero.");

            RuleFor(x => x.NumberOfMonth)
                .GreaterThan(0).WithMessage("Number of months must be greater than zero.")
                .LessThanOrEqualTo(360).WithMessage("Number of months cannot exceed 360.");

            RuleFor(x => x.Birthday)
                .LessThan(DateTime.Now.AddYears(-18)).WithMessage("Applicant must be at least 18 years old.");

            RuleFor(x => x.MonthlyIncome)
                .GreaterThan(0).WithMessage("Monthly income must be greater than zero.");

            RuleFor(x => x.CreditTypeName)
                .NotEmpty().WithMessage("Credit type name is required.");
        }
    }
}
