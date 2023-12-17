using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimiAPS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmriAPS",
                table: "AfatiParaqitjesSemestrit",
                newName: "VitiAkademik");

            migrationBuilder.AddColumn<string>(
                name: "LlojiSemestrit",
                table: "AfatiParaqitjesSemestrit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NiveliStudimeveID",
                table: "AfatiParaqitjesSemestrit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AfatiParaqitjesSemestrit_NiveliStudimeveID",
                table: "AfatiParaqitjesSemestrit",
                column: "NiveliStudimeveID");

            migrationBuilder.AddForeignKey(
                name: "FK_AfatiParaqitjesSemestrit_NiveliStudimeve_NiveliStudimeveID",
                table: "AfatiParaqitjesSemestrit",
                column: "NiveliStudimeveID",
                principalTable: "NiveliStudimeve",
                principalColumn: "NiveliStudimeveID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfatiParaqitjesSemestrit_NiveliStudimeve_NiveliStudimeveID",
                table: "AfatiParaqitjesSemestrit");

            migrationBuilder.DropIndex(
                name: "IX_AfatiParaqitjesSemestrit_NiveliStudimeveID",
                table: "AfatiParaqitjesSemestrit");

            migrationBuilder.DropColumn(
                name: "LlojiSemestrit",
                table: "AfatiParaqitjesSemestrit");

            migrationBuilder.DropColumn(
                name: "NiveliStudimeveID",
                table: "AfatiParaqitjesSemestrit");

            migrationBuilder.RenameColumn(
                name: "VitiAkademik",
                table: "AfatiParaqitjesSemestrit",
                newName: "EmriAPS");
        }
    }
}
