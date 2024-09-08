using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountEmployee",
                columns: table => new
                {
                    AccountsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEmployee", x => new { x.AccountsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_AccountEmployee_Account_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountEmployee_Employee_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sum = table.Column<double>(type: "float", nullable: false),
                    CardSenderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardReceiverID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Card_CardReceiverID",
                        column: x => x.CardReceiverID,
                        principalTable: "Card",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_Card_CardSenderID",
                        column: x => x.CardSenderID,
                        principalTable: "Card",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CustomerID",
                table: "Account",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountEmployee_EmployeesId",
                table: "AccountEmployee",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_AccountID",
                table: "Card",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardReceiverID",
                table: "Transaction",
                column: "CardReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardSenderID",
                table: "Transaction",
                column: "CardSenderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountEmployee");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
