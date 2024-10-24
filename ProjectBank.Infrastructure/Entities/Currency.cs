using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal AnnualInterestRate { get; set; }

        public virtual ICollection<Card> Cards { get; set; } 
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}
