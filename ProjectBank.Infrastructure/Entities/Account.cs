using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Infrastructure.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? EmployeeID { get; set; }
        public Guid CustomerID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
