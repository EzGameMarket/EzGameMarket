using Microsoft.EntityFrameworkCore.Migrations;

namespace Categories.API.Migrations
{
    public partial class TagAndCategoryReposAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesAndProductsLink_Categories_CategoryID1",
                table: "CategoriesAndProductsLink");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsAndProductsLink_Tags_TagID1",
                table: "TagsAndProductsLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagsAndProductsLink",
                table: "TagsAndProductsLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesAndProductsLink",
                table: "CategoriesAndProductsLink");

            migrationBuilder.RenameTable(
                name: "TagsAndProductsLink",
                newName: "ProductTags");

            migrationBuilder.RenameTable(
                name: "CategoriesAndProductsLink",
                newName: "ProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TagsAndProductsLink_TagID1",
                table: "ProductTags",
                newName: "IX_ProductTags_TagID1");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriesAndProductsLink_CategoryID1",
                table: "ProductCategories",
                newName: "IX_ProductCategories_CategoryID1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "ID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryID1",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagID1",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductTags",
                newName: "TagsAndProductsLink");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "CategoriesAndProductsLink");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_TagID1",
                table: "TagsAndProductsLink",
                newName: "IX_TagsAndProductsLink_TagID1");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_CategoryID1",
                table: "CategoriesAndProductsLink",
                newName: "IX_CategoriesAndProductsLink_CategoryID1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagsAndProductsLink",
                table: "TagsAndProductsLink",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesAndProductsLink",
                table: "CategoriesAndProductsLink",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesAndProductsLink_Categories_CategoryID1",
                table: "CategoriesAndProductsLink",
                column: "CategoryID1",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsAndProductsLink_Tags_TagID1",
                table: "TagsAndProductsLink",
                column: "TagID1",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
