using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogService.API.Migrations
{
    public partial class AddProductKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    ProductID = table.Column<string>(nullable: false),
                    Used = table.Column<bool>(nullable: false),
                    UsedDate = table.Column<DateTime>(nullable: false),
                    BuyerID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keys");
        }
    }
}
