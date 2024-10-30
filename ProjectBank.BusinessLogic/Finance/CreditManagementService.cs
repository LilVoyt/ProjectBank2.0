using Azure.Core;
using MediatR;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Transactions;
using ProjectBank.Infrastructure.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Finance
{
    public class CreditManagementService(ICardService cardService, ICurrencyService currencyService, ICreditService creditService, IMoneyTransferService moneyTransferService) : ICreditManagementService
    {
        public async Task<Credit> CreateCredit(string CardNumber, decimal Principal, DateTime StartDate, DateTime EndDate, string CreditTypeName, CancellationToken cancellationToken)
        {
            var Card = await cardService.GetByNumber(CardNumber);
            var currency = Card.CurrencyID;

            var annualInterestRate = currencyService.GetById(currency).Result.AnnualInterestRate * creditService.GetByName(CreditTypeName).Result.InterestRateMultiplier;

            Credit credit = new Credit()
            {
                Id = Guid.NewGuid(),
                CardId = cardService.GetByNumber(CardNumber).Result.Id,
                Principal = Principal,
                AnnualInterestRate = annualInterestRate,
                StartDate = StartDate,
                EndDate = EndDate,
                CurrencyId = currency,
                IsPaidOff = false,
                CreditTypeId = creditService.GetByName(CreditTypeName).Result.Id,
            };

            int months = (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month;

            decimal interestForPeriod = credit.Principal * annualInterestRate * months / 12;

            credit.AmountToRepay = Principal + interestForPeriod;

            credit.MonthlyPayment = credit.AmountToRepay / months;

            await creditService.Post(credit);

            return credit;
        }

        public async Task<Guid> CreditAnnualPayment(Guid CreditId, CancellationToken cancellationToken)
        {
            Credit credit = await creditService.GetById(CreditId);
            Card card = await cardService.GetById(credit.CardId);

            Guid transaction = await moneyTransferService.CreateTransaction(card.NumberCard, "4411385885164046", credit.MonthlyPayment, cancellationToken);

            credit.AmountToRepay -= credit.MonthlyPayment;

            if(credit.AmountToRepay < 0)
            {
                credit.IsPaidOff = true;
            }

            await creditService.Update(credit);

            Card GeneralCard = await cardService.GetByNumber("4411385885164046");

            cardService.Update(GeneralCard);
            cardService.Update(card);

            return transaction;
        }
    }
}
