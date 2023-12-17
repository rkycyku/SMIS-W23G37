using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiiParaqitjesseSemestrit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PataqitjaSemestrit",
                columns: table => new
                {
                    ParaqitjaSemestritID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semestri = table.Column<int>(type: "int", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    StudentiID = table.Column<int>(type: "int", nullable: true),
                    DataParaqitjes = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PataqitjaSemestrit", x => x.ParaqitjaSemestritID);
                    table.ForeignKey(
                        name: "FK_PataqitjaSemestrit_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_PataqitjaSemestrit_Perdoruesit_StudentiID",
                        column: x => x.StudentiID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PataqitjaSemestrit_DepartamentiID",
                table: "PataqitjaSemestrit",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_PataqitjaSemestrit_StudentiID",
                table: "PataqitjaSemestrit",
                column: "StudentiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PataqitjaSemestrit");
        }
    }
}
