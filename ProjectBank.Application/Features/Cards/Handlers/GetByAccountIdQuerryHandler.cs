using AutoMapper;
using MediatR;
using ProjectBank.BusinessLogic.Features.Cards.Commands;
using ProjectBank.BusinessLogic.Features.Cards.Queries;
using ProjectBank.BusinessLogic.Features.Cards.Service;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Accounts;
using ProjectBank.DataAcces.Services.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Handlers
{
    public class GetByAccountIdQuerryHandler(ICardLogicService cardLogicService) : IRequestHandler<GetByAccountIdQuerry, List<CardDto>>
    {
        public async Task<List<CardDto>> Handle(GetByAccountIdQuerry request, CancellationToken cancellationToken)
        {
            return await cardLogicService.GetCardDtos(request);
        }
    }
}
