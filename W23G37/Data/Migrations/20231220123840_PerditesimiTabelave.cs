using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimiTabelave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_UserId",
                table: "TeDhenatRegjistrimitStudentit");

            migrationBuilder.DropIndex(
                name: "IX_Pagesat_AplikimiID",
                table: "Pagesat");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_UserId",
                table: "TeDhenatRegjistrimitStudentit",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagesat_AplikimiID",
                table: "Pagesat",
                column: "AplikimiID",
                unique: true,
                filter: "[AplikimiID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_UserId",
                table: "TeDhenatRegjistrimitStudentit");

            migrationBuilder.DropIndex(
                name: "IX_Pagesat_AplikimiID",
                table: "Pagesat");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_UserId",
                table: "TeDhenatRegjistrimitStudentit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagesat_AplikimiID",
                table: "Pagesat",
                column: "AplikimiID");
        }
    }
}
