using Microsoft.EntityFrameworkCore.Migrations;

namespace SenseIt.Data.Migrations
{
    public partial class RenameAppointmentColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserFullName",
                table: "Appointments",
                newName: "CustomerFullName");

            migrationBuilder.RenameColumn(
                name: "UserAge",
                table: "Appointments",
                newName: "CustomerAge");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerFullName",
                table: "Appointments",
                newName: "UserFullName");

            migrationBuilder.RenameColumn(
                name: "CustomerAge",
                table: "Appointments",
                newName: "UserAge");
        }
    }
}
