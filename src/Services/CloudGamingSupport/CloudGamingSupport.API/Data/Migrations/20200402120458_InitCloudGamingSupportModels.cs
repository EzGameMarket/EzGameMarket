using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudGamingSupport.API.Migrations
{
    public partial class InitCloudGamingSupportModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(nullable: false),
                    Supported = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    SearchURl = table.Column<string>(nullable: true),
                    CloudGamingSupportedID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Providers_Games_CloudGamingSupportedID",
                        column: x => x.CloudGamingSupportedID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_CloudGamingSupportedID",
                table: "Providers",
                column: "CloudGamingSupportedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
