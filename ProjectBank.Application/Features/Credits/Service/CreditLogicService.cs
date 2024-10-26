using ProjectBank.BusinessLogic.Features.Credits.Commands;
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

namespace ProjectBank.BusinessLogic.Features.Credits.Service
{
    public class CreditLogicService(ICurrencyService currencyService, ICreditService creditService, ICardService cardService) : ICreditLogicService
    {
        public async Task<CreditDto> CreateCredit(CreateCreditCommand creditCommand)
        {
            Credit credit = new Credit()
            {
                Id = Guid.NewGuid(),
                CardId = cardService.GetByNumber(creditCommand.CardNumber).Result.Id,
                Principal = creditCommand.Principal,
                AnnualInterestRate = currencyService.GetByCode(creditCommand.CurrencyCode).Result.AnnualInterestRate
                * creditService.GetByName(creditCommand.CreditTypeName).Result.InterestRateMultiplier,
                StartDate = creditCommand.StartDate,
                EndDate = creditCommand.EndDate,
                CurrencyId = currencyService.GetByCode(creditCommand.CurrencyCode).Result.Id,
                IsPaidOff = false,
                CreditTypeId = creditService.GetByName(creditCommand.CreditTypeName).Result.Id,
            };
            var startDate = credit.StartDate;
            var endDate = credit.EndDate;

            int months = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;
            credit.MonthlyPayment = credit.Principal * credit.AnnualInterestRate / months;

            await creditService.Post(credit);

            CreditDto creditDto = new CreditDto()
            {
                Principal = credit.Principal,
                AnnualInterestRate = credit.AnnualInterestRate,
                StartDate = credit.StartDate,
                EndDate = credit.EndDate,
                CurrencyName = creditCommand.CurrencyCode,
                CreditTypeName = creditCommand.CreditTypeName,
            };

            return creditDto;
        }
    }
}
