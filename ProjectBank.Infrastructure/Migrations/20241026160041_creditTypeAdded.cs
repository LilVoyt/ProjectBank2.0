using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class creditTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("36edbee9-69ca-438e-ac86-126f3f8229b6"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("5ea82a84-1cd2-4f30-b4d1-6166ac53c2bf"));

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: new Guid("feb2a842-00cf-486a-8e49-29100afaa4f8"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreditTypeId",
                table: "Credit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CreditType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRateMultiplier = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditType", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Credit_CreditTypeId",
                table: "Credit",
                column: "CreditTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credit_CreditType_CreditTypeId",
                table: "Credit",
                column: "CreditTypeId",
                principalTable: "CreditType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credit_CreditType_CreditTypeId",
                table: "Credit");

            migrationBuilder.DropTable(
                name: "CreditType");

            migrationBuilder.DropIndex(
                name: "IX_Credit_CreditTypeId",
                table: "Credit");

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

            migrationBuilder.DropColumn(
                name: "CreditTypeId",
                table: "Credit");

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "AnnualInterestRate", "CurrencyCode", "CurrencyName" },
                values: new object[,]
                {
                    { new Guid("36edbee9-69ca-438e-ac86-126f3f8229b6"), 1.2m, "EUR", "Euro" },
                    { new Guid("5ea82a84-1cd2-4f30-b4d1-6166ac53c2bf"), 2.0m, "UAH", "Ukrainian Hryvnia" },
                    { new Guid("feb2a842-00cf-486a-8e49-29100afaa4f8"), 1.5m, "USD", "US Dollar" }
                });
        }
    }
}
