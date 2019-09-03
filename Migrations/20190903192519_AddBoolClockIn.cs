using Microsoft.EntityFrameworkCore.Migrations;

namespace ClockIn_ClockOut.Migrations
{
    public partial class AddBoolClockIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClockIn",
                table: "ClockEvents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockIn",
                table: "ClockEvents");
        }
    }
}
