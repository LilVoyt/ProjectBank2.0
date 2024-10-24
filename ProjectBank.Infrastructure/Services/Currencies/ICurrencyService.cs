using ProjectBank.DataAcces.Entities;

namespace ProjectBank.DataAcces.Services.Currencies
{
    public interface ICurrencyService
    {
        Task<Currency?> GetByCode(string code);
        Task<Currency?> GetById(Guid Id);
    }
}