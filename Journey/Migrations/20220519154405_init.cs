using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "CreatedAt", "Password", "Role", "UpdatedAt" },
                values: new object[] { 1, "Igor", new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6098), "pass", 0, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6133) });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "CreatedAt", "Password", "Role", "UpdatedAt" },
                values: new object[] { 2, "Sasha", new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6138), "pass", 0, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6139) });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountName", "CreatedAt", "Password", "Role", "UpdatedAt" },
                values: new object[] { 3, "Roma", new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6141), "pass", 1, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6143) });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "AccountId", "CreatedAt", "Description", "PlaceName", "UpdatedAt" },
                values: new object[] { 1, 3, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6238), "Maiores quis ipsam et.", "Apartment on Kamyanetskaya street", new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6241) });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "AccountId", "CreatedAt", "Description", "PlaceName", "UpdatedAt" },
                values: new object[] { 2, 3, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6945), "Iste ad rem saepe asperiores inventore.", "Avto Spa", new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(6950) });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "AccountId", "ArrivalDate", "CreatedAt", "DepartureDate", "IsArrived", "PlaceId", "UpdatedAt" },
                values: new object[] { 1, 1, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7507), new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7501), new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7509), false, 1, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7505) });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "AccountId", "ArrivalDate", "CreatedAt", "DepartureDate", "IsArrived", "PlaceId", "UpdatedAt" },
                values: new object[] { 2, 2, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7514), new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7511), new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7515), false, 1, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7512) });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "AccountId", "ArrivalDate", "CreatedAt", "DepartureDate", "IsArrived", "PlaceId", "UpdatedAt" },
                values: new object[] { 3, 2, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7520), new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7517), new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7522), false, 2, new DateTime(2022, 5, 19, 18, 44, 5, 501, DateTimeKind.Local).AddTicks(7519) });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountName",
                table: "Accounts",
                column: "AccountName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_AccountId",
                table: "Places",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_PlaceName",
                table: "Places",
                column: "PlaceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AccountId",
                table: "Reservations",
                column: "AccountId");

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

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
