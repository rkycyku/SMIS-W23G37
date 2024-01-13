using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiIVendosjesSeNotave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nota",
                table: "ParaqitjaSemestrit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NotatStudenti",
                columns: table => new
                {
                    NotaStudentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LendaID = table.Column<int>(type: "int", nullable: true),
                    StudentiID = table.Column<int>(type: "int", nullable: true),
                    ParaqitjaProvimitID = table.Column<int>(type: "int", nullable: true),
                    StatusiINotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataParaqitjes = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotatStudenti", x => x.NotaStudentiID);
                    table.ForeignKey(
                        name: "FK_NotatStudenti_Lendet_LendaID",
                        column: x => x.LendaID,
                        principalTable: "Lendet",
                        principalColumn: "LendaID");
                    table.ForeignKey(
                        name: "FK_NotatStudenti_ParaqitjaProvimit_ParaqitjaProvimitID",
                        column: x => x.ParaqitjaProvimitID,
                        principalTable: "ParaqitjaProvimit",
                        principalColumn: "ParaqitjaProvimitID");
                    table.ForeignKey(
                        name: "FK_NotatStudenti_Perdoruesit_StudentiID",
                        column: x => x.StudentiID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotatStudenti_LendaID",
                table: "NotatStudenti",
                column: "LendaID");

            migrationBuilder.CreateIndex(
                name: "IX_NotatStudenti_ParaqitjaProvimitID",
                table: "NotatStudenti",
                column: "ParaqitjaProvimitID");

            migrationBuilder.CreateIndex(
                name: "IX_NotatStudenti_StudentiID",
                table: "NotatStudenti",
                column: "StudentiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotatStudenti");

            migrationBuilder.DropColumn(
                name: "Nota",
                table: "ParaqitjaSemestrit");

        }
    }
}
