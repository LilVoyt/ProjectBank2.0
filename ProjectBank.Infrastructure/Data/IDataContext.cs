using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.DataAcces.Data
{
    public interface IDataContext
    {
        DbSet<Account> Account { get; set; }
        DbSet<Card> Card { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<Transaction> Transaction { get; set; }
        DbSet<Currency> Currency { get; set; }
        DbSet<Credit> Credit { get; set; }
        DbSet<CreditType> CreditType { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
