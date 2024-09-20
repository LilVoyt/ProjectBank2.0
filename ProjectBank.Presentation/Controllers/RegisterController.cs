using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.Infrastructure.Entities;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post(UserDto user)
        {
            CreateCustomerCommand customer = new CreateCustomerCommand()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };


           var customerRes =  await _mediator.Send(customer);

            CreateAccountCommand account = new CreateAccountCommand()
            {
                Name = user.Name,
                Login = user.Login,
                Password = user.Password,
                CustomerID = customerRes.Id,
            };


            await _mediator.Send(account);
            return Ok();
        }


    }
}
