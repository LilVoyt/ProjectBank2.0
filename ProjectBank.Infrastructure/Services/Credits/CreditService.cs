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
            var credits = await context.Credit.Where(card => card.CardId == cardId).ToListAsync();
            return credits;
        }

        public async Task<CreditType> GetByName(string name)
        {
            var creditType = await context.CreditType.SingleOrDefaultAsync(c => c.Name == name);
            return creditType;
        }

        public async Task<Credit> Post(Credit credit)
        {

            await context.Credit.AddAsync(credit);
            await context.SaveChangesAsync();

            return credit;
        }
    }
}
