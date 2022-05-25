using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Migrations.ApplicationDb
{
    public partial class accountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Places");

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
        }
    }
}
