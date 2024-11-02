using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Finance
{
    public interface ICreditManagementService
    {
        Task<Credit> CreateCredit(string CardNumber, decimal Principal, int NumberOfMonth, string CreditTypeName, CancellationToken cancellationToken);
        Task<Guid> CreditMonthlyPayment(Guid CreditId, CancellationToken cancellationToken);
    }
}