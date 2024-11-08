using AutoMapper;
using MediatR;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Queries;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using ProjectBank.DataAcces.Services.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Credits.Handlers
{
    public class GetCreditsQueryHandler(ICreditService creditService, ICardService cardService, 
        ICurrencyService currencyService, IMapper mapper)
        : IRequestHandler<GetCreditsQuery, List<CreditDto>>
    {
        public async Task<List<CreditDto>> Handle(GetCreditsQuery request, CancellationToken cancellationToken)
        {
            var credits = await creditService.Get(request.cardId, cancellationToken);

            List<CreditDto> creditsDto = new List<CreditDto>();

            foreach (var credit in credits)
            {
                var creditTypeObject = await creditService.GetTypeById(credit.CreditTypeId);
                var currency = await currencyService.GetByIdAsync(credit.CurrencyId);
                var card = await cardService.GetById(credit.CardId);

                creditsDto.Add(mapper.Map<CreditDto>(credit, opt =>
                {
                    opt.Items["CardNumber"] = card.NumberCard;
                    opt.Items["CurrencyName"] = currency.CurrencyName;
                    opt.Items["CreditTypeName"] = creditTypeObject.Name;
                }));
            }

            return creditsDto;
        }
    }
}
