using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class CreditTypeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("3dbd7fba-fe3a-475d-b971-ae2417f9b53f"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("4acfbb47-d564-44b2-b3e0-d12b74d11317"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("6be67f6e-54c0-4c36-b606-0dba734ffcdd"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("b04c937a-25a6-4328-b172-d3226dc77b94"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("28d62178-c4e7-4b8a-9d87-dd83a78fcc02"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("ddc9084c-af72-4775-aebb-cb4f12c3c258"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("f1d7c6c9-9727-4979-afd4-ccd03d64ddfe"));

            migrationBuilder.AddColumn<decimal>(
                name: "MaxCreditLimit",
                table: "CreditType",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "CreditType",
                columns: new[] { "Id", "Description", "InterestRateMultiplier", "MaxCreditLimit", "Name" },
                values: new object[,]
                {
                    { new Guid("063f2ac1-6978-4fd4-8a95-1fd5a93d475e"), "Small, short-term loan, often with a high interest rate.", 1.5m, 1000.0m, "Microloan" },
                    { new Guid("7c4ce595-991a-4ad2-b673-3da608c75334"), "Used for personal purchases, like electronics or vacations.", 1.0m, 100000.0m, "Consumer Loan" },
                    { new Guid("7e7ee526-401a-491b-8ae3-62f4c3630823"), "For business expenses like equipment or expansion.", 0.9m, 500000.0m, "Business Loan" },
                    { new Guid("f1572e23-c59b-4120-8b3b-3ea40c0e7478"), "Used to buy real estate. Long-term with property as collateral.", 0.5m, 1000000.0m, "Mortgage Loan" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("4570efe9-2b59-47e9-8338-5a5dfffc599b"), 2.0m, "UAH", "Ukrainian Hryvnia" },
                    { new Guid("64f9830b-2e51-48a2-8886-c830bffb0000"), 1.5m, "USD", "US Dollar" },
                    { new Guid("79b638b9-23d8-42b4-ba2c-d0ede0ea9380"), 1.2m, "EUR", "Euro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("063f2ac1-6978-4fd4-8a95-1fd5a93d475e"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("7c4ce595-991a-4ad2-b673-3da608c75334"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("7e7ee526-401a-491b-8ae3-62f4c3630823"));

            migrationBuilder.DeleteData(
                table: "CreditType",
                keyColumn: "Id",
                keyValue: new Guid("f1572e23-c59b-4120-8b3b-3ea40c0e7478"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("4570efe9-2b59-47e9-8338-5a5dfffc599b"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("64f9830b-2e51-48a2-8886-c830bffb0000"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("79b638b9-23d8-42b4-ba2c-d0ede0ea9380"));

            migrationBuilder.DropColumn(
                name: "MaxCreditLimit",
                table: "CreditType");

            migrationBuilder.InsertData(
                table: "CreditType",
                columns: new[] { "Id", "Description", "InterestRateMultiplier", "Name" },
                values: new object[,]
                {
                    { new Guid("3dbd7fba-fe3a-475d-b971-ae2417f9b53f"), "Used to buy real estate. Long-term with property as collateral.", 0.5m, "Mortgage Loan" },
                    { new Guid("4acfbb47-d564-44b2-b3e0-d12b74d11317"), "Used for personal purchases, like electronics or vacations.", 1.0m, "Consumer Loan" },
                    { new Guid("6be67f6e-54c0-4c36-b606-0dba734ffcdd"), "For business expenses like equipment or expansion.", 0.9m, "Business Loan" },
                    { new Guid("b04c937a-25a6-4328-b172-d3226dc77b94"), "Small, short-term loan, often with a high interest rate.", 1.5m, "Microloan" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("28d62178-c4e7-4b8a-9d87-dd83a78fcc02"), 1.5m, "USD", "US Dollar" },
                    { new Guid("ddc9084c-af72-4775-aebb-cb4f12c3c258"), 2.0m, "UAH", "Ukrainian Hryvnia" },
                    { new Guid("f1d7c6c9-9727-4979-afd4-ccd03d64ddfe"), 1.2m, "EUR", "Euro" }
                });
        }
    }
}
