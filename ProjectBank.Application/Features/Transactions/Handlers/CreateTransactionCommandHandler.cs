using MediatR;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Features.Transactions.Service;
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
    public class CreateTransactionCommandHandler(ITransactionLogicService transactionLogicService) 
        : IRequestHandler<CreateTransactionCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            return await transactionLogicService.CreateTransaction(request, cancellationToken);
        }
    }
}
