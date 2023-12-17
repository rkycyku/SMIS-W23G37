using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiAPS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AfatiParaqitjesSemestrit",
                columns: table => new
                {
                    APSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriAPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemestriID = table.Column<int>(type: "int", nullable: true),
                    DataFillimitAfatit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataMbarimitAfatit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfatiParaqitjesSemestrit", x => x.APSID);
                    table.ForeignKey(
                        name: "FK_AfatiParaqitjesSemestrit_Semestri_SemestriID",
                        column: x => x.SemestriID,
                        principalTable: "Semestri",
                        principalColumn: "SemestriID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AfatiParaqitjesSemestrit_SemestriID",
                table: "AfatiParaqitjesSemestrit",
                column: "SemestriID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfatiParaqitjesSemestrit");
        }
    }
}
