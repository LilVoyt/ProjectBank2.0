using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Customers.Queries;
using ProjectBank.BusinessLogic.Features.Transactions.Queries;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TrasnsactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrasnsactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? sender, Guid? receiver, string? sortItem, string? sortOrder)
        {

            var response = new GetTransactionQuery() { Sender = sender, Receiver = receiver, SortItem = sortItem, SortOrder = sortOrder };
            var result = await _mediator.Send(response);

            return Ok(result);
        }
    }
}
