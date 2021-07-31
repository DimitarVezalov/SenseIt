using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseIt.Data.Migrations
{
    public partial class AddReviewAddOverallRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_PostedById",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Services_AppServiceId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Review_PostedById",
                table: "Reviews",
                newName: "IX_Reviews_PostedById");

            migrationBuilder.RenameIndex(
                name: "IX_Review_IsDeleted",
                table: "Reviews",
                newName: "IX_Reviews_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Review_AppServiceId",
                table: "Reviews",
                newName: "IX_Reviews_AppServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_PostedById",
                table: "Reviews",
                column: "PostedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Services_AppServiceId",
                table: "Reviews",
                column: "AppServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_PostedById",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Services_AppServiceId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PostedById",
                table: "Review",
                newName: "IX_Review_PostedById");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_IsDeleted",
                table: "Review",
                newName: "IX_Review_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_AppServiceId",
                table: "Review",
                newName: "IX_Review_AppServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_PostedById",
                table: "Review",
                column: "PostedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Services_AppServiceId",
                table: "Review",
                column: "AppServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
