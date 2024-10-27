using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class fixOfDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("61a963f0-19c5-40fd-8747-350afbb258c5"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("7db9e3d2-9bdd-456c-bf7d-4d379e481615"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("cee2b405-e8f5-4c7a-88dc-f2bb985d10e2"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("f37194af-97c5-415c-95d8-3421a2131177"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("3d5feb75-64b8-4bb3-b9bc-60eafb168582"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("96745839-6c9d-4911-97ff-f626b9388a5b"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("d6bd7e66-13e1-4b5a-b2a6-c31515281a61"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Principal",
                table: "Credit",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyPayment",
                table: "Credit",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Principal",
                table: "Credit",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyPayment",
                table: "Credit",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "CreditType",
                columns: new[] { "Id", "Description", "InterestRateMultiplier", "Name" },
                values: new object[,]
                {
                    { new Guid("61a963f0-19c5-40fd-8747-350afbb258c5"), "For business expenses like equipment or expansion.", 0.9m, "Business Loan" },
                    { new Guid("7db9e3d2-9bdd-456c-bf7d-4d379e481615"), "Used to buy real estate. Long-term with property as collateral.", 0.5m, "Mortgage Loan" },
                    { new Guid("cee2b405-e8f5-4c7a-88dc-f2bb985d10e2"), "Used for personal purchases, like electronics or vacations.", 1.0m, "Consumer Loan" },
                    { new Guid("f37194af-97c5-415c-95d8-3421a2131177"), "Small, short-term loan, often with a high interest rate.", 1.5m, "Microloan" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("3d5feb75-64b8-4bb3-b9bc-60eafb168582"), 1.5m, "USD", "US Dollar" },
                    { new Guid("96745839-6c9d-4911-97ff-f626b9388a5b"), 1.2m, "EUR", "Euro" },
                    { new Guid("d6bd7e66-13e1-4b5a-b2a6-c31515281a61"), 2.0m, "UAH", "Ukrainian Hryvnia" }
                });
        }
    }
}
