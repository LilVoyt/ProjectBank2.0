using MediatR;

namespace ProjectBank.BusinessLogic.Features.Cards.Commands
{
    public class AddCardCommand : IRequest<Guid>
    {
        public string Pincode { get; set; }
        public string CardName { get; set; }
        public string CurrencyCode { get; set; }
        public Guid AccountID { get; set; }
    }
}
