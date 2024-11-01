using ProjectBank.DataAcces.Entities;
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
        public decimal Sum { get; set; }
        public virtual CardDto CardSender { get; set; }
        public virtual CardDto CardReceiver { get; set; }
    }
}
