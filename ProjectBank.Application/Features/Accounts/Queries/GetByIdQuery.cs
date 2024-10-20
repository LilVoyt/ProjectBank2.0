using MediatR;
using ProjectBank.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Accounts.Queries
{
    public class GetByIdQuery : IRequest<AccountDto>
    {
        public Guid Id { get; set; }
    }
}
