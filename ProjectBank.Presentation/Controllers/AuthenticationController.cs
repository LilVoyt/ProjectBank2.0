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

            try
            {
                var account = await mediator.Send(user);
                return Ok(new
                {
                    Token = account.Token,
                    Message = "Login success"
                });
            }
            catch (KeyNotFoundException)
            {
                return Unauthorized(new { Message = "Invalid login or password." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginCommand userLogin)
        {
            try
            {
                var account = await mediator.Send(userLogin);
                return Ok(new
                {
                    Token = account.Token,
                    Message = "Login success"
                });
            }
            catch (KeyNotFoundException)
            {
                return Unauthorized(new { Message = "Invalid login or password." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }

    }
}
