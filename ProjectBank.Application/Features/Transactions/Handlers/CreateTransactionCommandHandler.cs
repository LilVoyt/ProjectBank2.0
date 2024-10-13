using MediatR;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Customers;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Handlers
{
    public class CreateTransactionCommandHandler(ITransactionService transactionService, ICardService cardService) 
        : IRequestHandler<CreateTransactionCommand, Transaction>
    {
        public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            Card cardReceiver = await cardService.GetByNumber(request.ReceiverNumber);
            cardReceiver.Balance += request.Sum;
            Card cardSender = await cardService.GetByNumber(request.SenderNumber);
            cardSender.Balance -= request.Sum;

            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Sum = request.Sum,
                CardSenderID = cardSender.Id,
                CardReceiverID = cardReceiver.Id,
            };

            await cardService.Update(cardReceiver);
            await cardService.Update(cardSender);

            await transactionService.Post(transaction);
            
            return transaction;
        }
    }
}
