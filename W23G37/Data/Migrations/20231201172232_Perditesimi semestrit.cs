using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class Perditesimisemestrit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmriSemestrit",
                table: "Semestri");

            migrationBuilder.AddColumn<int>(
                name: "NrSemestrit",
                table: "Semestri",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrSemestrit",
                table: "Semestri");

            migrationBuilder.AddColumn<string>(
                name: "EmriSemestrit",
                table: "Semestri",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
