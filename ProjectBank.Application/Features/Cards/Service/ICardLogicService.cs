using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.DataAcces.Entities;

namespace ProjectBank.BusinessLogic.Features.Cards.Service
{
    public interface ICardLogicService
    {
        Task<Card> GenerateCard(AddCardCommand request);
    }
}