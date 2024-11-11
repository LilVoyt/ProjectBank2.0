using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Handlers;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using Xunit;

namespace ProjectBank.Tests.BusinessLogic.Features.Authentication.Handlers
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IJwtHandler> _jwtHandlerMock;
        private readonly LoginCommandHandler _loginCommandHandler;

        public LoginCommandHandlerTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _jwtHandlerMock = new Mock<IJwtHandler>();

            _loginCommandHandler = new LoginCommandHandler(
                _accountServiceMock.Object,
                _passwordHasherMock.Object,
                _jwtHandlerMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnJwt_When_LoginIsSuccessful()
        {
            // Arrange
            var loginCommand = new LoginCommand { Login = "testUser", Password = "password123" };
            var account = new Account { Login = "testUser", Password = "hashedPassword" };
            var expectedJwt = "testJwtToken";

            _accountServiceMock.Setup(s => s.GetByLoginAsync(loginCommand.Login))
                .ReturnsAsync(account);

            _passwordHasherMock.Setup(h => h.Verify(loginCommand.Password, account.Password))
                .Returns(true);

            _jwtHandlerMock.Setup(h => h.Handle(account))
                .Returns(expectedJwt);

            // Act
            var result = await _loginCommandHandler.Handle(loginCommand, CancellationToken.None);

            // Assert
            Assert.Equal(expectedJwt, result);
        }

        [Fact]
        public async Task Handle_Should_ThrowKeyNotFoundException_When_AccountNotFound()
        {
            // Arrange
            var loginCommand = new LoginCommand { Login = "nonExistentUser", Password = "password123" };

            _accountServiceMock.Setup(s => s.GetByLoginAsync(loginCommand.Login))
                .ReturnsAsync((Account)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _loginCommandHandler.Handle(loginCommand, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_ThrowKeyNotFoundException_When_PasswordIsInvalid()
        {
            // Arrange
            var loginCommand = new LoginCommand { Login = "testUser", Password = "invalidPassword" };
            var account = new Account { Login = "testUser", Password = "hashedPassword" };

            _accountServiceMock.Setup(s => s.GetByLoginAsync(loginCommand.Login))
                .ReturnsAsync(account);

            _passwordHasherMock.Setup(h => h.Verify(loginCommand.Password, account.Password))
                .Returns(false);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _loginCommandHandler.Handle(loginCommand, CancellationToken.None));
        }
    }
}
