using Moq;
using Newtonsoft.Json.Linq;
using ProjectBank.BusinessLogic.Features.Currency;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Currencies;
using ProjectBank.DataAcces.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Tests.Finance
{
    public class MoneyTransferServiceTests
    {
        private readonly Mock<ICurrencyHandler> _currencyHandlerMock;
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly Mock<ICurrencyService> _currencyServiceMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly MoneyTransferService _moneyTransferService;

        public MoneyTransferServiceTests()
        {
            _currencyHandlerMock = new Mock<ICurrencyHandler>();
            _cardServiceMock = new Mock<ICardService>();
            _currencyServiceMock = new Mock<ICurrencyService>();
            _transactionServiceMock = new Mock<ITransactionService>();

            _moneyTransferService = new MoneyTransferService(
                _currencyHandlerMock.Object,
                _cardServiceMock.Object,
                _currencyServiceMock.Object,
                null, // Assuming null for mapper if it's not used
                _transactionServiceMock.Object
            );
        }

        [Fact]
        public async Task CreateTransaction_Should_ThrowException_When_InsufficientBalance()
        {
            // Arrange
            var senderCard = new Card { Id = Guid.NewGuid(), NumberCard = "1111222233334444", Balance = 50, CurrencyID = Guid.NewGuid() };
            var receiverCard = new Card { Id = Guid.NewGuid(), NumberCard = "5555666677778888", Balance = 100, CurrencyID = Guid.NewGuid() };
            decimal transferAmount = 100;

            _cardServiceMock.Setup(s => s.GetByNumber("1111222233334444")).ReturnsAsync(senderCard);
            _cardServiceMock.Setup(s => s.GetByNumber("5555666677778888")).ReturnsAsync(receiverCard);
            _currencyHandlerMock.Setup(h => h.GetFromApi()).Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));
            _currencyServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Currency { CurrencyCode = "USD" });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _moneyTransferService.CreateTransaction("1111222233334444", "5555666677778888", transferAmount, CancellationToken.None));
        }

        [Fact]
        public async Task CreateTransaction_Should_ThrowException_When_SameCard()
        {
            // Arrange
            var card = new Card { Id = Guid.NewGuid(), NumberCard = "1111222233334444", Balance = 200, CurrencyID = Guid.NewGuid() };

            _cardServiceMock.Setup(s => s.GetByNumber("1111222233334444")).ReturnsAsync(card);
            _currencyHandlerMock.Setup(h => h.GetFromApi()).Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));
            _currencyServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Currency { CurrencyCode = "USD" });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _moneyTransferService.CreateTransaction("1111222233334444", "1111222233334444", 50, CancellationToken.None));
        }

        [Fact]
        public async Task CreateTransaction_Should_Succeed_When_ValidConditions()
        {
            // Arrange
            var senderCard = new Card { Id = Guid.NewGuid(), NumberCard = "1111222233334444", Balance = 500, CurrencyID = Guid.NewGuid() };
            var receiverCard = new Card { Id = Guid.NewGuid(), NumberCard = "5555666677778888", Balance = 100, CurrencyID = Guid.NewGuid() };
            decimal transferAmount = 100;

            _cardServiceMock.Setup(s => s.GetByNumber("1111222233334444")).ReturnsAsync(senderCard);
            _cardServiceMock.Setup(s => s.GetByNumber("5555666677778888")).ReturnsAsync(receiverCard);
            _currencyHandlerMock.Setup(h => h.GetFromApi()).Returns(JObject.Parse("{ 'data': { 'USD': { 'value': 1.0 } } }"));
            _currencyServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Currency { CurrencyCode = "USD" });

            // Act
            var actionQueue = await _moneyTransferService.CreateTransaction("1111222233334444", "5555666677778888", transferAmount, CancellationToken.None);

            // Assert
            Assert.NotNull(actionQueue);
        }
    }
}
