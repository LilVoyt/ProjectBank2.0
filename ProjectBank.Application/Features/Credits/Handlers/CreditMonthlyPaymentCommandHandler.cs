using MediatR;
using ProjectBank.Application.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Features.Credits.Commands;
using ProjectBank.BusinessLogic.Finance;
using ProjectBank.BusinessLogic.Models;
using ProjectBank.DataAcces.Entities;
using ProjectBank.DataAcces.Services.Cards;
using ProjectBank.DataAcces.Services.Credits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Application.Features.Credits.Handlers
{
    internal class CreditMonthlyPaymentCommandHandler(ICreditManagementService creditManagement) : IRequestHandler<CreditMonthlyPaymentCommand, Guid>
    {
        public async Task<Guid> Handle(CreditMonthlyPaymentCommand request, CancellationToken cancellationToken)
        {
            return await creditManagement.CreditAnnualPayment(request.CreditId, cancellationToken);
        }
    }
}
