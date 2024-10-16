using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Handlers;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;

using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ProjectBank.Tests.Authentication
{
    public class RegisterTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<Customer>> _mockCustomerValidator;
        private readonly Mock<IValidator<Account>> _mockAccountValidator;
        private readonly Mock<IJwtHandler> _mockJwtHandler;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly RegisterCommandHandler _handler;

        public RegisterTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _mockAccountService = new Mock<IAccountService>();
            _mockMapper = new Mock<IMapper>();
            _mockCustomerValidator = new Mock<IValidator<Customer>>();
            _mockAccountValidator = new Mock<IValidator<Account>>();
            _mockJwtHandler = new Mock<IJwtHandler>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();

            _handler = new RegisterCommandHandler(
                _mockMapper.Object,
                _mockAccountValidator.Object,
                _mockAccountService.Object,
                _mockCustomerService.Object,
                _mockCustomerValidator.Object,
                _mockPasswordHasher.Object,
                _mockJwtHandler.Object
                );
        }

        [Fact]
        public async Task Handle_ValidData_ShouldReturnAccount()
        {
            // Arrange
            var command = new RegisterCommand
            {
                Name = "test",
                Login = "test",
                Password = "password",
                FirstName = "test",
                LastName = "test",
                Country = "test",
                PhoneNumber = "+380000000000",
                Email = "test@gmail.com"
            };
            var customer = new Customer { 
                Id = Guid.NewGuid(), 
                FirstName = "Test Customer" 
            };
            var account = new Account { 
                Id = Guid.NewGuid(), 
                Name = "test",
                CustomerID = Guid.NewGuid(),
                Role = DataAcces.Data.UserRole.Admin,
            };
            var validationResult = new ValidationResult(new List<ValidationFailure>());

            _mockMapper.Setup(m => m.Map<Customer>(It.IsAny<RegisterCommand>())).Returns(customer);
            _mockCustomerValidator.Setup(v => v.ValidateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);
            _mockMapper.Setup(m => m.Map<Account>(It.IsAny<RegisterCommand>())).Returns(account);
            _mockJwtHandler.Setup(j => j.Handle(It.IsAny<Account>())).Returns("testtoken");
            _mockPasswordHasher.Setup(p => p.Hash(It.IsAny<string>())).Returns("hashedpassword");
            _mockAccountValidator.Setup(v => v.ValidateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testtoken", result.Token);
            Assert.Equal("hashedpassword", result.Password);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async Task Handle_InvalidCustomer_ShouldThrowValidationException()
        {
            // Arrange
            var command = new RegisterCommand {};
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Test Customer"
            };
            var validationFailures = new List<ValidationFailure> { new ValidationFailure("Name", "Name is required") };

            _mockMapper.Setup(m => m.Map<Customer>(It.IsAny<RegisterCommand>())).Returns(customer);
            _mockCustomerValidator.Setup(v => v.ValidateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult(validationFailures));

            // Act & Assert
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_InvalidAccount_ShouldThrowValidationException()
        {
            // Arrange
            var command = new RegisterCommand
            {
                Name = "test",
                Login = "test",
                Password = "password",
                FirstName = "test",
                LastName = "test",
                Country = "test",
                PhoneNumber = "+380000000000",
                Email = "test@gmail.com"
            };
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Test Customer"
            };
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Name = "test",
                CustomerID = Guid.NewGuid(),
                Role = DataAcces.Data.UserRole.Admin,
            };
            var validationFailures = new List<ValidationFailure> { new ValidationFailure("Username", "Username is required") };

            _mockMapper.Setup(m => m.Map<Customer>(It.IsAny<RegisterCommand>())).Returns(customer);
            _mockCustomerValidator.Setup(v => v.ValidateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _mockMapper.Setup(m => m.Map<Account>(It.IsAny<RegisterCommand>())).Returns(account);
            _mockJwtHandler.Setup(j => j.Handle(It.IsAny<Account>())).Returns("testtoken");
            _mockPasswordHasher.Setup(p => p.Hash(It.IsAny<string>())).Returns("hashedpassword");
            _mockAccountValidator.Setup(v => v.ValidateAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult(validationFailures));

            // Act & Assert
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
