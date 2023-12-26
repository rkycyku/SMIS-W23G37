using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class DatabazaERe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                    LlojiBankes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankat", x => x.BankaID);
                });

            migrationBuilder.CreateTable(
                name: "Departamentet",
                columns: table => new
                {
                    DepartamentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriDepartamentit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShkurtesaDepartamentit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    SemestriLigjerimit = table.Column<int>(type: "int", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lendet", x => x.LendaID);
                });

            migrationBuilder.CreateTable(
                name: "Lokacionet",
                columns: table => new
                {
                    LokacioniID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriLokacionit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShkurtesaLokacionit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdresaLokacionit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QytetiLokacionit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacionet", x => x.LokacioniID);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Perdoruesit",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspNetUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perdoruesit", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Perdoruesit_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "LokacionetDepartamenti",
                columns: table => new
                {
                    LokacioniDepartamentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LokacioniID = table.Column<int>(type: "int", nullable: false),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Sallat",
                columns: table => new
                {
                    SallaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KapacitetiSalles = table.Column<int>(type: "int", nullable: false),
                    LokacioniID = table.Column<int>(type: "int", nullable: false),
                    KodiSalles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "AfatiParaqitjesSemestrit",
                columns: table => new
                {
                    APSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlojiSemestrit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VitiAkademik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NiveliStudimeveID = table.Column<int>(type: "int", nullable: true),
                    DataFillimitAfatit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataMbarimitAfatit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfatiParaqitjesSemestrit", x => x.APSID);
                    table.ForeignKey(
                        name: "FK_AfatiParaqitjesSemestrit_NiveliStudimeve_NiveliStudimeveID",
                        column: x => x.NiveliStudimeveID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                });

            migrationBuilder.CreateTable(
                name: "AplikimetEReja",
                columns: table => new
                {
                    AplikimiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmriPrindit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrKontaktit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qyteti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipKodi = table.Column<int>(type: "int", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shteti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gjinia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataLindjes = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KodiFinanciar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    NiveliStudimitID = table.Column<int>(type: "int", nullable: true),
                    VitiAkademikRegjistrim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlojiRegjistrimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegjistrimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplikimetEReja", x => x.AplikimiID);
                    table.ForeignKey(
                        name: "FK_AplikimetEReja_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_AplikimetEReja_NiveliStudimeve_NiveliStudimitID",
                        column: x => x.NiveliStudimitID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                });

            migrationBuilder.CreateTable(
                name: "NiveliStudimitDepartamenti",
                columns: table => new
                {
                    NiveliStudimitDepartamentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveliStudimitID = table.Column<int>(type: "int", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: false),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiveliStudimitDepartamenti", x => x.NiveliStudimitDepartamentiID);
                    table.ForeignKey(
                        name: "FK_NiveliStudimitDepartamenti_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NiveliStudimitDepartamenti_NiveliStudimeve_NiveliStudimitID",
                        column: x => x.NiveliStudimitID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                });

            migrationBuilder.CreateTable(
                name: "Semestri",
                columns: table => new
                {
                    SemestriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrSemestrit = table.Column<int>(type: "int", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "LendetDepartamentiProfesori",
                columns: table => new
                {
                    LDPID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LendaID = table.Column<int>(type: "int", nullable: false),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    ProfesoriID = table.Column<int>(type: "int", nullable: true),
                    Pozita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "LlogaritERejaTeKrijuara",
                columns: table => new
                {
                    LlogariaEReID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerdoruesiID = table.Column<int>(type: "int", nullable: true),
                    AspNetUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "TeDhenatPerdoruesit",
                columns: table => new
                {
                    TeDhenatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmriPrindit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrKontaktit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qyteti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipKodi = table.Column<int>(type: "int", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shteti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gjinia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataLindjes = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeDhenatPerdoruesit", x => x.TeDhenatID);
                    table.ForeignKey(
                        name: "FK_TeDhenatPerdoruesit_Perdoruesit_UserID",
                        column: x => x.UserID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeDhenatRegjistrimitStudentit",
                columns: table => new
                {
                    TDhRSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KodiFinanciar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    NiveliStudimitID = table.Column<int>(type: "int", nullable: true),
                    VitiAkademikRegjistrim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegjistrimit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LlojiRegjistrimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecializimiID = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IdStudenti = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeDhenatRegjistrimitStudentit", x => x.TDhRSID);
                    table.ForeignKey(
                        name: "FK_TeDhenatRegjistrimitStudentit_Departamentet_DepartamentiID",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamentet",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK_TeDhenatRegjistrimitStudentit_NiveliStudimeve_NiveliStudimitID",
                        column: x => x.NiveliStudimitID,
                        principalTable: "NiveliStudimeve",
                        principalColumn: "NiveliStudimeveID");
                    table.ForeignKey(
                        name: "FK_TeDhenatRegjistrimitStudentit_Perdoruesit_UserId",
                        column: x => x.UserId,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeDhenatRegjistrimitStudentit_SpecializimetPerDepartament_SpecializimiID",
                        column: x => x.SpecializimiID,
                        principalTable: "SpecializimetPerDepartament",
                        principalColumn: "SpecializimiID");
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

            migrationBuilder.CreateTable(
                name: "ParaqitjaSemestrit",
                columns: table => new
                {
                    ParaqitjaSemestritID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APSID = table.Column<int>(type: "int", nullable: true),
                    SemestriID = table.Column<int>(type: "int", nullable: true),
                    StudentiID = table.Column<int>(type: "int", nullable: true),
                    LokacioniID = table.Column<int>(type: "int", nullable: true),
                    NderrimiOrarit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataParaqitjes = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParaqitjaSemestrit", x => x.ParaqitjaSemestritID);
                    table.ForeignKey(
                        name: "FK_ParaqitjaSemestrit_AfatiParaqitjesSemestrit_APSID",
                        column: x => x.APSID,
                        principalTable: "AfatiParaqitjesSemestrit",
                        principalColumn: "APSID");
                    table.ForeignKey(
                        name: "FK_ParaqitjaSemestrit_Lokacionet_LokacioniID",
                        column: x => x.LokacioniID,
                        principalTable: "Lokacionet",
                        principalColumn: "LokacioniID");
                    table.ForeignKey(
                        name: "FK_ParaqitjaSemestrit_Perdoruesit_StudentiID",
                        column: x => x.StudentiID,
                        principalTable: "Perdoruesit",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_ParaqitjaSemestrit_Semestri_SemestriID",
                        column: x => x.SemestriID,
                        principalTable: "Semestri",
                        principalColumn: "SemestriID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AfatiParaqitjesSemestrit_NiveliStudimeveID",
                table: "AfatiParaqitjesSemestrit",
                column: "NiveliStudimeveID");

            migrationBuilder.CreateIndex(
                name: "IX_AplikimetEReja_DepartamentiID",
                table: "AplikimetEReja",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_AplikimetEReja_NiveliStudimitID",
                table: "AplikimetEReja",
                column: "NiveliStudimitID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_LokacionetDepartamenti_DepartamentiID",
                table: "LokacionetDepartamenti",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_LokacionetDepartamenti_LokacioniID",
                table: "LokacionetDepartamenti",
                column: "LokacioniID");

            migrationBuilder.CreateIndex(
                name: "IX_NiveliStudimitDepartamenti_DepartamentiID",
                table: "NiveliStudimitDepartamenti",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_NiveliStudimitDepartamenti_NiveliStudimitID",
                table: "NiveliStudimitDepartamenti",
                column: "NiveliStudimitID");

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

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaSemestrit_APSID",
                table: "ParaqitjaSemestrit",
                column: "APSID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaSemestrit_LokacioniID",
                table: "ParaqitjaSemestrit",
                column: "LokacioniID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaSemestrit_SemestriID",
                table: "ParaqitjaSemestrit",
                column: "SemestriID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaSemestrit_StudentiID",
                table: "ParaqitjaSemestrit",
                column: "StudentiID");

            migrationBuilder.CreateIndex(
                name: "IX_Perdoruesit_AspNetUserId",
                table: "Perdoruesit",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sallat_LokacioniID",
                table: "Sallat",
                column: "LokacioniID");

            migrationBuilder.CreateIndex(
                name: "IX_Semestri_NiveliStudimeveID",
                table: "Semestri",
                column: "NiveliStudimeveID");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializimetPerDepartament_DepartamentiID",
                table: "SpecializimetPerDepartament",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatPerdoruesit_UserID",
                table: "TeDhenatPerdoruesit",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_DepartamentiID",
                table: "TeDhenatRegjistrimitStudentit",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_NiveliStudimitID",
                table: "TeDhenatRegjistrimitStudentit",
                column: "NiveliStudimitID");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_SpecializimiID",
                table: "TeDhenatRegjistrimitStudentit",
                column: "SpecializimiID");

            migrationBuilder.CreateIndex(
                name: "IX_TeDhenatRegjistrimitStudentit_UserId",
                table: "TeDhenatRegjistrimitStudentit",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LendetDepartamentiProfesori");

            migrationBuilder.DropTable(
                name: "LlogaritERejaTeKrijuara");

            migrationBuilder.DropTable(
                name: "LokacionetDepartamenti");

            migrationBuilder.DropTable(
                name: "NiveliStudimitDepartamenti");

            migrationBuilder.DropTable(
                name: "Pagesat");

            migrationBuilder.DropTable(
                name: "ParaqitjaSemestrit");

            migrationBuilder.DropTable(
                name: "Sallat");

            migrationBuilder.DropTable(
                name: "TeDhenatPerdoruesit");

            migrationBuilder.DropTable(
                name: "TeDhenatRegjistrimitStudentit");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Lendet");

            migrationBuilder.DropTable(
                name: "AplikimetEReja");

            migrationBuilder.DropTable(
                name: "Bankat");

            migrationBuilder.DropTable(
                name: "AfatiParaqitjesSemestrit");

            migrationBuilder.DropTable(
                name: "Semestri");

            migrationBuilder.DropTable(
                name: "Lokacionet");

            migrationBuilder.DropTable(
                name: "Perdoruesit");

            migrationBuilder.DropTable(
                name: "SpecializimetPerDepartament");

            migrationBuilder.DropTable(
                name: "NiveliStudimeve");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departamentet");
        }
    }
}
