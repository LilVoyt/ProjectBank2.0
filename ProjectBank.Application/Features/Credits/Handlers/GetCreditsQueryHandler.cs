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
                var currencyName = await currencyService.GetByIdAsync(credit.CurrencyId) ?? throw new KeyNotFoundException();
                result.Add(new CreditDto()
                {
                    Id = credit.Id,
                    CardNumber = cardService.GetById(credit.CardId).Result.NumberCard,
                    Principal = credit.Principal,
                    AnnualInterestRate = credit.AnnualInterestRate,
                    MonthlyPayment = credit.MonthlyPayment,
                    StartDate = credit.StartDate,
                    EndDate = credit.EndDate,
                    CurrencyName = currencyName.CurrencyCode,
                    CreditTypeName = creditType,
                });
            }

            return result;
        }
    }
}
