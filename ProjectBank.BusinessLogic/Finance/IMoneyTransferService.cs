
using ProjectBank.BusinessLogic.ChainOfResponsibility;

namespace ProjectBank.BusinessLogic.Finance
{
    public interface IMoneyTransferService
    {
        Task<ActionQueue> CreateTransaction(string SenderNumber, string ReceiverNumber, decimal Sum, CancellationToken cancellationToken);
    }
}