using MediatR;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Commands
{
    public class CreateTransactionCommand : IRequest<Guid>
    {
        public string SenderNumber { get; set; } = string.Empty;
        public string ReceiverNumber { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public decimal Sum {  get; set; }
    }
}
