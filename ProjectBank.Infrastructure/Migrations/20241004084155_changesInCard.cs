using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class changesInCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "Transaction",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Card",
                newName: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "Pincode",
                table: "Card",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CVV",
                table: "Card",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transaction",
                newName: "TransactionDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Card",
                newName: "Data");

            migrationBuilder.AlterColumn<int>(
                name: "Pincode",
                table: "Card",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CVV",
                table: "Card",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
