using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Data;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public LoginController(IMediator mediator, DataContext dataContext, IMapper mapper)
        {
            _mediator = mediator;
            _dataContext = dataContext;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            bool IsLoginExist = false;
            bool IsPasswordValid = false;

            var account = await _dataContext.Account.SingleOrDefaultAsync(a => a.Login == userLogin.Login);
            if (account != null)
            {
                IsLoginExist = true;
                IsPasswordValid = account.Password == userLogin.Password;
            }

            if (IsLoginExist && IsPasswordValid)
            {
                return Ok(true);
            }

            return Unauthorized(false);
        }
    }
}
