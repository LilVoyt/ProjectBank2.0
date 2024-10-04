using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProjectBank.BusinessLogic.Features.Cards;
using ProjectBank.BusinessLogic.Features.Register_Login;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly private DataContext _dbContext;

        public TestController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<TestDtoDouble> PostAccount(TestDtoDouble testDto)
        {
            CreditCardGenerator creditCardGenerator = new CreditCardGenerator(_dbContext);

            Customer customer1 = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = testDto.TestDto2.FirstName,
                LastName = testDto.TestDto2.LastName,
                Country = testDto.TestDto2.Country,
                PhoneNumber = testDto.TestDto2.PhoneNumber,
                Email = testDto.TestDto2.Email
            };

            Account account1 = new Account()
            {
                Id = Guid.NewGuid(),
                Name = testDto.TestDto2.Name,
                CustomerID = customer1.Id,
                Login = testDto.TestDto2.Login,
                Password = testDto.TestDto2.Password,
                Role = testDto.TestDto2.Role
            };
            account1.Token = CreateJwt.Handle(account1);

            Card card1 = new Card()
            {
                Id = Guid.NewGuid(),
                NumberCard = creditCardGenerator.GenerateCardNumber(),
                CardName = testDto.TestDto2.CardName,
                Pincode = testDto.TestDto2.Pincode,
                Date = testDto.TestDto2.Date,
                CVV = testDto.TestDto2.CVV,
                Balance = testDto.TestDto2.Balance,
                AccountID = account1.Id,
            };

            //

            Customer customer2 = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = testDto.TestDto1.FirstName,
                LastName = testDto.TestDto1.LastName,
                Country = testDto.TestDto1.Country,
                PhoneNumber = testDto.TestDto1.PhoneNumber,
                Email = testDto.TestDto1.Email
            };

            Account account2 = new Account()
            {
                Id = Guid.NewGuid(),
                Name = testDto.TestDto1.Name,
                CustomerID = customer2.Id,
                Login = testDto.TestDto1.Login,
                Password = testDto.TestDto1.Password,
                Role = testDto.TestDto1.Role
            };
            account2.Token = CreateJwt.Handle(account2);

            Card card2 = new Card()
            {
                Id = Guid.NewGuid(),
                NumberCard = creditCardGenerator.GenerateCardNumber(),
                CardName = testDto.TestDto1.CardName,
                Pincode = testDto.TestDto1.Pincode,
                Date = testDto.TestDto1.Date,
                CVV = testDto.TestDto1.CVV,
                Balance = testDto.TestDto1.Balance,
                AccountID = account2.Id,
            };

            Transaction transaction2 = new Transaction()
            {
                Id = Guid.NewGuid(),
                Date = testDto.TestDto1.TransactionDate,
                Sum = testDto.TestDto1.Sum,
                CardReceiverID = card1.Id,
                CardSenderID = card2.Id
            };

            Transaction transaction1 = new Transaction()
            {
                Id = Guid.NewGuid(),
                Date = testDto.TestDto2.TransactionDate,
                Sum = testDto.TestDto2.Sum,
                CardReceiverID = card2.Id,
                CardSenderID = card1.Id
            };



            await _dbContext.Account.AddAsync(account1);
            await _dbContext.Account.AddAsync(account2);

            await _dbContext.Customer.AddAsync(customer1);
            await _dbContext.Customer.AddAsync(customer2);

            await _dbContext.Card.AddAsync(card1);
            await _dbContext.Card.AddAsync(card2);


            await _dbContext.Transaction.AddAsync(transaction1);
            await _dbContext.Transaction.AddAsync(transaction2);
            await _dbContext.SaveChangesAsync();
            return testDto;
        }
    }
}
