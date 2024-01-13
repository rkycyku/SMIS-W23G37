using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W23G37.Data.Migrations
{
    public partial class InsertimiITeDhenaveFillestareTePerditesuara : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bankat",
                columns: new[] { "BankaID", "EmriBankes", "KodiBankes", "NumriLlogaris", "AdresaBankes", "BicKodi", "SwiftKodi", "Valuta", "IbanFillimi", "LlojiBankes", "DataKrijimit" },
                values: new object[,]
                {
            { 0, "UBT Financat", "FIN", "0", "P. A.", null, null, "Euro", null, "TarifatEStudentit", "2023-12-20T11:17:08.323Z"}
                });

            migrationBuilder.UpdateData(
                table: "Perdoruesit",
                keyColumn: "UserID", 
                keyValue: null, 
                column: "DataKrijimit",
                value: "2023-12-20T11:17:08.323Z");

            migrationBuilder.InsertData(
                table: "Semestri",
                columns: new[] { "SemestriID", "NrSemestrit", "NiveliStudimeveID", "DataKrijimit"},
                values: new object[,]
                {
            { 1, 1, 1, "2023-12-01T18:24:05.431Z"}
                });

            migrationBuilder.InsertData(
                table: "AfatiParaqitjesSemestrit",
                columns: new[] { "APSID", "LlojiSemestrit", "VitiAkademik", "NiveliStudimeveID", "DataFillimitAfatit", "DataMbarimitAfatit", "DataKrijimit" },
                values: new object[,]
                {
            { 1, "veror", "2023/2024", 1, "2024-01-06T00:00:00.000Z", "2024-03-04T00:00:00.000Z", "2024-01-06T23:55:23.483Z"}
                });

            migrationBuilder.InsertData(
                table: "Lokacionet",
                columns: new[] { "LokacioniID", "EmriLokacionit", "ShkurtesaLokacionit", "AdresaLokacionit", "QytetiLokacionit", "DataKrijimit"},
                values: new object[,]
                {
                                { 1, "UBT Prishtinë", "K", "Prishtinë", "Prishtinë", "2022-02-27T00:00:00.000Z"}
                });

            migrationBuilder.InsertData(
                table: "ParaqitjaSemestrit",
                columns: new[] { "ParaqitjaSemestritID", "APSID", "SemestriID", "StudentiID", "LokacioniID", "NderrimiOrarit", "DataParaqitjes"},
                values: new object[,]
                {
                                { 1, 1, 1, 2, 1, "Paradite", "2024-01-07T15:24:05.536Z"}
                });

            migrationBuilder.InsertData(
                table: "Zbritjet",
                columns: new[] { "ZbritjaID", "EmriZbritjes", "Zbritja", "DataKrijimit", "LlojiZbritjes" },
                values: new object[,]
                {
                                { 1, "Pagesa e Vitit ne Tersi", 5, "2023-12-26T20:00:35.710Z", "Extra"},
                                { 2, "Vijueshmeri ne Trajnime", 15, "2023-12-29T00:41:03.863Z", "3 Vjeqare"}
                });

            migrationBuilder.InsertData(
                table: "TarifaStudenti",
                columns: new[] { "ZbritjaStudentiID", "StudentiID", "Zbritja1ID", "Zbritja2ID", "DataKrijimit", "TarifaFikse", "TarifaStudimitID" },
                values: new object[,]
                {
                                { 0, 2, 2, 1, "2024-01-07T19:24:49.517Z", 1800, null}
                });

            migrationBuilder.InsertData(
                table: "TarifatDepartamenti",
                columns: new[] { "TarifaID", "DepartamentiID", "NiveliStudimitID", "TarifaVjetore", "DataKrijimit"},
                values: new object[,]
                {
                                { 1, 1, 1, 1800, "2023-12-11T00:11:19.505Z"}
                });

            migrationBuilder.UpdateData(
                table: "TeDhenatRegjistrimitStudentit",
                keyColumn: "TDhRSID",
                keyValue: 1, 
                column: "LlojiKontrates",
                value: 3);

            migrationBuilder.InsertData(
                table: "LokacionetDepartamenti",
                columns: new[] { "LokacioniDepartamentiID", "LokacioniID", "DepartamentiID", "DataKrijimit"},
                values: new object[,]
                {
                                { 1, 1, 1, "2024-01-13T20:33:05.911Z"}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
