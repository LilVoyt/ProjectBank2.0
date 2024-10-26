using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class creditTypeAddedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("6bc4ff03-bef1-4895-a8c9-f02ca12fff1e"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("8724e9c8-036a-4579-9d81-056b9fd0c473"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("b847e2c2-f79f-4b00-8541-2fa15a403449"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("c06b4e83-e575-42c6-8d49-0dd0948a2f33"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("42414a2e-aa76-4dfb-a5ec-2f0d30736d6b"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("d1f0e947-ac1e-4330-97df-75dea3187ea5"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("d6e08512-3d1f-44ad-a260-ac789913e764"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CreditType",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_CreditType_Name",
                table: "CreditType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CreditType_Name",
                table: "CreditType");

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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CreditType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "CreditType",
                columns: new[] { "Id", "Description", "InterestRateMultiplier", "Name" },
                values: new object[,]
                {
                    { new Guid("6bc4ff03-bef1-4895-a8c9-f02ca12fff1e"), "Used for personal purchases, like electronics or vacations.", 1.0m, "Consumer Loan" },
                    { new Guid("8724e9c8-036a-4579-9d81-056b9fd0c473"), "Used to buy real estate. Long-term with property as collateral.", 0.5m, "Mortgage Loan" },
                    { new Guid("b847e2c2-f79f-4b00-8541-2fa15a403449"), "For business expenses like equipment or expansion.", 0.9m, "Business Loan" },
                    { new Guid("c06b4e83-e575-42c6-8d49-0dd0948a2f33"), "Small, short-term loan, often with a high interest rate.", 1.5m, "Microloan" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("42414a2e-aa76-4dfb-a5ec-2f0d30736d6b"), 1.2m, "EUR", "Euro" },
                    { new Guid("d1f0e947-ac1e-4330-97df-75dea3187ea5"), 2.0m, "UAH", "Ukrainian Hryvnia" },
                    { new Guid("d6e08512-3d1f-44ad-a260-ac789913e764"), 1.5m, "USD", "US Dollar" }
                });
        }
    }
}
