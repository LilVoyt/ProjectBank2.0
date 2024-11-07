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
    public class CreditApproval(ICreditService creditService, ICardService cardService, ICurrencyHandler currencyHandler) : ICreditApproval
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
                return new CreditApprovalResult()
                {
                    Status = CreditApprovalStatus.CreditLimitExceeded.ToString(),
                    Reason = $"Credit limit exceeded, max is {customCreditLimit} but you tried {Principal}!"
                };
            }

            //треба тут акаунт щоб ми отримали його кредитну історію

            var credits = await creditService.GetByAccount(card.AccountID, cancellationToken);

            var idOfNonPayedCredits = credits.Where(c => !c.IsPaidOff && c.EndDate < DateTime.Now).Select(c => c.Id);

            if(idOfNonPayedCredits.Any())
            {
                var ids = "";
                foreach (var id in idOfNonPayedCredits)
                {
                    ids += $"{id} ";
                }
                return new CreditApprovalResult()
                {
                    Status = CreditApprovalStatus.NotApproved.ToString(),
                    Reason = $"You have overdue credits. Ids of this credits: {ids}"
                };
            }


            var currentCredits = credits.Where(c => !c.IsPaidOff && c.EndDate > DateTime.Now);

            var creditSum = new decimal();

            foreach (var credit in currentCredits)
            {
                creditSum += credit.AmountToRepay / currency["data"][credit.Currency.CurrencyCode]["value"].ToObject<decimal>() * Currency;
            }

            var creditApproveIndex = (creditSum + Principal) / MonthlyIncome;

            if (creditApproveIndex > 0.3m)
            {
                return new CreditApprovalResult()
                {
                    Status = CreditApprovalStatus.NotApproved.ToString(),
                    Reason = $"You cannot take a credit cause of low monthly income!"
                };
            }


            int age = (int)((DateTime.Now - Birthday).TotalDays / 365.25);

            if (age < 18)
            {
                return new CreditApprovalResult()
                {
                    Status = CreditApprovalStatus.NotApproved.ToString(),
                    Reason = $"You are too young, credit can be possible in 18 years"
                };
            }

            return new CreditApprovalResult()
            {
                Status = CreditApprovalStatus.Approved.ToString(),
                Reason = "200"
            };

        }
    }
}
