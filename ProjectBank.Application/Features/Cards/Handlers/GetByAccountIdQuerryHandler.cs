using AutoMapper;
using MediatR;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Handlers
{
    public class GetByAccountIdQuerryHandler(ICardService cardService, IMapper mapper) : IRequestHandler<GetByAccountIdQuerry, List<CardDto>>
    {
        public async Task<List<CardDto>> Handle(GetByAccountIdQuerry request, CancellationToken cancellationToken)
        {
            List<Card> cards = await cardService.Get(request.AccountId);
            List<CardDto> cardsDto = cards.Select(card => mapper.Map<CardDto>(card)).ToList();
            return cardsDto;
        }
    }
}
