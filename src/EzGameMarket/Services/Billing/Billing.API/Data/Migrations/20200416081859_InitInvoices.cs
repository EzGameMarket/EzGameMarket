using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billing.API.Migrations
{
    public partial class InitInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceFiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileUri = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OwnData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: false),
                    VATNumber = table.Column<string>(maxLength: 14, nullable: false),
                    EUVATNumber = table.Column<string>(maxLength: 9, nullable: false),
                    BankAccountID = table.Column<int>(nullable: false),
                    InvoiceBlockID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserBills",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBills", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    VATNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PostCode = table.Column<string>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    FullfiledDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    UserInvoiceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bills_InvoiceFiles_FileID",
                        column: x => x.FileID,
                        principalTable: "InvoiceFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_UserBills_UserInvoiceID",
                        column: x => x.UserInvoiceID,
                        principalTable: "UserBills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NetPrice = table.Column<int>(nullable: false),
                    BruttoPrice = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    InvoiceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Bills_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_FileID",
                table: "Bills",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserInvoiceID",
                table: "Bills",
                column: "UserInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceID",
                table: "InvoiceItems",
                column: "InvoiceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "OwnData");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "InvoiceFiles");

            migrationBuilder.DropTable(
                name: "UserBills");
        }
    }
}
