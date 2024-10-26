using FluentValidation;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Features.Transactions.Transactions
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.SenderNumber)
                .NotEmpty()
                .WithMessage("Sender's card number is required.")
                .Length(16)
                .WithMessage("Sender's card number must be exactly 16 digits.")
                .Matches(@"^\d{16}$")
                .WithMessage("Sender's card number must contain only digits.");

            RuleFor(x => x.ReceiverNumber)
                .NotEmpty()
                .WithMessage("Receiver's card number is required.")
                .Length(16)
                .WithMessage("Receiver's card number must be exactly 16 digits.")
                .Matches(@"^\d{16}$")
                .WithMessage("Receiver's card number must contain only digits.");

            RuleFor(x => x.Sum)
                .GreaterThan(0)
                .WithMessage("Transaction sum must be greater than zero.");
        }
    }
}
