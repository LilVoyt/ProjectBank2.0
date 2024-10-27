using MediatR;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Credits.Queries
{
    public class GetCreditsQuery : IRequest<List<CreditDto>>
    {
        public Guid cardId;
    }
}
