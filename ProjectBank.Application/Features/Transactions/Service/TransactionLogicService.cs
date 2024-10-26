using AutoMapper;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Service
{
    public class TransactionLogicService(ITransactionService transactionService, ICardService cardService, IMapper mapper, ICurrencyHandler currencyHandler, ICurrencyService currencyService) : ITransactionLogicService
    {
        public async Task<Guid> CreateTransaction(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var currency = currencyHandler.GetFromApi();

            Card cardReceiver = await cardService.GetByNumber(request.ReceiverNumber);
            Card cardSender = await cardService.GetByNumber(request.SenderNumber);

            var cardReceiverCurrency = currency["data"][currencyService.GetById(cardReceiver.CurrencyID).Result.CurrencyCode]["value"].ToObject<decimal>();
            var cardSenderCurrency = currency["data"][currencyService.GetById(cardSender.CurrencyID).Result.CurrencyCode]["value"].ToObject<decimal>();

            decimal convertedAmount = request.Sum * (cardSenderCurrency / cardReceiverCurrency);

            cardReceiver.Balance += convertedAmount;
            cardSender.Balance -= request.Sum;

            Transaction transaction = mapper.Map<Transaction>(request, opt =>
            {
                opt.Items["CardSenderId"] = cardSender.Id;
                opt.Items["CardReceiverId"] = cardReceiver.Id;
                opt.Items["CurrencyId"] = cardSender.CurrencyID;
            });

            await cardService.Update(cardReceiver);
            await cardService.Update(cardSender);

            await transactionService.Post(transaction);

            return transaction.Id;
        }
    }
}
