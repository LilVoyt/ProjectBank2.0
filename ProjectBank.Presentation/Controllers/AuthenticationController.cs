using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Register_Login.Commands;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/regandlog")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("/register")]
        public async Task<IActionResult> Post(RegisterCommand user)
        {

            try
            {
                var account = await _mediator.Send(user);
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
                var account = await _mediator.Send(userLogin);
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
