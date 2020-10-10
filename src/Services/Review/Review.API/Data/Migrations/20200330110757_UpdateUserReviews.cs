using Microsoft.EntityFrameworkCore.Migrations;

namespace Review.API.Migrations
{
    public partial class UpdateUserReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    ProductID = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: false),
                    ReviewText = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
