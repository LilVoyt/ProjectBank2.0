using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Cards.Commands;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class CardController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(AddCardCommand cardCommand)
        {
            var res = await mediator.Send(cardCommand);
            return Ok(res);
        }
    }
}
