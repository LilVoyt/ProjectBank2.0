using FluentValidation;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Cards
{
    public class CardValidator : AbstractValidator<AddCardCommand>
    {
        public CardValidator()
        {
            RuleFor(x => x.Pincode)
            .NotEmpty()
            .WithMessage("Pincode is required.")
            .Length(4, 6)
            .WithMessage("Pincode must be between 4 and 6 characters.")
            .Matches(@"^\d+$")
            .WithMessage("Pincode must be numeric.");

            
            RuleFor(x => x.CardName)
                .NotEmpty()
                .WithMessage("CardName is required.")
                .Length(3, 20)
                .WithMessage("CardName must be between 3 and 20 characters.");

            
            RuleFor(x => x.AccountID)
                .NotEmpty()
                .WithMessage("AccountID is required.")
                .Must(id => Guid.TryParse(id.ToString(), out _))
                .WithMessage("Invalid AccountID format.");
        }
    }
}
