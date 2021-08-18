using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseIt.Data.Migrations
{
    public partial class AddedOrderItemImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "OrderItems");
        }
    }
}
