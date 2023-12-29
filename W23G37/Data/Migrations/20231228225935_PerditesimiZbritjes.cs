using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimiZbritjes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LlojiZbritjes",
                table: "Zbritjet",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LlojiZbritjes",
                table: "Zbritjet");
        }
    }
}
