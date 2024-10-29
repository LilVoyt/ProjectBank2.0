using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Finance
{
    public interface ICreditСreationService
    {
        Task<Credit> CreateCredit(string CardNumber, decimal Principal, DateTime StartDate, DateTime EndDate, string CreditTypeName, CancellationToken cancellationToken);
    }
}