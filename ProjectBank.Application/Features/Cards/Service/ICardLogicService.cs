using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Features.Cards.Service
{
    public interface ICardLogicService
    {
        Task<Card> GenerateCard(AddCardCommand request);
        Task<List<CardDto>> GetCardDtos(GetByAccountIdQuerry request);
    }
}