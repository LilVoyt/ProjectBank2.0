using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Services.Cards
{
    public class CardService(IDataContext context) : ICardService
    {
        public async Task<Card> GetByNumber(string cardNumber)
        {
            Card card = await context.Card.SingleOrDefaultAsync(card => card.NumberCard == cardNumber) 
                ?? throw new ArgumentNullException();
            return card;
        }

        public async Task<Card> GetById(Guid id)
        {
            Card card = await context.Card.SingleOrDefaultAsync(card => card.Id == id)
                ?? throw new Exception();
            return card;
        }

        public async Task<ActionResult<List<Card>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Card> cards = context.Card;

            if (!string.IsNullOrEmpty(search))
            {
                cards = cards.Where(c => c.CardName.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Card, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => card => card.CardName,
                _ => card => card.NumberCard,
            };

            cards = sortOrder?.ToLower() == "desc"
                ? cards.OrderByDescending(selectorKey)
                : cards.OrderBy(selectorKey);

            List<Card> accountList = await cards.ToListAsync();


            return accountList;
        }

        public async Task<List<Card>> Get(Guid accountId)
        {
            var cards = context.Card.Where(card => card.AccountID == accountId).ToListAsync();
            return await cards;
        }

        public async Task<Card> Post(Card card)
        {

            await context.Card.AddAsync(card);
            await context.SaveChangesAsync();

            return card;
        }

        public async Task<Card> Update(Guid id, Card requestModel)
        {
            var card = await context.Card.FindAsync(id);
            if (card == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            context.Card.Update(card);
            await context.SaveChangesAsync();
            return card;
        }


        public async Task<Card> Update(Card card)
        {
            context.Card.Update(card);
            await context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> Delete(Guid id)
        {
            var card = await context.Card.FindAsync(id);
            if (card == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            card.AccountID = Guid.Empty;

            context.Card.Remove(card);
            await context.SaveChangesAsync();

            return card;
        }
    }
}
