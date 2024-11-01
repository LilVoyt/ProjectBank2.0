using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Finance
{
    public interface ICreditManagementService
    {
        Task<Credit> CreateCredit(string CardNumber, decimal Principal, DateTime StartDate, DateTime EndDate, string CreditTypeName, CancellationToken cancellationToken);
        Task<Guid> CreditMonthlyPayment(Guid CreditId, CancellationToken cancellationToken);
    }
}