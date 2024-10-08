﻿using Microsoft.EntityFrameworkCore;
using ProjectBank.DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CustomerID)
                  .IsRequired();

                entity.HasOne(b => b.Customer)
                .WithOne(a => a.Account)
                .HasForeignKey<Account>(b => b.CustomerID);

                entity.HasMany(b => b.Employees)
                .WithMany(a => a.Accounts);

                entity.HasIndex(b => b.Login)
                .IsUnique();
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.AccountID)
                .IsRequired();

                entity.HasOne(b => b.Account)
                .WithMany(a => a.Cards)
                .HasForeignKey(b => b.AccountID);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CardSenderID)
                .IsRequired();

                entity.Property(b => b.CardReceiverID)
                .IsRequired();

                entity.HasOne(e => e.CardSender)
                  .WithMany(c => c.SentTransactions)
                  .HasForeignKey(e => e.CardSenderID)
                  .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.CardReceiver)
                      .WithMany(c => c.ReceivedTransactions)
                      .HasForeignKey(e => e.CardReceiverID)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
