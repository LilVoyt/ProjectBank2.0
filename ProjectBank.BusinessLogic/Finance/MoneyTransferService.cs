using AutoMapper;
using ProjectBank.BusinessLogic.ChainOfResponsibility;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Finance
{
    public class MoneyTransferService(ICurrencyHandler currencyHandler, ICardService cardService,
        ICurrencyService currencyService, IMapper mapper, ITransactionService transactionService)
        : IMoneyTransferService
    {
        public async Task<ActionQueue> CreateTransaction(string senderNumber, string receiverNumber, decimal sum, CancellationToken cancellationToken)
        {
            // Validate transaction amount
            if (sum <= 0)
                throw new ArgumentException("The transaction amount must be greater than zero.");

            // Fetch sender and receiver cards
            Card cardReceiver = await cardService.GetByNumber(receiverNumber) ?? throw new InvalidOperationException("Receiver card not found.");
            Card cardSender = await cardService.GetByNumber(senderNumber) ?? throw new InvalidOperationException("Sender card not found.");

            // Ensure sender and receiver cards are different
            if (cardSender.Id == cardReceiver.Id)
                throw new InvalidOperationException("Sender and receiver cards cannot be the same.");

            // Check sender's balance
            if (cardSender.Balance < sum)
                throw new InvalidOperationException("Insufficient balance in sender's account.");

            // Fetch currency rates
            var currency = currencyHandler.GetFromApi();
            var cardReceiverCurrency = currency["data"][currencyService.GetByIdAsync(cardReceiver.CurrencyID).Result.CurrencyCode]["value"].ToObject<decimal>();
            var cardSenderCurrency = currency["data"][currencyService.GetByIdAsync(cardSender.CurrencyID).Result.CurrencyCode]["value"].ToObject<decimal>();

            // Convert amount based on currency
            decimal convertedAmount = sum * (cardReceiverCurrency / cardSenderCurrency);

            // Update balances
            cardReceiver.Balance += convertedAmount;
            cardSender.Balance -= sum;

            // Create transaction
            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Sum = sum,
                CurrencyId = cardSender.CurrencyID,
                CardSenderID = cardSender.Id,
                CardReceiverID = cardReceiver.Id
            };

            // Create action queue and add update actions
            var actionQueue = new ActionQueue();
            actionQueue.AddAction(async () => await cardService.Update(cardReceiver));
            actionQueue.AddAction(async () => await cardService.Update(cardSender));
            actionQueue.AddAction(async () => await transactionService.Post(transaction));

            return actionQueue;
        }
    }
}
