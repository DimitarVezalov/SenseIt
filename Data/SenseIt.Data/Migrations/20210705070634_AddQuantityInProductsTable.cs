using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseIt.Data.Migrations
{
    public partial class AddQuantityInProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InStockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStockQuantity",
                table: "Products");
        }
    }
}
