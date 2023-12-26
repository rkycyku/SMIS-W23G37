using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiIZbritjeve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zbritjet",
                columns: table => new
                {
                    ZbritjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriZbritjes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zbritja = table.Column<double>(type: "float", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zbritjet", x => x.ZbritjaID);
                });

            migrationBuilder.CreateTable(
                name: "ZbritjaStudenti",
                columns: table => new
                {
                    ZbritjaStudentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentiID = table.Column<int>(type: "int", nullable: true),
                    Zbritja1ID = table.Column<int>(type: "int", nullable: true),
                    Zbritja2ID = table.Column<int>(type: "int", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZbritjaStudenti", x => x.ZbritjaStudentiID);
                    table.ForeignKey(
                        name: "FK_ZbritjaStudenti_Perdoruesit_StudentiID",
                        column: x => x.StudentiID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_ZbritjaStudenti_Zbritjet_Zbritja1ID",
                        column: x => x.Zbritja1ID,
                        principalTable: "Zbritjet",
                        principalColumn: "ZbritjaID");
                    table.ForeignKey(
                        name: "FK_ZbritjaStudenti_Zbritjet_Zbritja2ID",
                        column: x => x.Zbritja2ID,
                        principalTable: "Zbritjet",
                        principalColumn: "ZbritjaID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZbritjaStudenti_StudentiID",
                table: "ZbritjaStudenti",
                column: "StudentiID");

            migrationBuilder.CreateIndex(
                name: "IX_ZbritjaStudenti_Zbritja1ID",
                table: "ZbritjaStudenti",
                column: "Zbritja1ID");

            migrationBuilder.CreateIndex(
                name: "IX_ZbritjaStudenti_Zbritja2ID",
                table: "ZbritjaStudenti",
                column: "Zbritja2ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZbritjaStudenti");

            migrationBuilder.DropTable(
                name: "Zbritjet");
        }
    }
}
