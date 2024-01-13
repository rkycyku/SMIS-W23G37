using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimeTekParaqitjet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota",
                table: "ParaqitjaSemestrit");

            migrationBuilder.AddColumn<string>(
                name: "Nota",
                table: "ParaqitjaProvimit",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota",
                table: "ParaqitjaProvimit");

            migrationBuilder.AddColumn<string>(
                name: "Nota",
                table: "ParaqitjaSemestrit",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
