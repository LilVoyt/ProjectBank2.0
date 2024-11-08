using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Finance
{
    public interface ICreditManagementService
    {
        Task<CreditApprovalResult> CreateCredit(string CardNumber, decimal Principal, int NumberOfMonth, DateTime Birthday, decimal MonthlyIncome, string CreditTypeName, CancellationToken cancellationToken);
        Task<Guid> CreditMonthlyPayment(Guid CreditId, CancellationToken cancellationToken);
    }
}