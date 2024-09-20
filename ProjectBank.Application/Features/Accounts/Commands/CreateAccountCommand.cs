using MediatR;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<Account>
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid CustomerID { get; set; }
    }
}
