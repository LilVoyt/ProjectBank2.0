using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.Infrastructure.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Tests.IntegrationTests
{
    public class CardServiceTests
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public CardServiceTests()
        {
            // Налаштування in-memory бази даних
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .Options;
        }

        [Fact]
        public async Task GetByNumber_ShouldReturnCard_WhenCardExists()
        {
            // Arrange
            using var context = new DataContext(_dbContextOptions);
            var cardService = new CardService(context);
            var cardNumber = "1234567890123456";
            await context.Card.AddAsync(new Card
            {
                Id = Guid.NewGuid(),
                NumberCard = cardNumber,
                CardName = "Delete Card",
                Pincode = "1111",
                ExpirationDate = DateTime.Now,
                CVV = "1111",
                Balance = 10020,
                CurrencyID = Guid.NewGuid(),
                AccountID = Guid.NewGuid(),
            });
            await context.SaveChangesAsync();

            // Act
            var result = await cardService.GetByNumber(cardNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cardNumber, result.NumberCard);
        }

        //[Fact]
        //public async Task Get_ShouldReturnSortedAndFilteredCards()
        //{
        //    // Arrange
        //    using var context = new DataContext(_dbContextOptions);
        //    var cardService = new CardService(context);
        //    await context.Card.AddRangeAsync(
        //        new Card { NumberCard = "123", CardName = "Alpha Card" },
        //        new Card { NumberCard = "456", CardName = "Beta Card" });
        //    await context.SaveChangesAsync();

        //    // Act
        //    var result = await cardService.Get(search: "alpha", sortItem: "name", sortOrder: "asc");

        //    // Assert
        //    Assert.Single(result);
        //    Assert.Equal("Alpha Card", result[0].CardName);
        //}

        [Fact]
        public async Task Post_ShouldAddCardToDatabase()
        {
            // Arrange
            using var context = new DataContext(_dbContextOptions);
            var cardService = new CardService(context);
            var card = new Card
            {
                Id = Guid.NewGuid(),
                NumberCard = "456",
                CardName = "New Card",
                Pincode = "1111",
                ExpirationDate = DateTime.Now,
                CVV = "1111",
                Balance = 10020,
                CurrencyID = Guid.NewGuid(),
                AccountID = Guid.NewGuid(),
            };
            // Act
            var result = await cardService.Post(card);

            // Assert
            var addedCard = await context.Card.FirstOrDefaultAsync(c => c.NumberCard == "456");
            Assert.NotNull(addedCard);
            Assert.Equal("New Card", addedCard.CardName);
        }

        [Fact]
        public async Task Delete_ShouldRemoveCardFromDatabase()
        {
            // Arrange
            using var context = new DataContext(_dbContextOptions);
            var cardService = new CardService(context);
            var card = new Card 
            { 
                Id = Guid.NewGuid(),
                NumberCard = "456", 
                CardName = "Delete Card",
                Pincode = "1111",
                ExpirationDate = DateTime.Now,
                CVV = "1111",
                Balance = 10020,
                CurrencyID = Guid.NewGuid(),
                AccountID = Guid.NewGuid(),
            };
            await context.Card.AddAsync(card);
            await context.SaveChangesAsync();

            // Act
            await cardService.Delete(card.Id);

            // Assert
            var deletedCard = await context.Card.FindAsync(card.Id);
            Assert.Null(deletedCard);
        }
    }
}
