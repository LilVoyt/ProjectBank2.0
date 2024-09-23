using Microsoft.AspNetCore.Mvc;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.DataAcces.Services.Cards
{
    public interface ICardService
    {
        Task<ActionResult<List<Card>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Card> Post(Card card);
        Task<Card> Update(Guid id, Card requestModel);
        Task<Card> Delete(Guid id);
    }
}
