using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.Infrastructure.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProjectBank.Tests.IntegrationTests
{
    public class TransactionServiceTests
    {
        private readonly IDataContext _context;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);
            _transactionService = new TransactionService(_context);
        }

        [Fact]
        public async Task Post_Should_Add_New_Transaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Sum = 1000,
                CardSenderID = Guid.NewGuid(),
                CardReceiverID = Guid.NewGuid()
            };

            // Act
            var result = await _transactionService.Post(transaction);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(transaction.Id, result.Id);
            Assert.Equal(transaction.Sum, result.Sum);
        }

        [Fact]
        public async Task Update_Should_Modify_Existing_Transaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Sum = 1000,
                CardSenderID = Guid.NewGuid(),
                CardReceiverID = Guid.NewGuid()
            };

            await _transactionService.Post(transaction);

            var updatedTransaction = new Transaction
            {
                Id = transaction.Id,
                Date = transaction.Date,
                Sum = 1500,
                CardSenderID = transaction.CardSenderID,
                CardReceiverID = transaction.CardReceiverID
            };

            // Act
            var result = await _transactionService.Update(transaction.Id, updatedTransaction);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedTransaction.Sum, result.Sum);
        }

        [Fact]
        public async Task Delete_Should_Remove_Transaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Sum = 1000,
                CardSenderID = Guid.NewGuid(),
                CardReceiverID = Guid.NewGuid()
            };

            await _transactionService.Post(transaction);

            // Act
            var result = await _transactionService.Delete(transaction.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(transaction.Id, result.Id);

            var deletedTransaction = await _context.Transaction.FindAsync(transaction.Id);
            Assert.Null(deletedTransaction); // Transaction should be removed from the database
        }
    }
}
