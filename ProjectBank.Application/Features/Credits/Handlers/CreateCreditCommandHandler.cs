using MediatR;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.Infrastructure.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Credits.Handlers
{
    internal class CreateCreditCommandHandler(ICurrencyService currencyService, ICreditManagementService creditСreationService) 
        : IRequestHandler<CreateCreditCommand, CreditApprovalResult>
    {
        public async Task<CreditApprovalResult> Handle(CreateCreditCommand request, CancellationToken cancellationToken)
        {
            CreditApprovalResult result = await creditСreationService.CreateCredit(request.CardNumber, request.Principal, request.NumberOfMonth, request.Birthday, request.MonthlyIncome, request.CreditTypeName, cancellationToken);

            return result;
        }
    }
}
