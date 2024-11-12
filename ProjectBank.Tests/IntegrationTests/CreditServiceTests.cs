using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Credits;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProjectBank.Tests.IntegrationTests
{
    public class CreditServiceTests
    {
        private readonly IDataContext _context;
        private readonly CreditService _creditService;

        public CreditServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_CreditService")
                .Options;

            _context = new DataContext(options);
            _creditService = new CreditService(_context);
        }

        [Fact]
        public async Task Get_Should_Return_Credits_For_CardId()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var credit1 = new Credit { Id = Guid.NewGuid(), CardId = cardId, Principal = 1000 };
            var credit2 = new Credit { Id = Guid.NewGuid(), CardId = cardId, Principal = 2000 };

            await _creditService.Post(credit1);
            await _creditService.Post(credit2);

            // Act
            var result = await _creditService.Get(cardId, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Principal == 1000);
            Assert.Contains(result, c => c.Principal == 2000);
        }

        [Fact]
        public async Task Post_Should_Add_New_Credit()
        {
            // Arrange
            var credit = new Credit { Id = Guid.NewGuid(), Principal = 5000, CardId = Guid.NewGuid() };

            // Act
            var result = await _creditService.Post(credit);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(credit.Id, result.Id);

            var addedCredit = await _context.Credit.FindAsync(credit.Id);
            Assert.NotNull(addedCredit);
        }

        [Fact]
        public async Task Update_Should_Modify_Existing_Credit()
        {
            // Arrange
            var credit = new Credit { Id = Guid.NewGuid(), Principal = 5000, CardId = Guid.NewGuid() };
            await _creditService.Post(credit);

            credit.Principal = 7000;

            // Act
            var result = await _creditService.Update(credit);

            // Assert
            Assert.Equal(7000, result.Principal);

            var updatedCredit = await _context.Credit.FindAsync(credit.Id);
            Assert.Equal(7000, updatedCredit.Principal);
        }
    }
}
