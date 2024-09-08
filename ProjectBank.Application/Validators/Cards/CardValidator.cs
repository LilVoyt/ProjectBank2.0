using FluentValidation;
using ProjectBank.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Validators.Cards
{
    internal class CardValidator : AbstractValidator<Card>
    {
        private readonly ICardValidationService _validationService;

        public CardValidator(ICardValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(a => a.CardName)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");
        }
    }
}
