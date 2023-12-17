using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimiDatabazes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfatiParaqitjesSemestrit_Semestri_SemestriID",
                table: "AfatiParaqitjesSemestrit");

            migrationBuilder.DropForeignKey(
                name: "FK_PataqitjaSemestrit_Departamentet_DepartamentiID",
                table: "PataqitjaSemestrit");

            migrationBuilder.DropIndex(
                name: "IX_AfatiParaqitjesSemestrit_SemestriID",
                table: "AfatiParaqitjesSemestrit");

            migrationBuilder.DropColumn(
                name: "SemestriID",
                table: "AfatiParaqitjesSemestrit");

            migrationBuilder.RenameColumn(
                name: "DepartamentiID",
                table: "PataqitjaSemestrit",
                newName: "APSID");

            migrationBuilder.RenameIndex(
                name: "IX_PataqitjaSemestrit_DepartamentiID",
                table: "PataqitjaSemestrit",
                newName: "IX_PataqitjaSemestrit_APSID");

            migrationBuilder.AddColumn<string>(
                name: "NderrimiOrarit",
                table: "PataqitjaSemestrit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PataqitjaSemestrit_AfatiParaqitjesSemestrit_APSID",
                table: "PataqitjaSemestrit",
                column: "APSID",
                principalTable: "AfatiParaqitjesSemestrit",
                principalColumn: "APSID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PataqitjaSemestrit_AfatiParaqitjesSemestrit_APSID",
                table: "PataqitjaSemestrit");

            migrationBuilder.DropColumn(
                name: "NderrimiOrarit",
                table: "PataqitjaSemestrit");

            migrationBuilder.RenameColumn(
                name: "APSID",
                table: "PataqitjaSemestrit",
                newName: "DepartamentiID");

            migrationBuilder.RenameIndex(
                name: "IX_PataqitjaSemestrit_APSID",
                table: "PataqitjaSemestrit",
                newName: "IX_PataqitjaSemestrit_DepartamentiID");

            migrationBuilder.AddColumn<int>(
                name: "SemestriID",
                table: "AfatiParaqitjesSemestrit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AfatiParaqitjesSemestrit_SemestriID",
                table: "AfatiParaqitjesSemestrit",
                column: "SemestriID");

            migrationBuilder.AddForeignKey(
                name: "FK_AfatiParaqitjesSemestrit_Semestri_SemestriID",
                table: "AfatiParaqitjesSemestrit",
                column: "SemestriID",
                principalTable: "Semestri",
                principalColumn: "SemestriID");

            migrationBuilder.AddForeignKey(
                name: "FK_PataqitjaSemestrit_Departamentet_DepartamentiID",
                table: "PataqitjaSemestrit",
                column: "DepartamentiID",
                principalTable: "Departamentet",
                principalColumn: "DepartamentiID");
        }
    }
}
