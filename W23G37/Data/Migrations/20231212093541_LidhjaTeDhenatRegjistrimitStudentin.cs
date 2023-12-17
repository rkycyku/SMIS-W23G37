using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class LidhjaTeDhenatRegjistrimitStudentin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TeDhenatRegjistrimitStudentit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_UserId",
                table: "TeDhenatRegjistrimitStudentit",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeDhenatRegjistrimitStudentit_Perdoruesit_UserId",
                table: "TeDhenatRegjistrimitStudentit",
                column: "UserId",
                principalTable: "Perdoruesit",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeDhenatRegjistrimitStudentit_Perdoruesit_UserId",
                table: "TeDhenatRegjistrimitStudentit");

            migrationBuilder.DropIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_UserId",
                table: "TeDhenatRegjistrimitStudentit");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TeDhenatRegjistrimitStudentit");
        }
    }
}
