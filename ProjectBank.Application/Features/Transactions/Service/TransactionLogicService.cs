using AutoMapper;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Service
{
    public class TransactionLogicService(ITransactionService transactionService, ICardService cardService, IMapper mapper) : ITransactionLogicService
    {
        public async Task<Guid> CreateTransaction(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            Card cardReceiver = await cardService.GetByNumber(request.ReceiverNumber);
            cardReceiver.Balance += request.Sum;
            Card cardSender = await cardService.GetByNumber(request.SenderNumber);
            cardSender.Balance -= request.Sum;

            Transaction transaction = mapper.Map<Transaction>(request, opt =>
            {
                opt.Items["CardSenderID"] = cardSender.Id;
                opt.Items["CardReceiverID"] = cardReceiver.Id;
            });

            await cardService.Update(cardReceiver);
            await cardService.Update(cardSender);

            await transactionService.Post(transaction);

            return transaction.Id;
        }
    }
}
