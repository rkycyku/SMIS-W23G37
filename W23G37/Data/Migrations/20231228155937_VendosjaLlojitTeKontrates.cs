using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class VendosjaLlojitTeKontrates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LlojiKontrates",
                table: "TeDhenatRegjistrimitStudentit",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LlojiKontrates",
                table: "AplikimetEReja",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LlojiKontrates",
                table: "TeDhenatRegjistrimitStudentit");

            migrationBuilder.DropColumn(
                name: "LlojiKontrates",
                table: "AplikimetEReja");
        }
    }
}
