using MediatR;
using ProjectBank.Application.Features.Credits.Queries;
using ProjectBank.Application.Models;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Credits.Handlers
{
    public class GetCreditInfoQueryHandler(ICreditService creditService, ICurrencyService currencyService) : IRequestHandler<GetCreditInfoQuerry, PayingInfo>
    {
        public async Task<PayingInfo> Handle(GetCreditInfoQuerry request, CancellationToken cancellationToken)
        {
            var credit = await creditService.GetById(request.CreditId);
            var currency = await currencyService.GetByIdAsync(credit.CurrencyId);

            PayingInfo payingInfo = new PayingInfo()
            {
                MonthlyPayment = credit.MonthlyPayment,
                AmountToRepay = credit.AmountToRepay,
                CurrencyCode = currency.CurrencyCode
            };
            return payingInfo;
        }
    }
}
