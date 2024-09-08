using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Models
{
    internal class TransactionRequestModel
    {
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
        public Guid CardSenderID { get; set; } = Guid.Empty;
        public Guid CardReceiverID { get; set; } = Guid.Empty;
    }
}
