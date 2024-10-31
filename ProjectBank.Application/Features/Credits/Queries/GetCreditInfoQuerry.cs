using MediatR;
using ProjectBank.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Credits.Queries
{
    public class GetCreditInfoQuerry : IRequest<PayingInfo>
    {
        public Guid CreditId { get; set; }
    }
}
