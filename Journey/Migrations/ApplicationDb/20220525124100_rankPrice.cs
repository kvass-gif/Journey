using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Migrations.ApplicationDb
{
    public partial class rankPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PricePerNight",
                table: "Places",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Places");
        }
    }
}
