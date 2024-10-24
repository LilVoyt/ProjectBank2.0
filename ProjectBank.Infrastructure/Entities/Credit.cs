using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Entities
{
    public class Credit
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; } 
        public decimal Principal { get; set; } 
        public decimal AnnualInterestRate { get; set; } 
        public decimal? MonthlyPayment { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }  
        public Guid CurrencyId { get; set; }
        public bool IsPaidOff { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Card Card { get; set; }
    }
}
