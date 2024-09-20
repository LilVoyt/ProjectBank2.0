using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using AutoMapper;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public RegisterController(IMediator mediator, DataContext dataContext, IMapper mapper)
        {
            _mediator = mediator;
            _dataContext = dataContext;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Post(UserRegisterDto user)
        {
            using (var transaction = await _dataContext.Database.BeginTransactionAsync())
            {


                var customer = _mapper.Map<CreateCustomerCommand>(user);

                var customerRes = await _mediator.Send(customer);

                var account = _mapper.Map<CreateAccountCommand>(user);
                account.CustomerID = customerRes.Id;


                await _mediator.Send(account);
                return Ok();
            }
        }

        
    }
}
