using AutoMapper;
using Azure.Core;
using MediatR;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Security.Card;
using ProjectBank.BusinessLogic.Security.CVV;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.Infrastructure.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Service
{
    public class CardLogicService(ICardService cardService, IMapper mapper, ICVVGenerator cvvGenerator, ICreditCardGenerator creditCardGenerator, ICurrencyService currencyService) : ICardLogicService
    {
        public async Task<Card> GenerateCard(AddCardCommand request)
        {
            var card = mapper.Map<Card>(request, opt =>
            {
                opt.Items["creditCard"] = creditCardGenerator.GenerateCardNumber();
                opt.Items["expirationDate"] = DateTime.Now.AddYears(2);
                opt.Items["currencyId"] = currencyService.GetByCode(request.CurrencyCode).Result.Id;
            });
            card.CVV = cvvGenerator.GenerateCVV(card.NumberCard, card.ExpirationDate);

            await cardService.Post(card);

            return card;
        }
    }
}
