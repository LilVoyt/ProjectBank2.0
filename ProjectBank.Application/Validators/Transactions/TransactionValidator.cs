using FluentValidation;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Validators.Transactions
{
    internal class TransactionValidator : AbstractValidator<Transaction>
    {
        private readonly ITransactionValidationService _validationService;

        public TransactionValidator(ITransactionValidationService validationService)
        {
            _validationService = validationService;
        }
    }
}
