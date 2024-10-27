using MediatR;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
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
    internal class CreateCreditCommandHandler(ICurrencyService currencyService, ICreditService creditService, ICardService cardService) 
        : IRequestHandler<CreateCreditCommand, CreditDto>
    {
        public async Task<CreditDto> Handle(CreateCreditCommand request, CancellationToken cancellationToken)
        {
            Credit credit = new Credit()
            {
                Id = Guid.NewGuid(),
                CardId = cardService.GetByNumber(request.CardNumber).Result.Id,
                Principal = request.Principal,
                AnnualInterestRate = currencyService.GetByCode(request.CurrencyCode).Result.AnnualInterestRate
                * creditService.GetByName(request.CreditTypeName).Result.InterestRateMultiplier,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CurrencyId = currencyService.GetByCode(request.CurrencyCode).Result.Id,
                IsPaidOff = false,
                CreditTypeId = creditService.GetByName(request.CreditTypeName).Result.Id,
            };
            var startDate = credit.StartDate;
            var endDate = credit.EndDate;

            int months = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;
            credit.MonthlyPayment = credit.Principal * credit.AnnualInterestRate / months;

            await creditService.Post(credit);

            CreditDto creditDto = new CreditDto()
            {
                CardNumber = request.CardNumber,
                Principal = credit.Principal,
                AnnualInterestRate = credit.AnnualInterestRate,
                MonthlyPayment = credit.MonthlyPayment,
                StartDate = credit.StartDate,
                EndDate = credit.EndDate,
                CurrencyName = request.CurrencyCode,
                CreditTypeName = request.CreditTypeName,
            };

            return creditDto;
        }
    }
}
