using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Migrations.ApplicationDb
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArrived = table.Column<bool>(type: "bit", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "CreatedAt", "Description", "PlaceName", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2022, 5, 24, 15, 25, 10, 749, DateTimeKind.Local).AddTicks(9475), "Ut atque eius nobis.", "Apartment on Kamyanetskaya street", new DateTime(2022, 5, 24, 15, 25, 10, 749, DateTimeKind.Local).AddTicks(9505) });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "CreatedAt", "Description", "PlaceName", "UpdatedAt" },
                values: new object[] { 2, new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(136), "Et quos est dolor quisquam et sit iure consectetur fugit.", "Avto Spa", new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(140) });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "ArrivalDate", "CreatedAt", "DepartureDate", "IsArrived", "PlaceId", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1021), new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1015), new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1023), false, 1, new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1020) });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "ArrivalDate", "CreatedAt", "DepartureDate", "IsArrived", "PlaceId", "UpdatedAt" },
                values: new object[] { 2, new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1028), new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1025), new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1029), false, 1, new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1026) });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "ArrivalDate", "CreatedAt", "DepartureDate", "IsArrived", "PlaceId", "UpdatedAt" },
                values: new object[] { 3, new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1065), new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1061), new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1066), false, 2, new DateTime(2022, 5, 24, 15, 25, 10, 750, DateTimeKind.Local).AddTicks(1063) });

            migrationBuilder.CreateIndex(
                name: "IX_Places_PlaceName",
                table: "Places",
                column: "PlaceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlaceId",
                table: "Reservations",
                column: "PlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
