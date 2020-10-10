using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudGamingSupport.API.Migrations
{
    public partial class SupportedBoolPropertyRemovedAndItDependsOnWhichProvidersItHas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supported",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Supported",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
