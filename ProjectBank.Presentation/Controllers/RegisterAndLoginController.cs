using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Customers.Commands;
using ProjectBank.BusinessLogic.Features.Accounts.Commands;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using AutoMapper;
using ProjectBank.BusinessLogic.Features.Register_Login.Commands;
using GreenDonut;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/regandlog")]
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


        [HttpPost("/register")]
        public async Task<IActionResult> Post(CreateNewUserCommand user)
        {

            var result = await _mediator.Send(user);

            if (result != null)
            {
                return Ok(true);
            }

            return Unauthorized();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginIntoAccountCommand userLogin)
        {
            var result = _mediator.Send(userLogin);

            if (result.IsCompleted)
            {
                return Ok(true);
            }

            return Unauthorized(false);
        }

    }
}
