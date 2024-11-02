using MediatR;
using ProjectBank.Application.Features.Credits.Queries;
using ProjectBank.Application.Models;
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

namespace ProjectBank.Application.Features.Credits.Handlers
{
    public class GetAccountCreditsQueryHandler(ICreditService creditService, ICardService cardService, ICurrencyService currencyService) : IRequestHandler<GetAccountCreditsQuery, List<CreditDto>>
    {
        public async Task<List<CreditDto>> Handle(GetAccountCreditsQuery request, CancellationToken cancellationToken)
        {
            List<Credit> credits = await creditService.GetByAccount(request.AccountId, cancellationToken);

            List<CreditDto> result = new List<CreditDto>();

            foreach (var credit in credits)
            {
                var creditType = creditService.GetTypeById(credit.CreditTypeId).Result.Name;
                result.Add(new CreditDto()
                {
                    Id = credit.Id,
                    CardNumber = cardService.GetById(credit.CardId).Result.NumberCard,
                    Principal = credit.Principal,
                    AnnualInterestRate = credit.AnnualInterestRate,
                    MonthlyPayment = credit.MonthlyPayment,
                    StartDate = credit.StartDate,
                    EndDate = credit.EndDate,
                    CurrencyName = currencyService.GetById(credit.CurrencyId).Result.CurrencyName,
                    CreditTypeName = creditType,
                });
            }

            return result;
        }
    }
}
