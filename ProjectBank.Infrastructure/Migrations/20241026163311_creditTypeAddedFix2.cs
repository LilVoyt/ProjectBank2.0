using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class creditTypeAddedFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("3ceadc1e-00ec-4f5a-bee4-dec50623f1e5"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("8158edba-fe1e-4413-8dcb-5fd472ee3562"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("9c4865e3-d02b-419c-b455-30d621cc99cd"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("be9ab938-78ce-4882-9f64-eea50e579439"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("16ca5e73-0070-4cbf-a20e-8eba8c49f791"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("baee4892-6983-4d61-9ccb-558bda33fe76"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("e54dda7b-cef2-4b35-a8cf-9a98cb468523"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "CreditType",
                columns: new[] { "Id", "Description", "InterestRateMultiplier", "Name" },
                values: new object[,]
                {
                    { new Guid("3ceadc1e-00ec-4f5a-bee4-dec50623f1e5"), "Used to buy real estate. Long-term with property as collateral.", 0.5m, "Mortgage Loan" },
                    { new Guid("8158edba-fe1e-4413-8dcb-5fd472ee3562"), "For business expenses like equipment or expansion.", 0.9m, "Business Loan" },
                    { new Guid("9c4865e3-d02b-419c-b455-30d621cc99cd"), "Small, short-term loan, often with a high interest rate.", 1.5m, "Microloan" },
                    { new Guid("be9ab938-78ce-4882-9f64-eea50e579439"), "Used for personal purchases, like electronics or vacations.", 1.0m, "Consumer Loan" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("16ca5e73-0070-4cbf-a20e-8eba8c49f791"), 2.0m, "UAH", "Ukrainian Hryvnia" },
                    { new Guid("baee4892-6983-4d61-9ccb-558bda33fe76"), 1.2m, "EUR", "Euro" },
                    { new Guid("e54dda7b-cef2-4b35-a8cf-9a98cb468523"), 1.5m, "USD", "US Dollar" }
                });
        }
    }
}
