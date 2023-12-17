using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimiiTabelave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "TeDhenatPerdoruesit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "Sallat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "Perdoruesit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "LokacionetDepartamenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "LlogaritERejaTeKrijuara",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "LendetDepartamentiProfesori",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "Lendet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataKrijimit",
                table: "Departamentet",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "TeDhenatPerdoruesit");

            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "Sallat");

            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "Perdoruesit");

            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "LokacionetDepartamenti");

            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "LlogaritERejaTeKrijuara");

            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "LendetDepartamentiProfesori");

            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "Lendet");

            migrationBuilder.DropColumn(
                name: "DataKrijimit",
                table: "Departamentet");
        }
    }
}
