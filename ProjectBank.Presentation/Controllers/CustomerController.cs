using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Features.Customers.Commands;
using ProjectBank.Application.Features.Customers.Queries;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? search, string? sortItem, string? sortOrder)
        {
            var command = new GetCustomerQuery() { Search = search, SortItem = sortItem, SortOrder = sortOrder };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand customer)
        {
            var result = await _mediator.Send(customer);
            return Ok(result);
        }
    }
}
