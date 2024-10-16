using MediatR;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Commands
{
    public class AddCardCommand : IRequest<Card>
    {
        public string Pincode { get; set; }
        public string CardName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid AccountID { get; set; }
    }
}
