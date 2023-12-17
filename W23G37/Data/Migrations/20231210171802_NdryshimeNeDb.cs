using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class NdryshimeNeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplikimetEReja_SpecializimetPerDepartament_SpecializimiID",
                table: "AplikimetEReja");

            migrationBuilder.DropIndex(
                name: "IX_AplikimetEReja_SpecializimiID",
                table: "AplikimetEReja");

            migrationBuilder.DropColumn(
                name: "SpecializimiID",
                table: "AplikimetEReja");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecializimiID",
                table: "AplikimetEReja",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AplikimetEReja_SpecializimiID",
                table: "AplikimetEReja",
                column: "SpecializimiID");

            migrationBuilder.AddForeignKey(
                name: "FK_AplikimetEReja_SpecializimetPerDepartament_SpecializimiID",
                table: "AplikimetEReja",
                column: "SpecializimiID",
                principalTable: "SpecializimetPerDepartament",
                principalColumn: "SpecializimiID");
        }
    }
}
