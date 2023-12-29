using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiTarifave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LokacionetDepartamenti_Lokacionet_LokacioniID",
                table: "LokacionetDepartamenti");

            migrationBuilder.AddColumn<double>(
                name: "TarifaFikse",
                table: "ZbritjaStudenti",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifaStudimitID",
                table: "ZbritjaStudenti",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KestiPageses",
                table: "Pagesat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LokacioniID",
                table: "LokacionetDepartamenti",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TarifatDepartamenti",
                columns: table => new
                {
                    TarifaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    NiveliStudimitID = table.Column<int>(type: "int", nullable: true),
                    TarifaVjetore = table.Column<double>(type: "float", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarifatDepartamenti", x => x.TarifaID);
                    table.ForeignKey(
                        name: "FK_TarifatDepartamenti_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_TarifatDepartamenti_NiveliStudimeve_NiveliStudimitID",
                        column: x => x.NiveliStudimitID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZbritjaStudenti_TarifaStudimitID",
                table: "ZbritjaStudenti",
                column: "TarifaStudimitID");

            migrationBuilder.CreateIndex(
                name: "IX_TarifatDepartamenti_DepartamentiID",
                table: "TarifatDepartamenti",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_TarifatDepartamenti_NiveliStudimitID",
                table: "TarifatDepartamenti",
                column: "NiveliStudimitID");

            migrationBuilder.AddForeignKey(
                name: "FK_LokacionetDepartamenti_Lokacionet_LokacioniID",
                table: "LokacionetDepartamenti",
                column: "LokacioniID",
                principalTable: "Lokacionet",
                principalColumn: "LokacioniID");

            migrationBuilder.AddForeignKey(
                name: "FK_ZbritjaStudenti_TarifatDepartamenti_TarifaStudimitID",
                table: "ZbritjaStudenti",
                column: "TarifaStudimitID",
                principalTable: "TarifatDepartamenti",
                principalColumn: "TarifaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LokacionetDepartamenti_Lokacionet_LokacioniID",
                table: "LokacionetDepartamenti");

            migrationBuilder.DropForeignKey(
                name: "FK_ZbritjaStudenti_TarifatDepartamenti_TarifaStudimitID",
                table: "ZbritjaStudenti");

            migrationBuilder.DropTable(
                name: "TarifatDepartamenti");

            migrationBuilder.DropIndex(
                name: "IX_ZbritjaStudenti_TarifaStudimitID",
                table: "ZbritjaStudenti");

            migrationBuilder.DropColumn(
                name: "TarifaFikse",
                table: "ZbritjaStudenti");

            migrationBuilder.DropColumn(
                name: "TarifaStudimitID",
                table: "ZbritjaStudenti");

            migrationBuilder.DropColumn(
                name: "KestiPageses",
                table: "Pagesat");

            migrationBuilder.AlterColumn<int>(
                name: "LokacioniID",
                table: "LokacionetDepartamenti",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LokacionetDepartamenti_Lokacionet_LokacioniID",
                table: "LokacionetDepartamenti",
                column: "LokacioniID",
                principalTable: "Lokacionet",
                principalColumn: "LokacioniID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
