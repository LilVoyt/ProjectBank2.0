using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            GetAccountByLoginQuery loginQuery = new GetAccountByLoginQuery() { Login = login };
            var result = await mediator.Send(loginQuery);
            return Ok(result);
        }
    }
}
