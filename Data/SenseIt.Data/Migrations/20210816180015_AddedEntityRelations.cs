using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseIt.Data.Migrations
{
    public partial class AddedEntityRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_CustometId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CustometId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "CustometId",
                table: "Carts",
                newName: "CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_CustomerId",
                table: "Carts",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_CustomerId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Carts",
                newName: "CustometId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustometId",
                table: "Carts",
                column: "CustometId",
                unique: true,
                filter: "[CustometId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_CustometId",
                table: "Carts",
                column: "CustometId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
