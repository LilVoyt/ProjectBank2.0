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
        public async Task<CreditApprovalResult> CreditApprovalCheck(string CardNumber, decimal Principal, int NumberOfMonth, string CreditTypeName, CancellationToken cancellationToken)
        {
            var creditTypeLimit = await creditService.GetLimitByCurrencyCode(CreditTypeName);
            var card = await cardService.GetByNumber(CardNumber);

            var currency = currencyHandler.GetFromApi();
            var customCreditLimit = currency["data"][card.Currency.CurrencyCode]["value"].ToObject<decimal>();
            var res = customCreditLimit * creditTypeLimit;

            if (Principal > customCreditLimit)
            {
                return new CreditApprovalResult()
                {
                    Status = CreditApprovalStatus.CreditLimitExceeded.ToString(),
                    Reason = $"Credit limit exceeded, max is {res} but you tried {Principal}!"
                };
            }
            
            //треба тут акаунт щоб ми отримали його кредитну історію

            return new CreditApprovalResult();

        }
    }
}
