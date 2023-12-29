using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class VendosjaEZbritjesGjateAplikimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZbritjaID",
                table: "AplikimetEReja",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AplikimetEReja_ZbritjaID",
                table: "AplikimetEReja",
                column: "ZbritjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AplikimetEReja_Zbritjet_ZbritjaID",
                table: "AplikimetEReja",
                column: "ZbritjaID",
                principalTable: "Zbritjet",
                principalColumn: "ZbritjaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplikimetEReja_Zbritjet_ZbritjaID",
                table: "AplikimetEReja");

            migrationBuilder.DropIndex(
                name: "IX_AplikimetEReja_ZbritjaID",
                table: "AplikimetEReja");

            migrationBuilder.DropColumn(
                name: "ZbritjaID",
                table: "AplikimetEReja");
        }
    }
}
