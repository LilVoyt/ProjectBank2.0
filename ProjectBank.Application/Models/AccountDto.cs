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
        public string Name { get; set; }
        public virtual CustomerDto Customer { get; set; }
        public virtual ICollection<CardDto> Cards { get; set; }
    }
}
