using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimiNotatStudentiti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataParaqitjes",
                table: "NotatStudenti");

            migrationBuilder.DropColumn(
                name: "StatusiINotes",
                table: "NotatStudenti");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataParaqitjes",
                table: "NotatStudenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusiINotes",
                table: "NotatStudenti",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
