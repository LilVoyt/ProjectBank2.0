using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Security.Jwt
{
    public interface IJwtHandler
    {
        string Handle(Account account);
    }
}
