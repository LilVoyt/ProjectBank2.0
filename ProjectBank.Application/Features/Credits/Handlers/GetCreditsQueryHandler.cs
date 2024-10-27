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
        : IRequestHandler<GetCreditsQuery, List<Credit>>
    {
        public async Task<List<Credit>> Handle(GetCreditsQuery request, CancellationToken cancellationToken)
        {
            var credits = await creditService.GetById(Guid.NewGuid());

            //List<CreditDto> result = new List<CreditDto>();



            //foreach (var credit in credits)
            //{
            //    var creditType = await creditService.GetTypeById(credit.Id);
            //    result.Add(new CreditDto()
            //    {
            //        CardNumber = cardService.GetById(credit.Id).Result.NumberCard,
            //        Principal = credit.Principal,
            //        AnnualInterestRate = credit.AnnualInterestRate,
            //        MonthlyPayment = credit.MonthlyPayment,
            //        StartDate = credit.StartDate,
            //        EndDate = credit.EndDate,
            //        CurrencyName = currencyService.GetById(credit.CurrencyId).Result.CurrencyName,
            //        CreditTypeName = creditType.Name,
            //    });
            //}

            return credits;
        }
    }
}
