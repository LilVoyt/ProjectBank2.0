using AutoMapper;
using Azure.Core;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.BusinessLogic.Security.Card;
using ProjectBank.BusinessLogic.Security.CVV;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.Infrastructure.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.CardManagement
{
    public class CardManagementService(ICreditCardGenerator creditCardGenerator, ICVVGenerator cvvGenerator, 
        ICurrencyService currencyService, ICardService cardService, IMapper mapper, ICardService cardjService) : ICardManagementService
    {
        public async Task<Guid> CreateCardAsync(string Pincode, string CardName, string CurrencyCode, Guid AccountID)
        {
            var cardNumber = creditCardGenerator.GenerateCardNumber();
            var expirationDate = DateTime.Now.AddYears(5);
            var cvv = cvvGenerator.GenerateCVV(cardNumber, expirationDate);
            var currency = await currencyService.GetByCode(CurrencyCode);
            var currencyId = currency.Id;
            Card card = new Card()
            {
                Id = Guid.NewGuid(),
                NumberCard = cardNumber,
                CardName = CardName,
                Pincode = Pincode,
                ExpirationDate = expirationDate,
                CVV = cvv,
                Balance = 0,
                CurrencyID = currencyId,
                AccountID = AccountID
            };

            await cardService.Post(card);

            return card.Id;
        }

        public async Task<List<CardDto>> GetCardInfo(Guid AccountId)
        {
            List<Card> cards = await cardService.Get(AccountId);
            List<CardDto> cardsDto = cards.Select(card =>
            mapper.Map<CardDto>(card, opt =>
            {
                opt.Items["currencyCode"] =  currencyService.GetById(card.CurrencyID).Result.CurrencyCode;
            })).ToList();
            return cardsDto;
        }
    }
}
