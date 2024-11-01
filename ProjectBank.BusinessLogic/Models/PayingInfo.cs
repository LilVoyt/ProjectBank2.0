using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Models
{
    public class PayingInfo
    {
        public decimal MonthlyPayment { get; set; }
        public decimal AmountToRepay { get; set; }
        public string CurrencyCode { get; set; }
    }
}
