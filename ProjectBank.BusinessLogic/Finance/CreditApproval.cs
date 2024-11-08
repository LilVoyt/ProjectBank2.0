using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Finance
{
    public class CreditApproval(ICreditService creditService, ICardService cardService, ICurrencyHandler currencyHandler, ICurrencyService currencyService) : ICreditApproval
    {
        public async Task<CreditApprovalResult> CreditApprovalCheck(string CardNumber, decimal Principal, int NumberOfMonth, DateTime Birthday, decimal MonthlyIncome, string CreditTypeName, CancellationToken cancellationToken)
        {
            var creditTypeLimit = await creditService.GetLimitByCurrencyCode(CreditTypeName);
            var card = await cardService.GetByNumber(CardNumber);

            var currency = currencyHandler.GetFromApi();
            var Currency = currency["data"][card.Currency.CurrencyCode]["value"].ToObject<decimal>();
            var customCreditLimit = Currency * creditTypeLimit;

            if (Principal > customCreditLimit)
            {
                return new CreditApprovalResult(CreditApprovalStatus.CreditLimitExceeded, $"Credit limit exceeded, max is {customCreditLimit} but you tried {Principal}!");
            }


            var credits = await creditService.GetByAccount(card.AccountID, cancellationToken);

            var idOfNonPayedCredits = credits.Where(c => !c.IsPaidOff && c.EndDate < DateTime.Now).Select(c => c.Id);

            if(idOfNonPayedCredits.Any())
            {
                var ids = "";
                foreach (var id in idOfNonPayedCredits)
                {
                    ids += $"{id} ";
                }
                return new CreditApprovalResult(CreditApprovalStatus.NotApproved, $"You have overdue credits. Ids of this credits: {ids}");
            }


            var currentCredits = credits.Where(c => !c.IsPaidOff && c.EndDate > DateTime.Now);

            var creditSum = new decimal();

            foreach (var credit in currentCredits)
            {
                var type = await currencyService.GetByIdAsync(credit.CurrencyId);
                creditSum += credit.AmountToRepay / currency["data"][type.CurrencyCode]["value"].ToObject<decimal>() * Currency;
            }

            var creditApproveIndex = (creditSum + Principal) / MonthlyIncome;

            if (creditApproveIndex > 0.3m)
            {
                return new CreditApprovalResult(CreditApprovalStatus.NotApproved, $"You cannot take a credit cause of low monthly income!");
            }


            int age = (int)((DateTime.Now - Birthday).TotalDays / 365.25);

            if (age < 18)
            {
                return new CreditApprovalResult(CreditApprovalStatus.NotApproved, $"You are too young, credit can be possible in 18 years");
            }

            return new CreditApprovalResult(CreditApprovalStatus.Approved, "200");

        }
    }
}
