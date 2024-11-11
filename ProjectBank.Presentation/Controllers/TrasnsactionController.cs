using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Transactions.Commands;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TrasnsactionController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Get(Guid? sender, Guid? receiver, string? sortItem, string? sortOrder)
        {

            var response = new GetTransactionQuery() { Sender = sender, Receiver = receiver, SortItem = sortItem, SortOrder = sortOrder };
            var result = await mediator.Send(response);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Post(CreateTransactionCommand transactionCommand)
        {
            var result = await mediator.Send(transactionCommand);
            return Ok();
        }
    }
}
