using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class ImportimiDBVjeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Departamentet",
                columns: table => new
                {
                    DepartamentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriDepartamentit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShkurtesaDepartamentit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentet", x => x.DepartamentiID);
                });

            migrationBuilder.CreateTable(
                name: "Lendet",
                columns: table => new
                {
                    LendaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KodiLendes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmriLendes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShkurtesaLendes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriaLendes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KreditELendes = table.Column<int>(type: "int", nullable: true),
                    SemestriLigjerimit = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lendet", x => x.LendaID);
                });

            migrationBuilder.CreateTable(
                name: "LlogaritERejaTeKrijuara",
                columns: table => new
                {
                    LlogariaEReID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerdoruesiID = table.Column<int>(type: "int", nullable: true),
                    AspNetUserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlogaritERejaTeKrijuara", x => x.LlogariaEReID);
                    table.ForeignKey(
                        name: "FK_LlogaritERejaTeKrijuara_AspNetUsers_AspNetUserID",
                        column: x => x.AspNetUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LlogaritERejaTeKrijuara_Perdoruesit_PerdoruesiID",
                        column: x => x.PerdoruesiID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Lokacionet",
                columns: table => new
                {
                    LokacioniID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriLokacionit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShkurtesaLokacionit = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    AdresaLokacionit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QytetiLokacionit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacionet", x => x.LokacioniID);
                });

            migrationBuilder.CreateTable(
                name: "LendetDepartamentiProfesori",
                columns: table => new
                {
                    LDPID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LendaID = table.Column<int>(type: "int", nullable: false),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    ProfesoriID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendetDepartamentiProfesori", x => x.LDPID);
                    table.ForeignKey(
                        name: "FK_LendetDepartamentiProfesori_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_LendetDepartamentiProfesori_Lendet_LendaID",
                        column: x => x.LendaID,
                        principalTable: "Lendet",
                        principalColumn: "LendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LendetDepartamentiProfesori_Perdoruesit_ProfesoriID",
                        column: x => x.ProfesoriID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Sallat",
                columns: table => new
                {
                    SallaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KapacitetiSalles = table.Column<int>(type: "int", nullable: false),
                    LokacioniID = table.Column<int>(type: "int", nullable: false),
                    KodiSalles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sallat", x => x.SallaID);
                    table.ForeignKey(
                        name: "FK_Sallat_Lokacionet_LokacioniID",
                        column: x => x.LokacioniID,
                        principalTable: "Lokacionet",
                        principalColumn: "LokacioniID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LendetDepartamentiProfesori_DepartamentiID",
                table: "LendetDepartamentiProfesori",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_LendetDepartamentiProfesori_LendaID",
                table: "LendetDepartamentiProfesori",
                column: "LendaID");

            migrationBuilder.CreateIndex(
                name: "IX_LendetDepartamentiProfesori_ProfesoriID",
                table: "LendetDepartamentiProfesori",
                column: "ProfesoriID");

            migrationBuilder.CreateIndex(
                name: "IX_LlogaritERejaTeKrijuara_AspNetUserID",
                table: "LlogaritERejaTeKrijuara",
                column: "AspNetUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LlogaritERejaTeKrijuara_PerdoruesiID",
                table: "LlogaritERejaTeKrijuara",
                column: "PerdoruesiID");

            migrationBuilder.CreateIndex(
                name: "IX_Sallat_LokacioniID",
                table: "Sallat",
                column: "LokacioniID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LendetDepartamentiProfesori");

            migrationBuilder.DropTable(
                name: "LlogaritERejaTeKrijuara");

            migrationBuilder.DropTable(
                name: "Sallat");

            migrationBuilder.DropTable(
                name: "Departamentet");

            migrationBuilder.DropTable(
                name: "Lendet");

            migrationBuilder.DropTable(
                name: "Lokacionet");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
