using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Models
{
    public class CreditDto
    {
        public string CardNumber { get; set; }
        public decimal Principal { get; set; }
        public decimal AmountToRepay { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CurrencyName { get; set; }
        public string CreditTypeName { get; set; }
    }
}
