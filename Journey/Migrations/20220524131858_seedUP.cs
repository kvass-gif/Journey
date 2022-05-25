using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Migrations
{
    public partial class seedUP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3aebe9a1-b211-45f9-86e2-0537259c618d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d6bb834-07eb-4c47-9128-9bf0145f8597");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a8d84c0-b8df-42f1-a286-93b77b2ceda9", "ca51d7e0-1ec7-442d-9876-2123d6b1d7cd", "Tenant", "TENANT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c735ff1a-033b-41f4-a083-589f50b52f14", "efc864ad-2377-4c48-b478-7d16ef049b53", "LandLord", "LANDLORD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a8d84c0-b8df-42f1-a286-93b77b2ceda9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c735ff1a-033b-41f4-a083-589f50b52f14");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3aebe9a1-b211-45f9-86e2-0537259c618d", "f9fccf24-0e19-4a31-861f-55be5511946f", "LandLord", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9d6bb834-07eb-4c47-9128-9bf0145f8597", "0699de06-f0da-4004-a7c4-94e791be7c1e", "Tenant", null });
        }
    }
}
