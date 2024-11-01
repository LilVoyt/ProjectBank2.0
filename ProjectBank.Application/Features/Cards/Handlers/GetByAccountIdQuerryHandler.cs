using MediatR;
using ProjectBank.BusinessLogic.CardManagement;
using ProjectBank.BusinessLogic.Features.Cards.Queries;
using ProjectBank.BusinessLogic.Models;

namespace ProjectBank.BusinessLogic.Features.Cards.Handlers
{
    public class GetByAccountIdQuerryHandler(ICardManagementService managementService) : IRequestHandler<GetByAccountIdQuerry, List<CardDto>>
    {
        public async Task<List<CardDto>> Handle(GetByAccountIdQuerry request, CancellationToken cancellationToken)
        {
            return await managementService.GetCardInfo(request.AccountId);
        }
    }
}
