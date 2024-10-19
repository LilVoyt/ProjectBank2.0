using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Currency;

namespace ProjectBank.Presentation.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class CardController(IMediator mediator, IGetNewestCurrency newestCurrency) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(AddCardCommand cardCommand)
        {
            var res = await mediator.Send(cardCommand);
            return Ok(res);
        }
        [HttpGet]
        public string Get()
        {
            var res = newestCurrency.GetFromApi();
            return res;
        }
    }
}
