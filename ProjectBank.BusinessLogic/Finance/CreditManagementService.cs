using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;

namespace ProjectBank.BusinessLogic.Finance
{
    public class CreditManagementService(ICardService cardService, ICurrencyService currencyService, 
        ICreditService creditService, IMoneyTransferService moneyTransferService, DataContext context) : ICreditManagementService
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

            var chain = await moneyTransferService.CreateTransaction("4411385885164046", Card.NumberCard, credit.Principal, cancellationToken);

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await chain.ExecuteAll();
                await creditService.Post(credit);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return credit;
        }

        public async Task<Guid> CreditMonthlyPayment(Guid CreditId, CancellationToken cancellationToken)
        {
            Credit credit = await creditService.GetById(CreditId);
            Card card = await cardService.GetById(credit.CardId);

            var chain = await moneyTransferService.CreateTransaction(card.NumberCard, "4411385885164046", credit.MonthlyPayment, cancellationToken);

            credit.AmountToRepay -= credit.MonthlyPayment;

            if(credit.AmountToRepay < 0)
            {
                credit.IsPaidOff = true;
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await chain.ExecuteAll();
                await creditService.Update(credit);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }


            return credit.Id;
        }
    }
}
