using FluentValidation;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Validators.Transactions
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        private readonly ITransactionValidationService _validationService;

        public TransactionValidator(ITransactionValidationService validationService)
        {
            _validationService = validationService;
        }
    }
}
