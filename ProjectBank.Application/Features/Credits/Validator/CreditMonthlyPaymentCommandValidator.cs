using FluentValidation;
using ProjectBank.Application.Features.Credits.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Credits.Validator
{
    public class CreditMonthlyPaymentCommandValidator : AbstractValidator<CreditMonthlyPaymentCommand>
    {
        public CreditMonthlyPaymentCommandValidator()
        {
            RuleFor(x => x.CreditId)
                .NotEmpty().WithMessage("Credit ID is required.");
        }
    }
}
