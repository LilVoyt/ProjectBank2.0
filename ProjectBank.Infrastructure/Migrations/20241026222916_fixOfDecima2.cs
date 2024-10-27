using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class fixOfDecima2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("1accfadf-fa10-4984-8e96-0bae047975c3"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("545a8072-dfb0-41a5-beed-72eedaac9f21"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("82db340f-0cd9-4fea-996c-a6b8e2ba672a"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("f25f7fa1-3139-4e90-95a6-8eaca256fdad"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("1a00a12f-c315-4efc-b608-14b3182a9cd5"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("3fc17f83-2d19-4862-b9bb-d14abc5f97a0"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("686aef0d-37be-45c8-afc6-38635120a2d7"));

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyPayment",
                table: "Credit",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualInterestRate",
                table: "Credit",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.InsertData(
                table: "CreditType",
                columns: new[] { "Id", "Description", "InterestRateMultiplier", "Name" },
                values: new object[,]
                {
                    { new Guid("06aa989b-ca2e-4889-84f0-bea9c19346a7"), "Small, short-term loan, often with a high interest rate.", 1.5m, "Microloan" },
                    { new Guid("611592f4-679a-45e5-b182-02407c4611bf"), "Used to buy real estate. Long-term with property as collateral.", 0.5m, "Mortgage Loan" },
                    { new Guid("6f64b368-b2b5-4bac-b5ba-0491a66c85eb"), "Used for personal purchases, like electronics or vacations.", 1.0m, "Consumer Loan" },
                    { new Guid("e5a1f202-66e8-4f85-9e4f-01a7f71e2164"), "For business expenses like equipment or expansion.", 0.9m, "Business Loan" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("04c6127b-6a35-4a6f-9fe5-d27fdda3a389"), 1.2m, "EUR", "Euro" },
                    { new Guid("93915b70-2560-46a6-a20f-0cfc15f0f8c0"), 2.0m, "UAH", "Ukrainian Hryvnia" },
                    { new Guid("f273d9d0-be17-4dda-a7f4-940f526f7782"), 1.5m, "USD", "US Dollar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("06aa989b-ca2e-4889-84f0-bea9c19346a7"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("611592f4-679a-45e5-b182-02407c4611bf"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("6f64b368-b2b5-4bac-b5ba-0491a66c85eb"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("e5a1f202-66e8-4f85-9e4f-01a7f71e2164"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("04c6127b-6a35-4a6f-9fe5-d27fdda3a389"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("93915b70-2560-46a6-a20f-0cfc15f0f8c0"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("f273d9d0-be17-4dda-a7f4-940f526f7782"));

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyPayment",
                table: "Credit",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualInterestRate",
                table: "Credit",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.InsertData(
                table: "CreditType",
                columns: new[] { "Id", "Description", "InterestRateMultiplier", "Name" },
                values: new object[,]
                {
                    { new Guid("1accfadf-fa10-4984-8e96-0bae047975c3"), "For business expenses like equipment or expansion.", 0.9m, "Business Loan" },
                    { new Guid("545a8072-dfb0-41a5-beed-72eedaac9f21"), "Used for personal purchases, like electronics or vacations.", 1.0m, "Consumer Loan" },
                    { new Guid("82db340f-0cd9-4fea-996c-a6b8e2ba672a"), "Small, short-term loan, often with a high interest rate.", 1.5m, "Microloan" },
                    { new Guid("f25f7fa1-3139-4e90-95a6-8eaca256fdad"), "Used to buy real estate. Long-term with property as collateral.", 0.5m, "Mortgage Loan" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("1a00a12f-c315-4efc-b608-14b3182a9cd5"), 1.5m, "USD", "US Dollar" },
                    { new Guid("3fc17f83-2d19-4862-b9bb-d14abc5f97a0"), 1.2m, "EUR", "Euro" },
                    { new Guid("686aef0d-37be-45c8-afc6-38635120a2d7"), 2.0m, "UAH", "Ukrainian Hryvnia" }
                });
        }
    }
}
