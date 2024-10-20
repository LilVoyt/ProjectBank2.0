using MediatR;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
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
    public class AddCardCommandHandler(ICardService cardService, ICreditCardGenerator creditCardGenerator, ICVVGenerator cvvGenerator)
        : IRequestHandler<AddCardCommand, Card>
    {
        public Task<Card> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            Card card = new Card()
            {
                Id = Guid.NewGuid(),
                NumberCard = creditCardGenerator.GenerateCardNumber(),
                CardName = request.CardName,
                Pincode = request.Pincode,
                ExpirationDate = DateTime.UtcNow.AddYears(2),
                Balance = 0,
                AccountID = request.AccountID,
            };
            card.CVV = cvvGenerator.GenerateCVV(card.NumberCard, card.ExpirationDate);

            return cardService.Post(card);
        }
    }
}
