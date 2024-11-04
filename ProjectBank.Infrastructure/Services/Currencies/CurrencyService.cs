using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Data;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Services.Currencies
{
    public class CurrencyService(DataContext dataContext) : ICurrencyService
    {
        public async Task<Currency?> GetByCode(string code)
        {
            return await dataContext.Currency.SingleOrDefaultAsync(c => c.CurrencyCode == code);
        }

        public async Task<Currency?> GetByIdAsync(Guid Id)
        {
            Currency currency = await dataContext.Currency.SingleAsync(c => c.Id == Id);

            if(currency == null)
            {
                throw new ArgumentNullException();
            }
            return currency;
        }
    }
}
