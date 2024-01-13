using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class TeDhenatFillestare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "AspNetRoles",
              columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
              values: new object[,]
              {
            { "00000000-0000-0000-0000-000000000000", "User", "USER", "00000000-0000-0000-0000-000000000000" },
            { "01010101-0101-0101-0101-010101010101", "Admin", "Admin", "01010101-0101-0101-0101-010101010101" },
            { "02020202-0202-0202-0202-020202020202", "Student", "STUDENT", "02020202-0202-0202-0202-020202020202" },
            { "03030303-0303-0303-0303-030303030303", "Profesor", "PROFESOR", "03030303-0303-0303-0303-030303030303" },
            { "04040404-0404-0404-0404-040404040404", "Asistent", "ASISTENT", "04040404-0404-0404-0404-040404040404"},
            { "05050505-0505-0505-0505-050505050505", "Administrat", "ADMINISTRAT", "05050505-0505-0505-0505-050505050505"},
            { "06060606-0606-0606-0606-060606060606", "Financa", "FINANCA", "06060606-0606-0606-0606-060606060606"},
              });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
            "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
            "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
            "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled",
            "LockoutEnd", "LockoutEnabled", "AccessFailedCount"
                },
                values: new object[,]
                {
            {
                "01010101-0101-0101-0101-010101010101", "admin@ubt-uni.net", "ADMIN@UBT-UNI.NET",
                "admin@ubt-uni.net", "ADMIN@UBT-UNI.NET", false,
                "AQAAAAEAACcQAAAAEJlO6MbXUfC+q4JVGrZEjKV6z1dDnA323QebRD85vPQJ3ScQEmBr3P8zKSHwu5Cy2w==",
                "PKJ54KGIZB4MOS3BACM2INRJE54VBB32", "9e6bf9b8-daa7-42b1-9529-f17ee6633b0f",
                null, false, false, null, true, 0
            },

    {
        "06060606-0606-0606-0606-060606060606", "financa.financa@ubt-uni.net","FINANCA.FINANCA@UBT-UNI.NET",
         "financa.financa@ubt-uni.net",  "FINANCA.FINANCA@UBT-UNI.NET", false,
        "AQAAAAEAACcQAAAAEKNmVN4LisbTVJQOg4aLiP7H8OaNIIyDeYClU/jGhqSKQBS/r6IY8eGzIrDtSKhB/Q==",
        "DN7WLKLLLM3ED3HSD4RYBTDT3SBMFQYM", "d3fed7b3-bf67-4fbb-bfb8-b62409fbbfd7",
        null,false,false, null,true, 0
    },
    {
        "04040404-0404-0404-0404-040404040404","asistent.asistent@ubt-uni.net", "ASISTENT.ASISTENT@UBT-UNI.NET",
        "asistent.asistent@ubt-uni.net", "ASISTENT.ASISTENT@UBT-UNI.NET", false,
        "AQAAAAEAACcQAAAAECaLmlSz1riRM0umbVcP8Om4vSASrSeu66jZwkPuRXEVdnvq7FjXvyKVQJ597KYOUA==",
        "FAHVDF45LNGEYVNCZWMRMTLYUDDYN4YC","c6be0c9d-bd6b-47ff-859f-869c2c494d3b",
        null,false,false,null,true, 0
    },
    {
        "02020202-0202-0202-0202-020202020202","ss00002@ubt-uni.net", "SS00002@UBT-UNI.NET",
        "ss00002@ubt-uni.net","SS00002@UBT-UNI.NET", false,
        "AQAAAAEAACcQAAAAEHCdkr7FvSqSGvjTObLD9dJ5+caIeCmkpc9PP0FQ1yLvPy9Fh924E4DFeOB2BbJxaA==",
        "FW7GONDJV4JNJQ3DMF2SPV4Q5URG7MH4", "127a6d7b-afb2-4f50-9272-b263199ec776",
        null, false, false, null, true, 0
    },
    {
        "05050505-0505-0505-0505-050505050505", "administrat.administrat@ubt-uni.net", "ADMINISTRAT.ADMINISTRAT@UBT-UNI.NET",
        "administrat.administrat@ubt-uni.net","ADMINISTRAT.ADMINISTRAT@UBT-UNI.NET",false,
        "AQAAAAEAACcQAAAAECkLz2ymd7xgZ9g+fgS266DKOVh+ajQshqcS8E8MDXhoWYgtvyGgZna+okuEbtXXeA==",
        "BRHQ5LCAIV3IHXT3BI72ZQPFR5RO4HFU","b1d32db2-4368-4ef8-a714-2b474256fcb2",
        null, false, false, null, true, 0
    },
    {
        "03030303-0303-0303-0303-030303030303", "profesor.profesor@ubt-uni.net", "PROFESOR.PROFESOR@UBT-UNI.NET",
        "profesor.profesor@ubt-uni.net", "PROFESOR.PROFESOR@UBT-UNI.NET", false,
        "AQAAAAEAACcQAAAAEJVT1SIDQSEI6xHkco5zq2A/OPJV0gPoKCN6vYp4YQ6nJHfVAamMLsYJbZrkHOPGdA==",
        "5TEJXKT6Q5AQEXEYLHVZUY6AEIJ55PE2", "1ddf6934-9e32-47ba-9b71-c5344ca71f3d",
        null, false, false, null, true, 0
    }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {

            { "01010101-0101-0101-0101-010101010101", "00000000-0000-0000-0000-000000000000" },
            { "02020202-0202-0202-0202-020202020202", "00000000-0000-0000-0000-000000000000" },
            { "03030303-0303-0303-0303-030303030303", "00000000-0000-0000-0000-000000000000" },
            { "04040404-0404-0404-0404-040404040404", "00000000-0000-0000-0000-000000000000" },
            { "05050505-0505-0505-0505-050505050505", "00000000-0000-0000-0000-000000000000" },
            { "06060606-0606-0606-0606-060606060606", "00000000-0000-0000-0000-000000000000" },
            { "01010101-0101-0101-0101-010101010101", "01010101-0101-0101-0101-010101010101" },
            { "02020202-0202-0202-0202-020202020202", "02020202-0202-0202-0202-020202020202" },
            { "03030303-0303-0303-0303-030303030303", "03030303-0303-0303-0303-030303030303" },
            { "04040404-0404-0404-0404-040404040404", "04040404-0404-0404-0404-040404040404" },
            { "05050505-0505-0505-0505-050505050505", "05050505-0505-0505-0505-050505050505" },
            { "06060606-0606-0606-0606-060606060606", "06060606-0606-0606-0606-060606060606" },
                });

            migrationBuilder.InsertData(
                table: "Perdoruesit",
                columns: new[] { "UserID", "Emri", "Mbiemri", "Email", "Username", "AspNetUserId" },
                values: new object[,]
                {
            { 1, "Admin", "Admin", "admin@ubt-uni.net", "admin", "01010101-0101-0101-0101-010101010101" },
            { 2, "Student", "Student", "ss00002@ubt-uni.net", "ss00002", "02020202-0202-0202-0202-020202020202" },
            { 3, "Profesor", "Profesor", "profesor.profesor@ubt-uni.net", "profesor.profesor", "03030303-0303-0303-0303-030303030303" },
            { 4, "Asistent", "Asistent", "asistent.asistent@ubt-uni.net", "asistent.asistent", "04040404-0404-0404-0404-040404040404" },
            { 5, "Administrat", "Administrat", "administrat.administrat@ubt-uni.net", "administrat.administra", "05050505-0505-0505-0505-050505050505" },
            { 6, "Financa", "Financa", "financa.financa@ubt-uni.net", "financa.financa", "06060606-0606-0606-0606-060606060606" }
                });

            migrationBuilder.InsertData(
                table: "TeDhenatPerdoruesit",
                columns: new[] { "TeDhenatID", "NrKontaktit", "Qyteti", "ZipKodi", "Adresa", "Shteti", "UserID", "DataKrijimit", "DataLindjes", "EmailPersonal", "EmriPrindit", "Gjinia", "NrPersonal" },
                values: new object[,]
                {
            { 1, "38344111222", "Prishtine", "10000", "P.A.", "Kosove", 1, "1900-01-01T00:00:00.000Z", "1900-01-01T00:00:00.000Z", "email@gmail.com", "Filani", "M", "1100110011" },
            { 2, "38344111222", "Prishtine", "10000", "P.A.", "Kosove", 2, "1900-01-01T00:00:00.000Z", "1900-01-01T00:00:00.000Z", "email@gmail.com", "Filani", "M", "1100110011" },
            { 3, "38344111222", "Prishtine", "10000", "P.A.", "Kosove", 3, "1900-01-01T00:00:00.000Z", "1900-01-01T00:00:00.000Z", "email@gmail.com", "Filani", "M", "1100110011" },
            { 4, "38344111222", "Prishtine", "10000", "P.A.", "Kosove", 4, "1900-01-01T00:00:00.000Z", "1900-01-01T00:00:00.000Z", "email@gmail.com", "Filani", "M", "1100110011" },
            { 5, "38344111222", "Prishtine", "10000", "P.A.", "Kosove", 5, "1900-01-01T00:00:00.000Z", "1900-01-01T00:00:00.000Z", "email@gmail.com", "Filani", "M", "1100110011" },
            { 6, "38344111222", "Prishtine", "10000", "P.A.", "Kosove", 6, "1900-01-01T00:00:00.000Z", "1900-01-01T00:00:00.000Z", "email@gmail.com", "Filani", "M", "1100110011" }
                });

            migrationBuilder.InsertData(
                table: "Departamentet",
                columns: new[] { "DepartamentiID", "EmriDepartamentit", "ShkurtesaDepartamentit", "DataKrijimit" },
            values: new object[,]
            {
            { 1, "Shkenca Kompjuterike dhe Inxhinieri", "SHKI", "2023-11-30T00:37:25.023Z"},
                });

            migrationBuilder.InsertData(
                table: "Bankat",
                columns: new[] { "BankaID", "EmriBankes", "KodiBankes", "NumriLlogaris", "AdresaBankes", "BicKodi", "SwiftKodi", "Valuta", "IbanFillimi", "LlojiBankes", "DataKrijimit" },
                values: new object[,]
                {
            { 1, "NLB Banka SH. A.", "NLB", "1701010056731578", "Ukshin Hoti nr 124, Prishtinë, Kosovë", null, "NLPRXKPR", "Euro", "XK05", "Hyrese/Dalese", "2023-12-20T11:17:08.323Z"}
                });

            migrationBuilder.InsertData(
                table: "NiveliStudimeve",
                columns: new[] { "NiveliStudimeveID", "EmriNivelitStudimeve", "ShkurtesaEmritNivelitStudimeve", "DataKrijimit" },
                values: new object[,]
                {
            { 1, "Bachelor i Shkencave", "BS/BSc", "2023-11-29T23:32:48.570Z"},
                });

            migrationBuilder.InsertData(
               table: "NiveliStudimitDepartamenti",
               columns: new[] { "NiveliStudimitDepartamentiID", "NiveliStudimitID", "DepartamentiID", "DataKrijimit" },
               values: new object[,]
               {
            { 1, 1,1, "2023-12-11T00:11:19.505Z"}
               });

            migrationBuilder.InsertData(
                table: "Pagesat",
                columns: new[] { "PagesaID", "BankaID", "AplikimiID", "PersoniID", "Pagesa", "Faturimi", "PershkrimiPageses", "LlojiPageses", "DataPageses", "DataKrijimit" },
            values: new object[,]
            {
            { 1, 1, null, 2, "1440", null, "Pagese nga Studenti - Pagesa e Komplet vitit nga Studenti me kod financiar 010100001", "Hyrese", "2023-12-19T23:00:00.000Z", "2023-12-20T11:30:04.685Z"}
        });
            migrationBuilder.InsertData(
               table: "TeDhenatRegjistrimitStudentit",
               columns: new[] { "TDhRSID", "KodiFinanciar", "DepartamentiID", "NiveliStudimitID", "VitiAkademikRegjistrim", "DataRegjistrimit", "LlojiRegjistrimit", "SpecializimiID", "UserId", "IdStudenti" },
               values: new object[,]
               {
            { 1, "010100001",1, 1, "2023/2024", "2023-12-20T11:28:49.762Z", "I Rregullt", null, 2, "232400002" }
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
