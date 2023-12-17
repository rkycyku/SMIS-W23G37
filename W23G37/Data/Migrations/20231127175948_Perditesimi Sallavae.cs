using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimiSallavae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LokacionetDepartamenti_DepartamentiID",
                table: "LokacionetDepartamenti");

            migrationBuilder.AlterColumn<string>(
                name: "ShkurtesaLokacionit",
                table: "Lokacionet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LokacionetDepartamenti_DepartamentiID",
                table: "LokacionetDepartamenti",
                column: "DepartamentiID",
                unique: true,
                filter: "[DepartamentiID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LokacionetDepartamenti_DepartamentiID",
                table: "LokacionetDepartamenti");

            migrationBuilder.AlterColumn<string>(
                name: "ShkurtesaLokacionit",
                table: "Lokacionet",
                type: "nvarchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LokacionetDepartamenti_DepartamentiID",
                table: "LokacionetDepartamenti",
                column: "DepartamentiID");
        }
    }
}
