using MediatR;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Customers;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Handlers
{
    public class CreateTransactionCommandHandler(IMoneyTransferService moneyTansferService) 
        : IRequestHandler<CreateTransactionCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            return await moneyTansferService.CreateTransaction(request.SenderNumber, request.ReceiverNumber, request.Sum, cancellationToken);
        }
    }
}
