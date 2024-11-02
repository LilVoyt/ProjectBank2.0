using MediatR;
using ProjectBank.Application.Models;
using ProjectBank.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Credits.Queries
{
    public class GetAccountCreditsQuery : IRequest<List<CreditDto>>
    {
        public Guid AccountId { get; set; }
    }
}
