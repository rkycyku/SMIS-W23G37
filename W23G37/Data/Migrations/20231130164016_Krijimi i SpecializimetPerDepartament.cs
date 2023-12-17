using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiiSpecializimetPerDepartament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecializimetPerDepartament",
                columns: table => new
                {
                    SpecializimiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriSpecializimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    DataKrijimt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecializimetPerDepartament", x => x.SpecializimiID);
                    table.ForeignKey(
                        name: "FK_SpecializimetPerDepartament_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecializimetPerDepartament_DepartamentiID",
                table: "SpecializimetPerDepartament",
                column: "DepartamentiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecializimetPerDepartament");
        }
    }
}
