using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class InsertimiiTeDhenaveFillestare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
            { "00000000-0000-0000-0000-000000000000", "Admin", "ADMIN", "00000000-0000-0000-0000-000000000000" },
            { "01010101-0101-0101-0101-010101010101", "User", "USER", "01010101-0101-0101-0101-010101010101" }
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
                "00000000-0000-0000-0000-000000000000", "admin@ubt-uni.net", "ADMIN@UBT-UNI.NET",
                "admin@ubt-uni.net", "ADMIN@UBT-UNI.NET", false,
                "AQAAAAEAACcQAAAAEJlO6MbXUfC+q4JVGrZEjKV6z1dDnA323QebRD85vPQJ3ScQEmBr3P8zKSHwu5Cy2w==",
                "PKJ54KGIZB4MOS3BACM2INRJE54VBB32", "9e6bf9b8-daa7-42b1-9529-f17ee6633b0f",
                null, false, false, null, true, 0
            }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
            { "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000" },
            { "00000000-0000-0000-0000-000000000000", "01010101-0101-0101-0101-010101010101" }
                });

            migrationBuilder.InsertData(
                table: "Perdoruesit",
                columns: new[] { "UserID", "Emri", "Mbiemri", "Email", "Username", "AspNetUserId" },
                values: new object[,]
                {
            { 0, "Admin", "Admin", "admin@ubt-uni.net", "admin", "00000000-0000-0000-0000-000000000000" }
                });

            migrationBuilder.InsertData(
                table: "TeDhenatPerdoruesit",
                columns: new[] { "TeDhenatID", "NrKontaktit", "Qyteti", "ZipKodi", "Adresa", "Shteti", "UserID" },
                values: new object[,]
                {
            { 0, "38344111222", "Prishtine", "10000", "P.A.", "Kosove", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

    }
}
