using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerdetesimiEmrave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZbritjaStudenti_Perdoruesit_StudentiID",
                table: "ZbritjaStudenti");

            migrationBuilder.DropForeignKey(
                name: "FK_ZbritjaStudenti_TarifatDepartamenti_TarifaStudimitID",
                table: "ZbritjaStudenti");

            migrationBuilder.DropForeignKey(
                name: "FK_ZbritjaStudenti_Zbritjet_Zbritja1ID",
                table: "ZbritjaStudenti");

            migrationBuilder.DropForeignKey(
                name: "FK_ZbritjaStudenti_Zbritjet_Zbritja2ID",
                table: "ZbritjaStudenti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZbritjaStudenti",
                table: "ZbritjaStudenti");

            migrationBuilder.RenameTable(
                name: "ZbritjaStudenti",
                newName: "TarifaStudenti");

            migrationBuilder.RenameIndex(
                name: "IX_ZbritjaStudenti_Zbritja2ID",
                table: "TarifaStudenti",
                newName: "IX_TarifaStudenti_Zbritja2ID");

            migrationBuilder.RenameIndex(
                name: "IX_ZbritjaStudenti_Zbritja1ID",
                table: "TarifaStudenti",
                newName: "IX_TarifaStudenti_Zbritja1ID");

            migrationBuilder.RenameIndex(
                name: "IX_ZbritjaStudenti_TarifaStudimitID",
                table: "TarifaStudenti",
                newName: "IX_TarifaStudenti_TarifaStudimitID");

            migrationBuilder.RenameIndex(
                name: "IX_ZbritjaStudenti_StudentiID",
                table: "TarifaStudenti",
                newName: "IX_TarifaStudenti_StudentiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TarifaStudenti",
                table: "TarifaStudenti",
                column: "ZbritjaStudentiID");

            migrationBuilder.AddForeignKey(
                name: "FK_TarifaStudenti_Perdoruesit_StudentiID",
                table: "TarifaStudenti",
                column: "StudentiID",
                principalTable: "Perdoruesit",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_TarifaStudenti_TarifatDepartamenti_TarifaStudimitID",
                table: "TarifaStudenti",
                column: "TarifaStudimitID",
                principalTable: "TarifatDepartamenti",
                principalColumn: "TarifaID");

            migrationBuilder.AddForeignKey(
                name: "FK_TarifaStudenti_Zbritjet_Zbritja1ID",
                table: "TarifaStudenti",
                column: "Zbritja1ID",
                principalTable: "Zbritjet",
                principalColumn: "ZbritjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_TarifaStudenti_Zbritjet_Zbritja2ID",
                table: "TarifaStudenti",
                column: "Zbritja2ID",
                principalTable: "Zbritjet",
                principalColumn: "ZbritjaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarifaStudenti_Perdoruesit_StudentiID",
                table: "TarifaStudenti");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifaStudenti_TarifatDepartamenti_TarifaStudimitID",
                table: "TarifaStudenti");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifaStudenti_Zbritjet_Zbritja1ID",
                table: "TarifaStudenti");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifaStudenti_Zbritjet_Zbritja2ID",
                table: "TarifaStudenti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TarifaStudenti",
                table: "TarifaStudenti");

            migrationBuilder.RenameTable(
                name: "TarifaStudenti",
                newName: "ZbritjaStudenti");

            migrationBuilder.RenameIndex(
                name: "IX_TarifaStudenti_Zbritja2ID",
                table: "ZbritjaStudenti",
                newName: "IX_ZbritjaStudenti_Zbritja2ID");

            migrationBuilder.RenameIndex(
                name: "IX_TarifaStudenti_Zbritja1ID",
                table: "ZbritjaStudenti",
                newName: "IX_ZbritjaStudenti_Zbritja1ID");

            migrationBuilder.RenameIndex(
                name: "IX_TarifaStudenti_TarifaStudimitID",
                table: "ZbritjaStudenti",
                newName: "IX_ZbritjaStudenti_TarifaStudimitID");

            migrationBuilder.RenameIndex(
                name: "IX_TarifaStudenti_StudentiID",
                table: "ZbritjaStudenti",
                newName: "IX_ZbritjaStudenti_StudentiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZbritjaStudenti",
                table: "ZbritjaStudenti",
                column: "ZbritjaStudentiID");

            migrationBuilder.AddForeignKey(
                name: "FK_ZbritjaStudenti_Perdoruesit_StudentiID",
                table: "ZbritjaStudenti",
                column: "StudentiID",
                principalTable: "Perdoruesit",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ZbritjaStudenti_TarifatDepartamenti_TarifaStudimitID",
                table: "ZbritjaStudenti",
                column: "TarifaStudimitID",
                principalTable: "TarifatDepartamenti",
                principalColumn: "TarifaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ZbritjaStudenti_Zbritjet_Zbritja1ID",
                table: "ZbritjaStudenti",
                column: "Zbritja1ID",
                principalTable: "Zbritjet",
                principalColumn: "ZbritjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ZbritjaStudenti_Zbritjet_Zbritja2ID",
                table: "ZbritjaStudenti",
                column: "Zbritja2ID",
                principalTable: "Zbritjet",
                principalColumn: "ZbritjaID");
        }
    }
}
