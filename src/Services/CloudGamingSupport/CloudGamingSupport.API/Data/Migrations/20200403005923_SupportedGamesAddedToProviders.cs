using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudGamingSupport.API.Migrations
{
    public partial class SupportedGamesAddedToProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Games_CloudGamingSupportedID",
                table: "Providers");

            migrationBuilder.AlterColumn<int>(
                name: "CloudGamingSupportedID",
                table: "Providers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Games_CloudGamingSupportedID",
                table: "Providers",
                column: "CloudGamingSupportedID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Games_CloudGamingSupportedID",
                table: "Providers");

            migrationBuilder.AlterColumn<int>(
                name: "CloudGamingSupportedID",
                table: "Providers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Games_CloudGamingSupportedID",
                table: "Providers",
                column: "CloudGamingSupportedID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
