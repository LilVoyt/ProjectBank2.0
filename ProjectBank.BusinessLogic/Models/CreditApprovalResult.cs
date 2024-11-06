using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Models
{
    public class CreditApprovalResult
    {
        public string Status { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
