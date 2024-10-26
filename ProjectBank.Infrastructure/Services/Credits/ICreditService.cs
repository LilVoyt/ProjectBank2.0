using ProjectBank.DataAcces.Entities;

namespace ProjectBank.DataAcces.Services.Credits
{
    public interface ICreditService
    {
        Task<List<Credit>> Get(Guid cardId);
        Task<CreditType> GetByName(string name);
        Task<Credit> Post(Credit credit);
    }
}