using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json.Linq;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using Xunit;

namespace ProjectBank.Tests.BusinessLogic.Finance
{
    public class CreditApprovalTests
    {
        private readonly Mock<ICreditService> _creditServiceMock;
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly Mock<ICurrencyHandler> _currencyHandlerMock;
        private readonly Mock<ICurrencyService> _currencyServiceMock;
        private readonly CreditApproval _creditApproval;

        public CreditApprovalTests()
        {
            _creditServiceMock = new Mock<ICreditService>();
            _cardServiceMock = new Mock<ICardService>();
            _currencyHandlerMock = new Mock<ICurrencyHandler>();
            _currencyServiceMock = new Mock<ICurrencyService>();

            _creditApproval = new CreditApproval(
                _creditServiceMock.Object,
                _cardServiceMock.Object,
                _currencyHandlerMock.Object,
                _currencyServiceMock.Object);
        }

        [Fact]
        public async Task CreditApprovalCheck_Should_Return_LimitExceeded_When_Principal_Exceeds_Limit()
        {
            // Arrange
            decimal creditTypeLimit = 10000m;
            decimal principal = 15000m; // Principal greater than limit
            var card = new Card { Currency = new Currency { CurrencyCode = "USD" } };

            _creditServiceMock.Setup(s => s.GetLimitByCurrencyCode(It.IsAny<string>()))
                .ReturnsAsync(creditTypeLimit);
            _cardServiceMock.Setup(s => s.GetByNumber(It.IsAny<string>()))
                .ReturnsAsync(card);
            _currencyHandlerMock.Setup(h => h.GetFromApi())
                .Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));

            // Act
            var result = await _creditApproval.CreditApprovalCheck("1234567890123456", principal, 12, DateTime.Now.AddYears(-25), 3000, "Consumer Loan", CancellationToken.None);

            // Assert
            Assert.Equal(CreditApprovalStatus.CreditLimitExceeded.ToString(), result.Status);
            Assert.Contains("Credit limit exceeded", result.Reason);
        }

        [Fact]
        public async Task CreditApprovalCheck_Should_Return_NotApproved_When_Has_Overdue_Credits()
        {
            // Arrange
            var card = new Card { AccountID = Guid.NewGuid(), Currency = new Currency { CurrencyCode = "USD" } };
            var credits = new List<Credit>
            {
                new Credit { Id = Guid.NewGuid(), IsPaidOff = false, EndDate = DateTime.Now.AddMonths(-1) } // Overdue credit
            };

            _cardServiceMock.Setup(s => s.GetByNumber(It.IsAny<string>())).ReturnsAsync(card);
            _creditServiceMock.Setup(s => s.GetByAccount(card.AccountID, It.IsAny<CancellationToken>())).ReturnsAsync(credits);
            _currencyHandlerMock.Setup(h => h.GetFromApi())
                .Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));

            // Act
            var result = await _creditApproval.CreditApprovalCheck("1234567890123456", 5000, 12, DateTime.Now.AddYears(-25), 3000, "Consumer Loan", CancellationToken.None);

            // Assert
            Assert.Equal(CreditApprovalStatus.NotApproved.ToString(), result.Status);
            Assert.Contains("overdue credits", result.Reason);
        }

        [Fact]
        public async Task CreditApprovalCheck_Should_Return_NotApproved_When_MonthlyIncome_Is_Low()
        {
            // Arrange
            var card = new Card { AccountID = Guid.NewGuid(), Currency = new Currency { CurrencyCode = "USD" } };
            var credits = new List<Credit>
            {
                new Credit { IsPaidOff = false, AmountToRepay = 2000, CurrencyId = Guid.NewGuid() }
            };

            _cardServiceMock.Setup(s => s.GetByNumber(It.IsAny<string>())).ReturnsAsync(card);
            _creditServiceMock.Setup(s => s.GetByAccount(card.AccountID, It.IsAny<CancellationToken>())).ReturnsAsync(credits);
            _currencyHandlerMock.Setup(h => h.GetFromApi())
                .Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));
            _currencyServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Currency { CurrencyCode = "USD" });

            // Act
            var result = await _creditApproval.CreditApprovalCheck("1234567890123456", 3000, 12, DateTime.Now.AddYears(-25), 200, "Consumer Loan", CancellationToken.None);

            // Assert
            Assert.Equal(CreditApprovalStatus.NotApproved.ToString(), result.Status);
            Assert.Contains("low monthly income", result.Status);
        }

        [Fact]
        public async Task CreditApprovalCheck_Should_Return_NotApproved_When_Age_Is_Under_18()
        {
            // Arrange
            decimal principal = 5000m;
            var card = new Card { Currency = new Currency { CurrencyCode = "USD" } };

            _creditServiceMock.Setup(s => s.GetLimitByCurrencyCode(It.IsAny<string>()))
                .ReturnsAsync(10000);
            _cardServiceMock.Setup(s => s.GetByNumber(It.IsAny<string>()))
                .ReturnsAsync(card);
            _currencyHandlerMock.Setup(h => h.GetFromApi())
                .Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));

            // Act
            var result = await _creditApproval.CreditApprovalCheck("1234567890123456", principal, 12, DateTime.Now.AddYears(-17), 3000, "Consumer Loan", CancellationToken.None);

            // Assert
            Assert.Equal(CreditApprovalStatus.NotApproved.ToString(), result.Status);
            Assert.Contains("too young", result.Reason);
        }

        [Fact]
        public async Task CreditApprovalCheck_Should_Return_Approved_When_All_Conditions_Are_Met()
        {
            // Arrange
            decimal principal = 5000m;
            var card = new Card { AccountID = Guid.NewGuid(), Currency = new Currency { CurrencyCode = "USD" } };
            var credits = new List<Credit>();

            _creditServiceMock.Setup(s => s.GetLimitByCurrencyCode(It.IsAny<string>())).ReturnsAsync(10000);
            _cardServiceMock.Setup(s => s.GetByNumber(It.IsAny<string>())).ReturnsAsync(card);
            _creditServiceMock.Setup(s => s.GetByAccount(card.AccountID, It.IsAny<CancellationToken>())).ReturnsAsync(credits);
            _currencyHandlerMock.Setup(h => h.GetFromApi()).Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));

            // Act
            var result = await _creditApproval.CreditApprovalCheck("1234567890123456", principal, 12, DateTime.Now.AddYears(-25), 3000, "Consumer Loan", CancellationToken.None);

            // Assert
            Assert.Equal(CreditApprovalStatus.Approved.ToString(), result.Status);
            Assert.Equal("200", result.Reason);
        }
    }
}
