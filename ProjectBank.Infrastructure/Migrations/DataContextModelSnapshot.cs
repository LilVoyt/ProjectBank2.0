﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectBank.DataAcces.Data;

#nullable disable

namespace ProjectBank.DataAcces.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccountEmployee", b =>
                {
                    b.Property<Guid>("AccountsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountsId", "EmployeesId");

                    b.HasIndex("EmployeesId");

                    b.ToTable("AccountEmployee");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CurrencyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumberCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountID");

                    b.HasIndex("CurrencyID");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Credit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AnnualInterestRate")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreditTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaidOff")
                        .HasColumnType("bit");

                    b.Property<decimal?>("MonthlyPayment")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Principal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("CreditTypeId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Credit");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.CreditType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("InterestRateMultiplier")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CreditType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cee2b405-e8f5-4c7a-88dc-f2bb985d10e2"),
                            Description = "Used for personal purchases, like electronics or vacations.",
                            InterestRateMultiplier = 1.0m,
                            Name = "Consumer Loan"
                        },
                        new
                        {
                            Id = new Guid("7db9e3d2-9bdd-456c-bf7d-4d379e481615"),
                            Description = "Used to buy real estate. Long-term with property as collateral.",
                            InterestRateMultiplier = 0.5m,
                            Name = "Mortgage Loan"
                        },
                        new
                        {
                            Id = new Guid("f37194af-97c5-415c-95d8-3421a2131177"),
                            Description = "Small, short-term loan, often with a high interest rate.",
                            InterestRateMultiplier = 1.5m,
                            Name = "Microloan"
                        },
                        new
                        {
                            Id = new Guid("61a963f0-19c5-40fd-8747-350afbb258c5"),
                            Description = "For business expenses like equipment or expansion.",
                            InterestRateMultiplier = 0.9m,
                            Name = "Business Loan"
                        });
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AnnualInterestRate")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode")
                        .IsUnique();

                    b.ToTable("Currency");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3d5feb75-64b8-4bb3-b9bc-60eafb168582"),
                            AnnualInterestRate = 1.5m,
                            CurrencyCode = "USD",
                            CurrencyName = "US Dollar"
                        },
                        new
                        {
                            Id = new Guid("96745839-6c9d-4911-97ff-f626b9388a5b"),
                            AnnualInterestRate = 1.2m,
                            CurrencyCode = "EUR",
                            CurrencyName = "Euro"
                        },
                        new
                        {
                            Id = new Guid("d6bd7e66-13e1-4b5a-b2a6-c31515281a61"),
                            AnnualInterestRate = 2.0m,
                            CurrencyCode = "UAH",
                            CurrencyName = "Ukrainian Hryvnia"
                        });
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardReceiverID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardSenderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Sum")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CardReceiverID");

                    b.HasIndex("CardSenderID");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("AccountEmployee", b =>
                {
                    b.HasOne("ProjectBank.DataAcces.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBank.DataAcces.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Account", b =>
                {
                    b.HasOne("ProjectBank.DataAcces.Entities.Customer", "Customer")
                        .WithOne("Account")
                        .HasForeignKey("ProjectBank.DataAcces.Entities.Account", "CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Card", b =>
                {
                    b.HasOne("ProjectBank.DataAcces.Entities.Account", "Account")
                        .WithMany("Cards")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectBank.DataAcces.Entities.Currency", "Currency")
                        .WithMany("Cards")
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Credit", b =>
                {
                    b.HasOne("ProjectBank.DataAcces.Entities.Card", "Card")
                        .WithMany("Credits")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectBank.DataAcces.Entities.CreditType", "CreditType")
                        .WithMany("Credits")
                        .HasForeignKey("CreditTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectBank.DataAcces.Entities.Currency", "Currency")
                        .WithMany("Credits")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("CreditType");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Transaction", b =>
                {
                    b.HasOne("ProjectBank.DataAcces.Entities.Card", "CardReceiver")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("CardReceiverID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectBank.DataAcces.Entities.Card", "CardSender")
                        .WithMany("SentTransactions")
                        .HasForeignKey("CardSenderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectBank.DataAcces.Entities.Currency", "Currency")
                        .WithMany("Transactions")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CardReceiver");

                    b.Navigation("CardSender");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Account", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Card", b =>
                {
                    b.Navigation("Credits");

                    b.Navigation("ReceivedTransactions");

                    b.Navigation("SentTransactions");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.CreditType", b =>
                {
                    b.Navigation("Credits");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Currency", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Credits");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("ProjectBank.DataAcces.Entities.Customer", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
