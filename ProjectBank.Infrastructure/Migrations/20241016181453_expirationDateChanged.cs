using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class expirationDateChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Card",
                newName: "ExpirationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Card",
                newName: "Date");
        }
    }
}
