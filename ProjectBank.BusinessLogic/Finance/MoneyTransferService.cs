using AutoMapper;
using ProjectBank.BusinessLogic.ChainOfResponsibility;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Finance
{
    public class MoneyTransferService(ICurrencyHandler currencyHandler, ICardService cardService, 
        ICurrencyService currencyService, IMapper mapper, ITransactionService transactionService)
        : IMoneyTransferService
    {
        public async Task<ActionQueue> CreateTransaction(string SenderNumber, string ReceiverNumber, decimal Sum, CancellationToken cancellationToken)
        {
            var currency = currencyHandler.GetFromApi();

            Card cardReceiver = await cardService.GetByNumber(ReceiverNumber);
            Card cardSender = await cardService.GetByNumber(SenderNumber);

            var cardReceiverCurrency = currency["data"][currencyService.GetByIdAsync(cardReceiver.CurrencyID).Result.CurrencyCode]["value"].ToObject<decimal>();
            var cardSenderCurrency = currency["data"][currencyService.GetByIdAsync(cardSender.CurrencyID).Result.CurrencyCode]["value"].ToObject<decimal>();

            decimal convertedAmount = Sum * (cardReceiverCurrency / cardSenderCurrency);

            cardReceiver.Balance = convertedAmount;
            cardSender.Balance -= Sum;

            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Sum = Sum,
                CurrencyId = cardSender.CurrencyID,
                CardSenderID = cardSender.Id,
                CardReceiverID = cardReceiver.Id
            };

            var actionQueue = new ActionQueue();

            actionQueue.AddAction(async () => await cardService.Update(cardReceiver));
            actionQueue.AddAction(async () => await cardService.Update(cardSender));

            actionQueue.AddAction(async () => await transactionService.Post(transaction));

            return actionQueue;
        }
    }
}
