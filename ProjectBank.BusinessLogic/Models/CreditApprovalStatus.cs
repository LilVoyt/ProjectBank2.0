using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Models
{
    public enum CreditApprovalStatus
    {
        Approved,
        CreditLimitExceeded,
        NotApproved,
        RequiresManualReview
    }
}
