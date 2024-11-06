using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Async;

namespace ProjectBank.DataAcces.Services.Credits
{
    public class CreditService(DataContext context) : ICreditService
    {
        public async Task<List<Credit>> Get(Guid cardId, CancellationToken cancellationToken)
        {
            var res = await context.Credit.Where(c => c.CardId == cardId).ToListAsync(cancellationToken);
            return res;
        }

        public async Task<List<Credit>> GetByAccount(Guid accountId, CancellationToken cancellationToken)
        {
            var credits = await context.Credit
                    .Where(c => c.Card.AccountID == accountId)
                    .ToListAsync(cancellationToken);
            return credits;
        }

        public async Task<CreditType> GetByName(string name)
        {
            var creditType = await context.CreditType.SingleOrDefaultAsync(c => c.Name == name);
            return creditType;
        }

        public async Task<decimal> GetLimitByCurrencyCode(string name)
        {
            var creditType = await context.CreditType.SingleAsync(c => c.Name == name);
            return creditType.MaxCreditLimit;
        }

        public async Task<CreditType> GetTypeById(Guid id)
        {
            var creditType = await context.CreditType.SingleOrDefaultAsync(c => c.Id == id) ?? throw new ArgumentNullException();
            return creditType;
        }

        public async Task<Credit> GetById(Guid id)
        {
            var credit = await context.Credit.SingleOrDefaultAsync(c => c.Id == id);
            return credit;
        }

        public async Task<Credit> Post(Credit credit)
        {

            await context.Credit.AddAsync(credit);
            await context.SaveChangesAsync();

            return credit;
        }

        public async Task<Credit> Update(Credit credit)
        {
            context.Credit.Update(credit);
            await context.SaveChangesAsync();
            return credit;
        }
    }
}
