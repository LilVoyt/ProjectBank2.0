using MediatR;
using ProjectBank.BusinessLogic.Models;

namespace ProjectBank.BusinessLogic.Features.Cards.Queries
{
    public class GetByAccountIdQuerry : IRequest<List<CardDto>>
    {
        public Guid AccountId { get; set; }
    }
}
