using Microsoft.EntityFrameworkCore.Migrations;

namespace Categories.API.Migrations
{
    public partial class TagAndCategoryLinksUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryID1",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagID1",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductTags_TagID1",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CategoryID1",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "TagID1",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "CategoryID1",
                table: "ProductCategories");

            migrationBuilder.AlterColumn<int>(
                name: "TagID",
                table: "ProductTags",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "ProductCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagID",
                table: "ProductTags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryID",
                table: "ProductCategories",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryID",
                table: "ProductCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagID",
                table: "ProductTags",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryID",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagID",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductTags_TagID",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CategoryID",
                table: "ProductCategories");

            migrationBuilder.AlterColumn<string>(
                name: "TagID",
                table: "ProductTags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagID1",
                table: "ProductTags",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryID",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID1",
                table: "ProductCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagID1",
                table: "ProductTags",
                column: "TagID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryID1",
                table: "ProductCategories",
                column: "CategoryID1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryID1",
                table: "ProductCategories",
                column: "CategoryID1",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagID1",
                table: "ProductTags",
                column: "TagID1",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
