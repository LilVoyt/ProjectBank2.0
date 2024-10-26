using MediatR;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Service;
using ProjectBank.BusinessLogic.Security.Card;
using ProjectBank.BusinessLogic.Security.CVV;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Handlers
{
    public class AddCardCommandHandler(ICardLogicService cardService)
        : IRequestHandler<AddCardCommand, Card>
    {
        public async Task<Card> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            Card card = await cardService.GenerateCard(request);
            return card;
        }
    }
}
