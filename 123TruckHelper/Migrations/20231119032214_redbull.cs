using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _123TruckHelper.Migrations
{
    public partial class redbull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Mileage",
                table: "Notifications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Profit",
                table: "Notifications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "Notifications");
        }
    }
}
