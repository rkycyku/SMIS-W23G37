using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class KrijimiLidhjesmesLokacionitdheDepartamentit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LokacionetDepartamenti",
                columns: table => new
                {
                    LokacioniDepartamentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LokacioniID = table.Column<int>(type: "int", nullable: false),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LokacionetDepartamenti", x => x.LokacioniDepartamentiID);
                    table.ForeignKey(
                        name: "FK_LokacionetDepartamenti_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_LokacionetDepartamenti_Lokacionet_LokacioniID",
                        column: x => x.LokacioniID,
                        principalTable: "Lokacionet",
                        principalColumn: "LokacioniID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LokacionetDepartamenti_DepartamentiID",
                table: "LokacionetDepartamenti",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_LokacionetDepartamenti_LokacioniID",
                table: "LokacionetDepartamenti",
                column: "LokacioniID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LokacionetDepartamenti");
        }
    }
}
