using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Models
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
        public Guid CardSenderID { get; set; }
        public Guid CardReceiverID { get; set; }
    }
}
