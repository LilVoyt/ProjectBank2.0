using Moq;
using ProjectBank.BusinessLogic.ChainOfResponsibility;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Tests.Finance
{
    public class CreditManagementServiceTests
    {
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly Mock<ICurrencyService> _currencyServiceMock;
        private readonly Mock<ICreditService> _creditServiceMock;
        private readonly Mock<IMoneyTransferService> _moneyTransferServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICreditApproval> _creditApprovalMock;
        private readonly CreditManagementService _creditManagementService;

        public CreditManagementServiceTests()
        {
            _cardServiceMock = new Mock<ICardService>();
            _currencyServiceMock = new Mock<ICurrencyService>();
            _creditServiceMock = new Mock<ICreditService>();
            _moneyTransferServiceMock = new Mock<IMoneyTransferService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _creditApprovalMock = new Mock<ICreditApproval>();

            _creditManagementService = new CreditManagementService(
                _cardServiceMock.Object,
                _currencyServiceMock.Object,
                _creditServiceMock.Object,
                _moneyTransferServiceMock.Object,
                _unitOfWorkMock.Object,
                _creditApprovalMock.Object);
        }


        [Fact]
        public async Task CreditMonthlyPayment_Should_ProcessPayment_When_CreditExists()
        {
            // Arrange
            var credit = new Credit { Id = Guid.NewGuid(), MonthlyPayment = 500, AmountToRepay = 1000, CardId = Guid.NewGuid() };
            var card = new Card { NumberCard = "1234567890123456" };

            _creditServiceMock.Setup(s => s.GetById(It.IsAny<Guid>())).ReturnsAsync(credit);
            _cardServiceMock.Setup(s => s.GetById(credit.CardId)).ReturnsAsync(card);
            _moneyTransferServiceMock.Setup(m => m.CreateTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ActionQueue());

            // Act
            var result = await _creditManagementService.CreditMonthlyPayment(credit.Id, CancellationToken.None);

            // Assert
            Assert.Equal(credit.Id, result);
            _creditServiceMock.Verify(s => s.Update(It.IsAny<Credit>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }


        [Fact]
        public async Task CreditMonthlyPayment_Should_MarkAsPaidOff_When_AmountToRepay_IsZeroOrLess()
        {
            // Arrange
            var credit = new Credit { Id = Guid.NewGuid(), MonthlyPayment = 500, AmountToRepay = 400, CardId = Guid.NewGuid(), IsPaidOff = false };
            var card = new Card { NumberCard = "1234567890123456" };

            _creditServiceMock.Setup(s => s.GetById(It.IsAny<Guid>())).ReturnsAsync(credit);
            _cardServiceMock.Setup(s => s.GetById(credit.CardId)).ReturnsAsync(card);
            _moneyTransferServiceMock.Setup(m => m.CreateTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ActionQueue());

            // Act
            var result = await _creditManagementService.CreditMonthlyPayment(credit.Id, CancellationToken.None);

            // Assert
            Assert.Equal(credit.Id, result);
            Assert.True(credit.IsPaidOff);
            _creditServiceMock.Verify(s => s.Update(It.Is<Credit>(c => c.IsPaidOff)), Times.Once);
            _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }



    }
}
