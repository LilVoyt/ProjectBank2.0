using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        [HttpPost("/register")]
        public async Task<IActionResult> Post(RegisterCommand user)
        {
            string Jwt = await mediator.Send(user);
            return Ok(new
            {
                Token = Jwt,
                Message = "Login success"
            });
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginCommand userLogin)
        {
            var jwt = await mediator.Send(userLogin);
            return Ok(new
            {
                Token = jwt,
                Message = "Login success"
            });
        }

    }
}
