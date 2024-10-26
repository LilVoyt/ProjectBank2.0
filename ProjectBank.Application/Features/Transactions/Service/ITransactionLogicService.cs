using ProjectBank.BusinessLogic.Features.Transactions.Commands;

namespace ProjectBank.BusinessLogic.Features.Transactions.Service
{
    public interface ITransactionLogicService
    {
        Task<Guid> CreateTransaction(CreateTransactionCommand request, CancellationToken cancellationToken);
    }
}