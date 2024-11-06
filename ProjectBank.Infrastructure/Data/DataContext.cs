using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Entities;
using System;

namespace ProjectBank.DataAcces.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Account { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Credit> Credit { get; set; }
        public DbSet<CreditType> CreditType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CustomerID).IsRequired();

                entity.HasOne(b => b.Customer)
                    .WithOne(a => a.Account)
                    .HasForeignKey<Account>(b => b.CustomerID);

                entity.HasMany(b => b.Employees)
                    .WithMany(a => a.Accounts);

                entity.HasIndex(b => b.Login).IsUnique();
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.AccountID).IsRequired();
                entity.Property(b => b.CurrencyID).IsRequired();

                entity.HasOne(b => b.Account)
                    .WithMany(a => a.Cards)
                    .HasForeignKey(b => b.AccountID)
                    .OnDelete(DeleteBehavior.Restrict); // Заборона видалення

                entity.HasOne(b => b.Currency)
                    .WithMany(a => a.Cards)
                    .HasForeignKey(b => b.CurrencyID)
                    .OnDelete(DeleteBehavior.Restrict); // Заборона видалення

                entity.Property(b => b.Balance)
                    .HasPrecision(12, 2);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CardSenderID).IsRequired();
                entity.Property(b => b.CardReceiverID).IsRequired();
                entity.Property(b => b.CurrencyId).IsRequired();

                entity.HasOne(e => e.CardSender)
                    .WithMany(c => c.SentTransactions)
                    .HasForeignKey(e => e.CardSenderID)
                    .OnDelete(DeleteBehavior.Restrict); // Заборона видалення

                entity.HasOne(e => e.CardReceiver)
                    .WithMany(c => c.ReceivedTransactions)
                    .HasForeignKey(e => e.CardReceiverID)
                    .OnDelete(DeleteBehavior.Restrict); // Заборона видалення

                entity.HasOne(t => t.Currency)
                    .WithMany(c => c.Transactions)
                    .HasForeignKey(t => t.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict); // Заборона видалення

                entity.Property(t => t.Sum)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Credit>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CardId)
                .IsRequired();
                entity.Property(b => b.CurrencyId)
                .IsRequired();
                entity.Property(e => e.CreditTypeId)
                .IsRequired();

                entity.HasOne(c => c.Card)
                    .WithMany(c => c.Credits)
                    .HasForeignKey(c => c.CardId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(c => c.Currency)
                    .WithMany(c => c.Credits)
                    .HasForeignKey(c => c.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.CreditType)
                .WithMany(c => c.Credits)
                .HasForeignKey(c => c.CreditTypeId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(b => b.Principal)
                    .HasPrecision(12, 2);

                entity.Property(b => b.AmountToRepay)
                .HasPrecision(12, 2);

                entity.Property(b => b.MonthlyPayment)
                    .HasPrecision(12, 2);

                entity.Property(b => b.AnnualInterestRate)
                    .HasPrecision(12, 2);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasIndex(b => b.CurrencyCode).IsUnique();

                entity.Property(b => b.AnnualInterestRate).HasPrecision(5, 2);

                entity.HasData(
                    new Currency { Id = Guid.NewGuid(), CurrencyCode = "USD", CurrencyName = "US Dollar", AnnualInterestRate = 1.5m },
                    new Currency { Id = Guid.NewGuid(), CurrencyCode = "EUR", CurrencyName = "Euro", AnnualInterestRate = 1.2m },
                    new Currency { Id = Guid.NewGuid(), CurrencyCode = "UAH", CurrencyName = "Ukrainian Hryvnia", AnnualInterestRate = 2.0m }
                );
            });

            modelBuilder.Entity<CreditType>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasIndex(b => b.Name)
                .IsUnique();

                entity.Property(b => b.InterestRateMultiplier)
                .HasPrecision(5, 2);

                entity.Property(b => b.MaxCreditLimit)
                .HasPrecision(12, 2);

                entity.HasData(
                    new CreditType 
                    { 
                        Id = Guid.NewGuid(), 
                        Name = "Consumer Loan", 
                        InterestRateMultiplier = 1.0m, 
                        Description = "Used for personal purchases, like electronics or vacations.",
                        MaxCreditLimit = 100000.0m
                    },
                    new CreditType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Mortgage Loan",
                        InterestRateMultiplier = 0.5m,
                        Description = "Used to buy real estate. Long-term with property as collateral.",
                        MaxCreditLimit = 1000000.0m
                    },
                    new CreditType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Microloan",
                        InterestRateMultiplier = 1.5m,
                        Description = "Small, short-term loan, often with a high interest rate.",
                        MaxCreditLimit = 1000.0m
                    },
                    new CreditType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Business Loan",
                        InterestRateMultiplier = 0.9m,
                        Description = "For business expenses like equipment or expansion.",
                        MaxCreditLimit = 500000.0m
                    }
                    );
            });
        }
    }
}
