using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PaymentService.API.Migrations
{
    public partial class InitPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    PaidDate = table.Column<DateTime>(nullable: false),
                    Canceled = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}