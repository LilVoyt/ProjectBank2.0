using MediatR;
using ProjectBank.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Credits.Commands
{
    public class CreditMonthlyPaymentCommand : IRequest<Guid>
    {
        public Guid CreditId { get; set; }
    }
}
