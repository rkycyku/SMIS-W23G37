using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class PerditesimeTekParaqitjaProvimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataVendosjesSeNotes",
                table: "ParaqitjaProvimit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusiINotes",
                table: "ParaqitjaProvimit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVendosjesSeNotes",
                table: "NotatStudenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFunditShfaqjesProvimeve",
                table: "AfatiParaqitjesProvimit",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataVendosjesSeNotes",
                table: "ParaqitjaProvimit");

            migrationBuilder.DropColumn(
                name: "StatusiINotes",
                table: "ParaqitjaProvimit");

            migrationBuilder.DropColumn(
                name: "DataVendosjesSeNotes",
                table: "NotatStudenti");

            migrationBuilder.DropColumn(
                name: "DataFunditShfaqjesProvimeve",
                table: "AfatiParaqitjesProvimit");
        }
    }
}
