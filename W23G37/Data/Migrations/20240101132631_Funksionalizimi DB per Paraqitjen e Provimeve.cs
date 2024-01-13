using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class FunksionalizimiDBperParaqitjeneProvimeve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParaqitjaSemestrit_StudentiID",
                table: "ParaqitjaSemestrit");

            migrationBuilder.CreateTable(
                name: "AfatiParaqitjesProvimit",
                columns: table => new
                {
                    APPID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlojiAfatit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VitiAkademik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFillimitAfatit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataMbarimitAfatit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfatiParaqitjesProvimit", x => x.APPID);
                });

            migrationBuilder.CreateTable(
                name: "ParaqitjaProvimit",
                columns: table => new
                {
                    ParaqitjaProvimitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APPID = table.Column<int>(type: "int", nullable: true),
                    LDPID = table.Column<int>(type: "int", nullable: true),
                    StudentiID = table.Column<int>(type: "int", nullable: true),
                    DataParaqitjes = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParaqitjaProvimit", x => x.ParaqitjaProvimitID);
                    table.ForeignKey(
                        name: "FK_ParaqitjaProvimit_AfatiParaqitjesProvimit_APPID",
                        column: x => x.APPID,
                        principalTable: "AfatiParaqitjesProvimit",
                        principalColumn: "APPID");
                    table.ForeignKey(
                        name: "FK_ParaqitjaProvimit_LendetDepartamentiProfesori_LDPID",
                        column: x => x.LDPID,
                        principalTable: "LendetDepartamentiProfesori",
                        principalColumn: "LDPID");
                    table.ForeignKey(
                        name: "FK_ParaqitjaProvimit_Perdoruesit_StudentiID",
                        column: x => x.StudentiID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaSemestrit_StudentiID",
                table: "ParaqitjaSemestrit",
                column: "StudentiID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaProvimit_APPID",
                table: "ParaqitjaProvimit",
                column: "APPID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaProvimit_LDPID",
                table: "ParaqitjaProvimit",
                column: "LDPID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaProvimit_StudentiID",
                table: "ParaqitjaProvimit",
                column: "StudentiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParaqitjaProvimit");

            migrationBuilder.DropTable(
                name: "AfatiParaqitjesProvimit");

            migrationBuilder.DropIndex(
                name: "IX_ParaqitjaSemestrit_StudentiID",
                table: "ParaqitjaSemestrit");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaSemestrit_StudentiID",
                table: "ParaqitjaSemestrit",
                column: "StudentiID");
        }
    }
}
