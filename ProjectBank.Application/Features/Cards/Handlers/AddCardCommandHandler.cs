using MediatR;
using ProjectBank.BusinessLogic.CardManagement;
using ProjectBank.BusinessLogic.Features.Cards.Commands;

namespace ProjectBank.BusinessLogic.Features.Cards.Handlers
{
    public class AddCardCommandHandler(ICardManagementService managementService)
        : IRequestHandler<AddCardCommand, Guid>
    {
        public async Task<Guid> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            Guid id = await managementService.CreateCardAsync(request.Pincode, request.CardName, request.CurrencyCode, request.AccountID);
            return id;
        }
    }
}
