using ProjectBank.BusinessLogic.Models;

namespace ProjectBank.BusinessLogic.Finance
{
    public interface ICreditApproval
    {
        Task<CreditApprovalResult> CreditApprovalCheck(string CardNumber, decimal Principal, int NumberOfMonth, string CreditTypeName, CancellationToken cancellationToken);
    }
}