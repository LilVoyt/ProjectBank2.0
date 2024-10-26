using MediatR;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Credits.Commands
{
    public class CreateCreditCommand : IRequest<CreditDto>
    {
        public string CardNumber { get; set; }
        public decimal Principal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CurrencyCode { get; set; }
        public string CreditTypeName { get; set; }
    }
}
