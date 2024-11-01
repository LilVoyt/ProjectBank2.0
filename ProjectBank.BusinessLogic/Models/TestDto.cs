using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Models
{
    public class TestDto
    {
        //Account
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; }


        //Card
        public string CardName { get; set; }
        public string Pincode { get; set; }
        public DateTime Date { get; set; }
        public string CVV { get; set; }
        public double Balance { get; set; }

        //Customer
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //Transaction
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
    }
}
