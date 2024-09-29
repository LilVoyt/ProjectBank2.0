using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Models
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<CardDto> Cards { get; set; }
    }
}
