using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiNiveliStudimitDepartamenti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KodiFinanciar",
                table: "AplikimetEReja",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NiveliStudimitDepartamenti",
                columns: table => new
                {
                    NiveliStudimitDepartamentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveliStudimitID = table.Column<int>(type: "int", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: false),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiveliStudimitDepartamenti", x => x.NiveliStudimitDepartamentiID);
                    table.ForeignKey(
                        name: "FK_NiveliStudimitDepartamenti_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NiveliStudimitDepartamenti_NiveliStudimeve_NiveliStudimitID",
                        column: x => x.NiveliStudimitID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NiveliStudimitDepartamenti_DepartamentiID",
                table: "NiveliStudimitDepartamenti",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_NiveliStudimitDepartamenti_NiveliStudimitID",
                table: "NiveliStudimitDepartamenti",
                column: "NiveliStudimitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NiveliStudimitDepartamenti");

            migrationBuilder.DropColumn(
                name: "KodiFinanciar",
                table: "AplikimetEReja");
        }
    }
}
