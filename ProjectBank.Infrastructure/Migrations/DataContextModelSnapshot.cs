﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectBank.Infrastructure.Data;

#nullable disable

namespace ProjectBank.Infrastructure.Migrations
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

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Account", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("CustomerID")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumberCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountID");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Customer", b =>
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

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Employee", b =>
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

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardReceiverID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardSenderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Sum")
                        .HasColumnType("float");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CardReceiverID");

                    b.HasIndex("CardSenderID");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("AccountEmployee", b =>
                {
                    b.HasOne("ProjectBank.Infrastructure.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBank.Infrastructure.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Account", b =>
                {
                    b.HasOne("ProjectBank.Infrastructure.Entities.Customer", "Customer")
                        .WithOne("Account")
                        .HasForeignKey("ProjectBank.Infrastructure.Entities.Account", "CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Card", b =>
                {
                    b.HasOne("ProjectBank.Infrastructure.Entities.Account", "Account")
                        .WithMany("Cards")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Transaction", b =>
                {
                    b.HasOne("ProjectBank.Infrastructure.Entities.Card", "CardReceiver")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("CardReceiverID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjectBank.Infrastructure.Entities.Card", "CardSender")
                        .WithMany("SentTransactions")
                        .HasForeignKey("CardSenderID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CardReceiver");

                    b.Navigation("CardSender");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Account", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Card", b =>
                {
                    b.Navigation("ReceivedTransactions");

                    b.Navigation("SentTransactions");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entities.Customer", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
