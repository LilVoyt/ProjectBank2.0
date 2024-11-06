using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;

namespace ProjectBank.BusinessLogic.Finance
{
    public class CreditManagementService(ICardService cardService, ICurrencyService currencyService, 
        ICreditService creditService, IMoneyTransferService moneyTransferService, IUnitOfWork unitOfWork, ICreditApproval creditApproval) : ICreditManagementService
    {
        public async Task<Credit> CreateCredit(string CardNumber, decimal Principal, int NumberOfMonth, string CreditTypeName, CancellationToken cancellationToken)
        {
            var Card = await cardService.GetByNumber(CardNumber);
            var currency = Card.CurrencyID;

            var annualInterestRate = currencyService.GetByIdAsync(currency).Result.AnnualInterestRate * creditService.GetByName(CreditTypeName).Result.InterestRateMultiplier;

            Credit credit = new Credit()
            {
                Id = Guid.NewGuid(),
                CardId = cardService.GetByNumber(CardNumber).Result.Id,
                Principal = Principal,
                AnnualInterestRate = annualInterestRate,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(NumberOfMonth),
                CurrencyId = currency,
                IsPaidOff = false,
                CreditTypeId = creditService.GetByName(CreditTypeName).Result.Id,
            };

            decimal interestForPeriod = credit.Principal * annualInterestRate * NumberOfMonth / 12;

            credit.AmountToRepay = Principal + interestForPeriod;

            credit.MonthlyPayment = credit.AmountToRepay / NumberOfMonth;

            var res = await creditApproval.CreditApprovalCheck(CardNumber, Principal, NumberOfMonth, CreditTypeName, cancellationToken);

            var chain = await moneyTransferService.CreateTransaction("4411542399821780", Card.NumberCard, credit.Principal, cancellationToken);

            await unitOfWork.BeginTransactionAsync();
            try
            {
                await chain.ExecuteAll();
                await creditService.Post(credit);
                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }

            return credit;
        }

        public async Task<Guid> CreditMonthlyPayment(Guid CreditId, CancellationToken cancellationToken)
        {
            Credit credit = await creditService.GetById(CreditId);
            Card card = await cardService.GetById(credit.CardId);

            var chain = await moneyTransferService.CreateTransaction(card.NumberCard, "4411542399821780", credit.MonthlyPayment, cancellationToken);

            credit.AmountToRepay -= credit.MonthlyPayment;

            if(credit.AmountToRepay < 0)
            {
                credit.IsPaidOff = true;
            }

            await unitOfWork.BeginTransactionAsync();
            try
            {
                await chain.ExecuteAll();
                await creditService.Update(credit);

                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }


            return credit.Id;
        }
    }
}
