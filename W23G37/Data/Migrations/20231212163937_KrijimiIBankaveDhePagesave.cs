using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiIBankaveDhePagesave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bankat",
                columns: table => new
                {
                    BankaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriBankes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KodiBankes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumriLlogaris = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdresaBankes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BicKodi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwiftKodi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valuta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IbanFillimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlojiBanles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankat", x => x.BankaID);
                });

            migrationBuilder.CreateTable(
                name: "Pagesat",
                columns: table => new
                {
                    PagesaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankaID = table.Column<int>(type: "int", nullable: true),
                    AplikimiID = table.Column<int>(type: "int", nullable: true),
                    PersoniID = table.Column<int>(type: "int", nullable: true),
                    Pagesa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faturimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PershkrimiPageses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlojiPageses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataPageses = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagesat", x => x.PagesaID);
                    table.ForeignKey(
                        name: "FK_Pagesat_AplikimetEReja_AplikimiID",
                        column: x => x.AplikimiID,
                        principalTable: "AplikimetEReja",
                        principalColumn: "AplikimiID");
                    table.ForeignKey(
                        name: "FK_Pagesat_Bankat_BankaID",
                        column: x => x.BankaID,
                        principalTable: "Bankat",
                        principalColumn: "BankaID");
                    table.ForeignKey(
                        name: "FK_Pagesat_Perdoruesit_PersoniID",
                        column: x => x.PersoniID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagesat_AplikimiID",
                table: "Pagesat",
                column: "AplikimiID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagesat_BankaID",
                table: "Pagesat",
                column: "BankaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagesat_PersoniID",
                table: "Pagesat",
                column: "PersoniID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagesat");

            migrationBuilder.DropTable(
                name: "Bankat");
        }
    }
}
