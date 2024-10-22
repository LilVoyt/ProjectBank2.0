using ProjectBank.DataAcces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? EmployeeID { get; set; }
        public Guid CustomerID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; } //need to be not null
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
