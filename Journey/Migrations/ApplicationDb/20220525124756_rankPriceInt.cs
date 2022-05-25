using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Migrations.ApplicationDb
{
    public partial class rankPriceInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PricePerNight",
                table: "Places",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PricePerNight",
                table: "Places",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
