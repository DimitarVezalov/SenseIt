using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseIt.Data.Migrations
{
    public partial class ChangeServiceCategoriesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCategory_CategoryId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCategory",
                table: "ServiceCategory");

            migrationBuilder.RenameTable(
                name: "ServiceCategory",
                newName: "ServiceCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceCategory_IsDeleted",
                table: "ServiceCategories",
                newName: "IX_ServiceCategories_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCategories",
                table: "ServiceCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCategories_CategoryId",
                table: "Services",
                column: "CategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCategories_CategoryId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCategories",
                table: "ServiceCategories");

            migrationBuilder.RenameTable(
                name: "ServiceCategories",
                newName: "ServiceCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceCategories_IsDeleted",
                table: "ServiceCategory",
                newName: "IX_ServiceCategory_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCategory",
                table: "ServiceCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCategory_CategoryId",
                table: "Services",
                column: "CategoryId",
                principalTable: "ServiceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
