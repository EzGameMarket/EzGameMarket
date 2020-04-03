using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudGamingSupport.API.Migrations
{
    public partial class MatchesTableAddedToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudGamingProvidersAndGames_Providers_CloudGamingProviderID",
                table: "CloudGamingProvidersAndGames");

            migrationBuilder.DropForeignKey(
                name: "FK_CloudGamingProvidersAndGames_Games_CloudGamingSupportedID",
                table: "CloudGamingProvidersAndGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CloudGamingProvidersAndGames",
                table: "CloudGamingProvidersAndGames");

            migrationBuilder.RenameTable(
                name: "CloudGamingProvidersAndGames",
                newName: "Matches");

            migrationBuilder.RenameIndex(
                name: "IX_CloudGamingProvidersAndGames_CloudGamingSupportedID",
                table: "Matches",
                newName: "IX_Matches_CloudGamingSupportedID");

            migrationBuilder.RenameIndex(
                name: "IX_CloudGamingProvidersAndGames_CloudGamingProviderID",
                table: "Matches",
                newName: "IX_Matches_CloudGamingProviderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Providers_CloudGamingProviderID",
                table: "Matches",
                column: "CloudGamingProviderID",
                principalTable: "Providers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Games_CloudGamingSupportedID",
                table: "Matches",
                column: "CloudGamingSupportedID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Providers_CloudGamingProviderID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Games_CloudGamingSupportedID",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "CloudGamingProvidersAndGames");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_CloudGamingSupportedID",
                table: "CloudGamingProvidersAndGames",
                newName: "IX_CloudGamingProvidersAndGames_CloudGamingSupportedID");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_CloudGamingProviderID",
                table: "CloudGamingProvidersAndGames",
                newName: "IX_CloudGamingProvidersAndGames_CloudGamingProviderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CloudGamingProvidersAndGames",
                table: "CloudGamingProvidersAndGames",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudGamingProvidersAndGames_Providers_CloudGamingProviderID",
                table: "CloudGamingProvidersAndGames",
                column: "CloudGamingProviderID",
                principalTable: "Providers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CloudGamingProvidersAndGames_Games_CloudGamingSupportedID",
                table: "CloudGamingProvidersAndGames",
                column: "CloudGamingSupportedID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
