using ProjectBank.Application.Models;
using ProjectBank.Infrastructure.Entities;
using ProjectBank.Infrastructure.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Mappers
{
    internal class CardMapper
    {
        private readonly CreditCardGenerator _cardGenerator;

        public CardMapper(CreditCardGenerator cardGenerator)
        {
            _cardGenerator = cardGenerator;
        }

        public Card GetCard(CardRequestModel requestModel)
        {
            var card = new Card();
            card.Id = Guid.NewGuid();
            card.NumberCard = _cardGenerator.GenerateCardNumber();
            card.CardName = requestModel.CardName;
            card.Pincode = requestModel.Pincode;
            card.Data = requestModel.Data;
            card.CVV = requestModel.CVV;
            card.Balance = requestModel.Balance;
            card.AccountID = requestModel.AccountID;

            return card;
        }

        public CardRequestModel GetRequestModel(Card card)
        {
            var requestModel = new CardRequestModel();
            requestModel.NumberCard = card.NumberCard;
            requestModel.CardName = card.CardName;
            requestModel.Pincode = card.Pincode;
            requestModel.Data = card.Data;
            requestModel.CVV = card.CVV;
            requestModel.Balance = card.Balance;

            return requestModel;
        }

        public Card PutRequestModelInCard(Card res, CardRequestModel card)
        {
            res.NumberCard = card.NumberCard;
            res.CardName = card.CardName;
            res.Pincode = card.Pincode;
            res.Data = card.Data;
            res.CVV = card.CVV;
            res.Balance = card.Balance;

            return res;
        }

        public List<CardRequestModel> GetRequestModels(List<Card> cards)
        {
            return cards.Select(card => GetRequestModel(card)).ToList();
        }
    }
}
