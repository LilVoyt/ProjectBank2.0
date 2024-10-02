using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Accounts.Queries;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetByLogin(string login)
        {
            GetAccountByLoginQuery loginQuery = new GetAccountByLoginQuery() { Login = login };
            var result = await _mediator.Send(loginQuery);
            return Ok(result);
        }
    }
}
