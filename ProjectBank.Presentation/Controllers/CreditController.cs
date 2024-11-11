using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Features.Credits.Commands;
using ProjectBank.Application.Features.Credits.Queries;
using ProjectBank.BusinessLogic.Features.Authentication.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Queries;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/credit")]
    [ApiController]
    public class CreditController(IMediator mediator) : ControllerBase
    {
        [HttpPost("/create")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Post(CreateCreditCommand credit)
        {
            var res = await mediator.Send(credit);
            return Ok(res);
        }

        [HttpPost("/monthly-pay")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> MonthlyPayment(CreditMonthlyPaymentCommand command) 
        {
            var res = await mediator.Send(command);
            return Ok(res);
        }

        [HttpGet("/get-card-credits")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetByCard(Guid cardId)
        {
            GetCreditsQuery query = new GetCreditsQuery() { cardId = cardId };
            var res = await mediator.Send(query);

            return Ok(res);
        }

        [HttpGet("/get-account-credits")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetByAccount(Guid accountId)
        {
            GetAccountCreditsQuery query = new GetAccountCreditsQuery() { AccountId = accountId };
            var res = await mediator.Send(query);

            return Ok(res);
        }

        [HttpGet("/get-credit-info")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetInfo(Guid creditId)
        {
            GetCreditInfoQuerry creditInfoQuerry = new GetCreditInfoQuerry() { CreditId = creditId };
            var res = mediator.Send(creditInfoQuerry);
            return Ok(res.Result);
        }
    }
}
