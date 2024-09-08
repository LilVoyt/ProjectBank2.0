using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Models
{
    internal class AccountRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public Guid? EmployeeID { get; set; }
        public Guid CustomerID { get; set; } = Guid.Empty;
    }
}
