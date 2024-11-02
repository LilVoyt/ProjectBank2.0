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
    internal class CreateCreditCommandHandler(ICurrencyService currencyService, ICreditService creditService, 
        ICardService cardService, IMoneyTransferService moneyTransferService, ICreditManagementService creditСreationService) 
        : IRequestHandler<CreateCreditCommand, CreditDto>
    {
        public async Task<CreditDto> Handle(CreateCreditCommand request, CancellationToken cancellationToken)
        {
            Credit credit = await creditСreationService.CreateCredit(request.CardNumber, request.Principal, request.NumberOfMonth, request.CreditTypeName, cancellationToken);
            CreditDto creditDto = new CreditDto() 
            {
                CardNumber = request.CardNumber,
                Principal = credit.Principal,
                AmountToRepay = credit.AmountToRepay,
                AnnualInterestRate = credit.AnnualInterestRate,
                MonthlyPayment = credit.MonthlyPayment,
                StartDate = credit.StartDate,
                EndDate = credit.EndDate,
                CurrencyName = currencyService.GetById(credit.CurrencyId).Result.CurrencyName,
                CreditTypeName = request.CreditTypeName,
            };

            return creditDto;
        }
    }
}
