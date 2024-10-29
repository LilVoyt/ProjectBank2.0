using Azure.Core;
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

namespace ProjectBank.BusinessLogic.Finance
{
    public class CreditСreationService(ICardService cardService, ICurrencyService currencyService, ICreditService creditService) : ICreditСreationService
    {
        public async Task<Credit> CreateCredit(string CardNumber, decimal Principal, DateTime StartDate, DateTime EndDate, string CreditTypeName, CancellationToken cancellationToken)
        {
            var Card = await cardService.GetByNumber(CardNumber);
            var currency = Card.CurrencyID;

            Credit credit = new Credit()
            {
                Id = Guid.NewGuid(),
                CardId = cardService.GetByNumber(CardNumber).Result.Id,
                Principal = Principal,
                AnnualInterestRate = currencyService.GetById(currency).Result.AnnualInterestRate
                * creditService.GetByName(CreditTypeName).Result.InterestRateMultiplier,
                StartDate = StartDate,
                EndDate = EndDate,
                CurrencyId = currency,
                IsPaidOff = false,
                CreditTypeId = creditService.GetByName(CreditTypeName).Result.Id,
            };
            var startDate = credit.StartDate;
            var endDate = credit.EndDate;

            int months = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;
            credit.MonthlyPayment = credit.Principal * credit.AnnualInterestRate / months;

            await creditService.Post(credit);

            return credit;
        }
    }
}
