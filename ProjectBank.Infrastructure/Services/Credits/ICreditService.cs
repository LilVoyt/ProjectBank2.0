using ProjectBank.DataAcces.Entities;

namespace ProjectBank.DataAcces.Services.Credits
{
    public interface ICreditService
    {
        Task<List<Credit>> Get(Guid cardId, CancellationToken cancellationToken);
        Task<List<Credit>> GetByAccount(Guid accountId, CancellationToken cancellationToken);
        Task<CreditType> GetByName(string name);
        Task<Credit> GetById(Guid id);
        Task<CreditType> GetTypeById(Guid id);
        Task<Credit> Post(Credit credit);
        Task<Credit> Update(Credit credit);
    }
}