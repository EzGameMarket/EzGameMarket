using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudGamingSupport.API.Migrations
{
    public partial class SupportedGamesAddedToCloudGamingProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Games_CloudGamingSupportedID",
                table: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Providers_CloudGamingSupportedID",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "CloudGamingSupportedID",
                table: "Providers");

            migrationBuilder.CreateTable(
                name: "CloudGamingProvidersAndGames",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CloudGamingProviderID = table.Column<int>(nullable: false),
                    CloudGamingSupportedID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudGamingProvidersAndGames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CloudGamingProvidersAndGames_Providers_CloudGamingProviderID",
                        column: x => x.CloudGamingProviderID,
                        principalTable: "Providers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CloudGamingProvidersAndGames_Games_CloudGamingSupportedID",
                        column: x => x.CloudGamingSupportedID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CloudGamingProvidersAndGames_CloudGamingProviderID",
                table: "CloudGamingProvidersAndGames",
                column: "CloudGamingProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_CloudGamingProvidersAndGames_CloudGamingSupportedID",
                table: "CloudGamingProvidersAndGames",
                column: "CloudGamingSupportedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CloudGamingProvidersAndGames");

            migrationBuilder.AddColumn<int>(
                name: "CloudGamingSupportedID",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Providers_CloudGamingSupportedID",
                table: "Providers",
                column: "CloudGamingSupportedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Games_CloudGamingSupportedID",
                table: "Providers",
                column: "CloudGamingSupportedID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
