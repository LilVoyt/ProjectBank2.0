using FluentValidation;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Validator
{
    public class GetTransactionValidator : AbstractValidator<GetTransactionQuery>
    {
        public GetTransactionValidator()
        {
            RuleFor(x => x.Sender)
                .NotEmpty()
                .When(x => x.Sender.HasValue)
                .WithMessage("Sender ID must not be empty.")
                .Must(BeValidGuid).When(x => x.Sender.HasValue)
                .WithMessage("Sender ID must be a valid GUID.");

            RuleFor(x => x.Receiver)
                .NotEmpty()
                .When(x => x.Receiver.HasValue)
                .WithMessage("Receiver ID must not be empty.")
                .Must(BeValidGuid)
                .When(x => x.Receiver.HasValue)
                .WithMessage("Receiver ID must be a valid GUID.");

            RuleFor(x => x.SortItem)
                .Must(value => string.IsNullOrEmpty(value) || IsValidSortItem(value))
                .WithMessage("SortItem is not valid.");

            RuleFor(x => x.SortOrder)
                .Must(value => string.IsNullOrEmpty(value) || IsValidSortOrder(value))
                .WithMessage("SortOrder must be 'asc' or 'desc'.");
        }

        private bool BeValidGuid(Guid? guid)
        {
            return guid.HasValue && guid != Guid.Empty;
        }

        private bool IsValidSortItem(string sortItem)
        {
            var validSortItems = new[] { "Date", "Amount", "Sender", "Receiver" }; 
            return validSortItems.Contains(sortItem, StringComparer.OrdinalIgnoreCase);
        }

        private bool IsValidSortOrder(string sortOrder)
        {
            return sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                   sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase);
        }
    }
}