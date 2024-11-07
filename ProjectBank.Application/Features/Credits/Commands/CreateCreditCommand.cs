using MediatR;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using System;

namespace ProjectBank.BusinessLogic.Features.Credits.Commands
{
    public class CreateCreditCommand : IRequest<CreditDto>
    {
        public string CardNumber { get; set; }
        public decimal Principal { get; set; }
        public int NumberOfMonth { get; set; }
        public DateTime Birthday { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string CreditTypeName { get; set; }
    }
}
