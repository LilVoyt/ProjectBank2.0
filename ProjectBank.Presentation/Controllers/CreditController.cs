﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Features.Credits.Commands;
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
        public async Task<IActionResult> Post(CreateCreditCommand credit)
        {
            var res = await mediator.Send(credit);
            return Ok(res);
        }

        [HttpPost("/monthly-pay")]
        public async Task<IActionResult> MonthlyPayment(CreditMonthlyPaymentCommand paymentCommand)
        {
            var res = await mediator.Send(paymentCommand);
            return Ok(res);
        }

        [HttpGet] 
        public async Task<IActionResult> Get(Guid cardId)
        {
            GetCreditsQuery query = new GetCreditsQuery() { cardId = cardId };
            var res = mediator.Send(query);

            return Ok(res.Result);
        }
    }
}
