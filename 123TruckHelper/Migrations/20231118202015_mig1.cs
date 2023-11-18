using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _123TruckHelper.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadId = table.Column<int>(type: "int", nullable: false),
                    OriginLatitude = table.Column<double>(type: "float", nullable: false),
                    OriginLongitude = table.Column<double>(type: "float", nullable: false),
                    DestinationLatitude = table.Column<double>(type: "float", nullable: false),
                    DestinationLongitude = table.Column<double>(type: "float", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mileage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckId = table.Column<int>(type: "int", nullable: false),
                    PositionLatitude = table.Column<double>(type: "float", nullable: false),
                    PositionLongitude = table.Column<double>(type: "float", nullable: false),
                    EquipType = table.Column<int>(type: "int", nullable: false),
                    NextTripLengthPreference = table.Column<int>(type: "int", nullable: false),
                    Busy = table.Column<bool>(type: "bit", nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TruckId = table.Column<int>(type: "int", nullable: false),
                    LoadId = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Loads_LoadId",
                        column: x => x.LoadId,
                        principalTable: "Loads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LoadId",
                table: "Notifications",
                column: "LoadId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TruckId",
                table: "Notifications",
                column: "TruckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "Trucks");
        }
    }
}
