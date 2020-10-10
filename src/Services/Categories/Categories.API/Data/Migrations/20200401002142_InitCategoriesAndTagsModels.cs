using Microsoft.EntityFrameworkCore.Migrations;

namespace Categories.API.Migrations
{
    public partial class InitCategoriesAndTagsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesAndProductsLink",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(nullable: false),
                    CategoryID = table.Column<string>(nullable: false),
                    CategoryID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesAndProductsLink", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoriesAndProductsLink_Categories_CategoryID1",
                        column: x => x.CategoryID1,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagsAndProductsLink",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(nullable: false),
                    TagID = table.Column<string>(nullable: false),
                    TagID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsAndProductsLink", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TagsAndProductsLink_Tags_TagID1",
                        column: x => x.TagID1,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesAndProductsLink_CategoryID1",
                table: "CategoriesAndProductsLink",
                column: "CategoryID1");

            migrationBuilder.CreateIndex(
                name: "IX_TagsAndProductsLink_TagID1",
                table: "TagsAndProductsLink",
                column: "TagID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriesAndProductsLink");

            migrationBuilder.DropTable(
                name: "TagsAndProductsLink");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
