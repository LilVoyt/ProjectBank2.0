using MediatR;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Service;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Credits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Credits.Handlers
{
    internal class CreateCreditCommandHandler(ICreditLogicService creditService) : IRequestHandler<CreateCreditCommand, CreditDto>
    {
        public async Task<CreditDto> Handle(CreateCreditCommand request, CancellationToken cancellationToken)
        {
            return await creditService.CreateCredit(request);
        }
    }
}
