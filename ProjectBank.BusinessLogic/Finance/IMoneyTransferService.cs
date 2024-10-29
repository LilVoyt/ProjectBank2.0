
namespace ProjectBank.BusinessLogic.Finance
{
    public interface IMoneyTransferService
    {
        Task<Guid> CreateTransaction(string SenderNumber, string ReceiverNumber, decimal Sum, CancellationToken cancellationToken);
    }
}