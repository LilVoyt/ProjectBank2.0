using MediatR;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Register_Login.Commands
{
    public class LoginIntoAccountCommand : IRequest<Account>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
