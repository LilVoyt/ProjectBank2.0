using MediatR;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Credits.Handlers
{
    public class GetCreditsQueryHandler(ICreditService creditService, ICardService cardService, ICurrencyService currencyService)
        : IRequestHandler<GetCreditsQuery, List<CreditDto>>
    {
        public async Task<List<CreditDto>> Handle(GetCreditsQuery request, CancellationToken cancellationToken)
        {
            var credits = await creditService.Get(request.cardId, cancellationToken);


            List<CreditDto> result = new List<CreditDto>();

            foreach (var credit in credits)
            {
                var creditType = creditService.GetTypeById(credit.CreditTypeId).Result.Name;
                result.Add(new CreditDto()
                {
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
