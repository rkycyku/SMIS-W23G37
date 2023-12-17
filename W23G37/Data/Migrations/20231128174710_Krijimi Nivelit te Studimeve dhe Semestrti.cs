using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiNivelitteStudimevedheSemestrti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LlojiStudimit",
                table: "PataqitjaSemestrit");

            migrationBuilder.RenameColumn(
                name: "Semestri",
                table: "PataqitjaSemestrit",
                newName: "SemestriID");

            migrationBuilder.CreateTable(
                name: "NiveliStudimeve",
                columns: table => new
                {
                    NiveliStudimeveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriNivelitStudimeve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShkurtesaEmritNivelitStudimeve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiveliStudimeve", x => x.NiveliStudimeveID);
                });

            migrationBuilder.CreateTable(
                name: "Semestri",
                columns: table => new
                {
                    SemestriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriSemestrit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NiveliStudimeveID = table.Column<int>(type: "int", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semestri", x => x.SemestriID);
                    table.ForeignKey(
                        name: "FK_Semestri_NiveliStudimeve_NiveliStudimeveID",
                        column: x => x.NiveliStudimeveID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PataqitjaSemestrit_SemestriID",
                table: "PataqitjaSemestrit",
                column: "SemestriID");

            migrationBuilder.CreateIndex(
                name: "IX_Semestri_NiveliStudimeveID",
                table: "Semestri",
                column: "NiveliStudimeveID");

            migrationBuilder.AddForeignKey(
                name: "FK_PataqitjaSemestrit_Semestri_SemestriID",
                table: "PataqitjaSemestrit",
                column: "SemestriID",
                principalTable: "Semestri",
                principalColumn: "SemestriID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PataqitjaSemestrit_Semestri_SemestriID",
                table: "PataqitjaSemestrit");

            migrationBuilder.DropTable(
                name: "Semestri");

            migrationBuilder.DropTable(
                name: "NiveliStudimeve");

            migrationBuilder.DropIndex(
                name: "IX_PataqitjaSemestrit_SemestriID",
                table: "PataqitjaSemestrit");

            migrationBuilder.RenameColumn(
                name: "SemestriID",
                table: "PataqitjaSemestrit",
                newName: "Semestri");

            migrationBuilder.AddColumn<string>(
                name: "LlojiStudimit",
                table: "PataqitjaSemestrit",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
