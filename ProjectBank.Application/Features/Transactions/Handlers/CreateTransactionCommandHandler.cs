using MediatR;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.DataAcces.Data;

namespace ProjectBank.BusinessLogic.Features.Transactions.Handlers
{
    public class CreateTransactionCommandHandler(IMoneyTransferService moneyTansferService, IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateTransactionCommand, Guid>
    {
        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var chain = await moneyTansferService.CreateTransaction(request.SenderNumber, request.ReceiverNumber, request.Sum, cancellationToken);

            await unitOfWork.BeginTransactionAsync();
            try
            {
                await chain.ExecuteAll();

                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }

            return Guid.NewGuid();
        }
    }
}
