using MediatR;
using ProjectBank.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Transactions.Queries
{
    public class GetTransactionQuery : IRequest<List<TransactionDto>>
    {
        public Guid? Sender { get; set; }
        public Guid? Receiver { get; set; }
        public string? SortItem { get; set; }
        public string? SortOrder { get; set; }
    }
}
