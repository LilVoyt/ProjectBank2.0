using Microsoft.AspNetCore.Mvc;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.DataAcces.Services.Cards
{
    public interface ICardService
    {
        Task<Card> GetByNumber(string cardNumber);
        Task<Card> GetById(Guid id);
        Task<ActionResult<List<Card>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<List<Card>> Get(Guid accountId);
        Task<Card> Post(Card card);
        Task<Card> Update(Guid id, Card requestModel);
        Task<Card> Update(Card card);
        Task<Card> Delete(Guid id);
    }
}
