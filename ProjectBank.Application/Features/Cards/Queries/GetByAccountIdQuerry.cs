using MediatR;
using ProjectBank.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Cards.Queries
{
    public class GetByAccountIdQuerry : IRequest<List<CardDto>>
    {
        public Guid AccountId { get; set; }
    }
}
