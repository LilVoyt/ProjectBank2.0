using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Authentication.Handlers;
using ProjectBank.BusinessLogic.Security.Jwt;
using ProjectBank.BusinessLogic.Security.Password;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Customers;

public class RegisterCommandHandlerTests
{
    private readonly Mock<ICustomerService> _customerServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IJwtHandler> _jwtHandlerMock;
    private readonly Mock<IAccountService> _accountServiceMock;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        _customerServiceMock = new Mock<ICustomerService>();
        _mapperMock = new Mock<IMapper>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _jwtHandlerMock = new Mock<IJwtHandler>();
        _accountServiceMock = new Mock<IAccountService>();

        _handler = new RegisterCommandHandler(
            _customerServiceMock.Object,
            _mapperMock.Object,
            _passwordHasherMock.Object,
            _jwtHandlerMock.Object,
            _accountServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnJwtToken_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var command = new RegisterCommand
        {
            Name = "Test User",
            Login = "testuser",
            Password = "securePassword",
            FirstName = "Test",
            LastName = "User",
            Country = "Testland",
            PhoneNumber = "+123456789",
            Email = "test@example.com",
            Role = UserRole.User
        };

        var customer = new Customer { Id = Guid.NewGuid() };
        var account = new Account { Id = Guid.NewGuid(), Login = command.Login, CustomerID = customer.Id };
        var jwtToken = "mockJwtToken";

        _mapperMock.Setup(m => m.Map<Customer>(command)).Returns(customer);
        _customerServiceMock.Setup(s => s.Post(It.IsAny<Customer>())).ReturnsAsync(new Customer());
        _passwordHasherMock.Setup(h => h.Hash(command.Password)).Returns("hashedPassword");
        _mapperMock.Setup(m => m.Map<Account>(command, It.IsAny<Action<IMappingOperationOptions>>()))
                   .Returns(account);
        _jwtHandlerMock.Setup(j => j.Handle(It.IsAny<Account>())).Returns(jwtToken);
        _accountServiceMock.Setup(s => s.Post(It.IsAny<Account>())).ReturnsAsync(new Account());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(jwtToken, result);
    }
}
