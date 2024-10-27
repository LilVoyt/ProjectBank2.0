using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Services.Credits
{
    public class CreditService(DataContext context) : ICreditService
    {
        public async Task<List<Credit>> Get(Guid cardId)
        {
            var res1 = await context.Credit.SingleOrDefaultAsync(c => c.CardId == cardId);
            var res = await context.Credit.Where(c => c.CardId == cardId).ToListAsync();
            return res;
        }

        public async Task<CreditType> GetByName(string name)
        {
            var creditType = await context.CreditType.SingleOrDefaultAsync(c => c.Name == name);
            return creditType;
        }

        public async Task<CreditType> GetTypeById(Guid id)
        {
            var creditType = await context.CreditType.SingleOrDefaultAsync(c => c.Id == id);
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
    }
}
