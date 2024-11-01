using MediatR;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Credits;

namespace ProjectBank.BusinessLogic.Features.Transactions.Handlers
{
    public class CreateTransactionCommandHandler(IMoneyTransferService moneyTansferService, DataContext context) 
        : IRequestHandler<CreateTransactionCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var chain = await moneyTansferService.CreateTransaction(request.SenderNumber, request.ReceiverNumber, request.Sum, cancellationToken);

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await chain.ExecuteAll();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return Guid.NewGuid();
        }
    }
}
