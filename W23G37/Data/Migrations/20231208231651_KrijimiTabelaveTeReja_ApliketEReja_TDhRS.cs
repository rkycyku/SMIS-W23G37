using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiTabelaveTeReja_ApliketEReja_TDhRS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataLindjes",
                table: "TeDhenatPerdoruesit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailPersonal",
                table: "TeDhenatPerdoruesit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmriPrindit",
                table: "TeDhenatPerdoruesit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gjinia",
                table: "TeDhenatPerdoruesit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NrPersonal",
                table: "TeDhenatPerdoruesit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AplikimetEReja",
                columns: table => new
                {
                    AplikimiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrPersonal = table.Column<int>(type: "int", nullable: true),
                    EmriPrindit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrKontaktit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qyteti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipKodi = table.Column<int>(type: "int", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shteti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gjinia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataLindjes = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    NiveliStudimitID = table.Column<int>(type: "int", nullable: true),
                    VitiAkademikRegjistrim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlojiRegjistrimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecializimiID = table.Column<int>(type: "int", nullable: true),
                    DataRegjistrimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplikimetEReja", x => x.AplikimiID);
                    table.ForeignKey(
                        name: "FK_AplikimetEReja_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_AplikimetEReja_NiveliStudimeve_NiveliStudimitID",
                        column: x => x.NiveliStudimitID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                    table.ForeignKey(
                        name: "FK_AplikimetEReja_SpecializimetPerDepartament_SpecializimiID",
                        column: x => x.SpecializimiID,
                        principalTable: "SpecializimetPerDepartament",
                        principalColumn: "SpecializimiID");
                });

            migrationBuilder.CreateTable(
                name: "TeDhenatRegjistrimitStudentit",
                columns: table => new
                {
                    TDhRSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KodiFinanciar = table.Column<int>(type: "int", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    NiveliStudimitID = table.Column<int>(type: "int", nullable: true),
                    VitiAkademikRegjistrim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegjistrimit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LlojiRegjistrimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecializimiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeDhenatRegjistrimitStudentit", x => x.TDhRSID);
                    table.ForeignKey(
                        name: "FK_TeDhenatRegjistrimitStudentit_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_TeDhenatRegjistrimitStudentit_NiveliStudimeve_NiveliStudimitID",
                        column: x => x.NiveliStudimitID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                    table.ForeignKey(
                        name: "FK_TeDhenatRegjistrimitStudentit_SpecializimetPerDepartament_SpecializimiID",
                        column: x => x.SpecializimiID,
                        principalTable: "SpecializimetPerDepartament",
                        principalColumn: "SpecializimiID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AplikimetEReja_DepartamentiID",
                table: "AplikimetEReja",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_AplikimetEReja_NiveliStudimitID",
                table: "AplikimetEReja",
                column: "NiveliStudimitID");

            migrationBuilder.CreateIndex(
                name: "IX_AplikimetEReja_SpecializimiID",
                table: "AplikimetEReja",
                column: "SpecializimiID");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_DepartamentiID",
                table: "TeDhenatRegjistrimitStudentit",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_NiveliStudimitID",
                table: "TeDhenatRegjistrimitStudentit",
                column: "NiveliStudimitID");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_SpecializimiID",
                table: "TeDhenatRegjistrimitStudentit",
                column: "SpecializimiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplikimetEReja");

            migrationBuilder.DropTable(
                name: "TeDhenatRegjistrimitStudentit");

            migrationBuilder.DropColumn(
                name: "DataLindjes",
                table: "TeDhenatPerdoruesit");

            migrationBuilder.DropColumn(
                name: "EmailPersonal",
                table: "TeDhenatPerdoruesit");

            migrationBuilder.DropColumn(
                name: "EmriPrindit",
                table: "TeDhenatPerdoruesit");

            migrationBuilder.DropColumn(
                name: "Gjinia",
                table: "TeDhenatPerdoruesit");

            migrationBuilder.DropColumn(
                name: "NrPersonal",
                table: "TeDhenatPerdoruesit");
        }
    }
}
