using Microsoft.EntityFrameworkCore.Migrations;

namespace NotiCore.API.Migrations
{
    public partial class SeedSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "IsActive", "LanguageId", "Name", "Url" },
                values: new object[,]
                {
                    { 1, true, 1, "BBC UK", "https://www.bbc.co.uk/" },
                    { 55, true, 2, "Proceso", "https://www.proceso.com.mx/" },
                    { 54, true, 2, "Diario MX", "https://www.diario.mx/" },
                    { 53, true, 2, "Milenio", "https://www.milenio.com/" },
                    { 52, true, 2, "La Jornada", "https://www.jornada.com.mx/" },
                    { 51, true, 2, "La Razón", "https://www.razon.com.mx/" },
                    { 50, true, 1, "Mexico News Daily", "https://www.mexiconewsdaily.com/" },
                    { 49, true, 2, "El Siglo de Torreón", "https://www.elsiglodetorreon.com.mx/" },
                    { 48, true, 2, "Vanguardia", "https://www.vanguardia.com.mx/" },
                    { 47, true, 2, "Reforma", "https://www.reforma.com/" },
                    { 46, true, 2, "Excelsior", "https://www.excelsior.com.mx/" },
                    { 45, true, 2, "El Debate", "https://www.debate.com.mx/" },
                    { 44, true, 2, "El Universal", "https://www.eluniversal.com.mx/" },
                    { 43, true, 1, "South China Morning Post", "https://www.scmp.com/" },
                    { 42, true, 1, "France 24", "https://www.france24.com/en/" },
                    { 41, true, 1, "The Sydney Morning Herald", "https://www.smh.com.au/" },
                    { 56, true, 2, "El Financiero", "https://www.elfinanciero.com.mx/" },
                    { 57, true, 2, "SDP Noticias", "https://www.sdpnoticias.com/" },
                    { 58, true, 2, "Sin Embargo", "https://www.sinembargo.mx/" },
                    { 59, true, 2, "Publimetro", "https://www.publimetro.com.mx/mx/" },
                    { 75, true, 1, "NAI México", "http://www.naimexico.com/news/" },
                    { 74, true, 2, "Novedades Campeche", "https://www.novedadescampeche.com.mx/" },
                    { 73, true, 2, "Grupo Metropoli", "https://www.grupometropoli.net/" },
                    { 72, true, 2, "Capital Querétaro", "https://www.capitalqueretaro.com.mx/" },
                    { 71, true, 2, "Expreso Chiapas", "http://www.expresochiapas.com/" },
                    { 70, true, 2, "8 Columnas", "https://www.8columnas.com.mx/" },
                    { 69, true, 2, "Diario Plaza Juárez", "https://www.plazajuarez.mx/" },
                    { 40, true, 1, "Global News", "https://www.globalnews.ca/" },
                    { 68, true, 2, "Quequi", "https://www.quequi.com.mx/" },
                    { 66, true, 1, "The Yucatán Times", "https://www.theyucatantimes.com/" },
                    { 65, true, 2, "Diario Basta Hidalgo", "https://www.diariobasta.com/" },
                    { 64, true, 2, "El Imparcial Oaxaca", "https://imparcialoaxaca.mx/" },
                    { 63, true, 2, "24 Horas México", "https://www.24-horas.mx/" },
                    { 62, true, 2, "El Informador Jalisco", "https://www.informador.mx/" },
                    { 61, true, 2, "Radio Fórmula", "https://www.radioformula.com.mx/" },
                    { 60, true, 2, "El Norte", "https://www.elnorte.com/" },
                    { 67, true, 2, "Precencia Veracruz", "https://www.presencia.mx/default.aspx/" },
                    { 76, true, 2, "Gizmodo en español", "https://www.es.gizmodo.com/" },
                    { 39, true, 1, "Channel News Asia", "https://www.channelnewsasia.com/" },
                    { 37, true, 1, "Vox", "https://www.vox.com/" },
                    { 16, true, 1, "Businessinsider", "https://www.businessinsider.com/" },
                    { 15, true, 1, "NY Post", "https://www.nypost.com/" }
                });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "SourceId", "IsActive", "LanguageId", "Name", "Url" },
                values: new object[,]
                {
                    { 14, true, 1, "Buzzfeed", "https://www.buzzfeed.com/us/" },
                    { 13, true, 1, "USA Today", "https://www.usatoday.com/" },
                    { 12, true, 1, "Express UK", "https://www.express.co.uk/" },
                    { 11, true, 1, "CNBC", "https://www.cnbc.com/" },
                    { 10, true, 1, "Washington Post", "https://www.washingtonpost.com/" },
                    { 9, true, 1, "Yahoo Finance", "https://www.finance.yahoo.com/" },
                    { 8, true, 1, "Fox News", "https://www.foxnews.com/" },
                    { 7, true, 1, "The Guardian", "https://www.theguardian.com/" },
                    { 6, true, 1, "Daily Mail", "https://www.dailymail.co.uk/" },
                    { 5, true, 1, "NY Times", "https://www.nytimes.com/" },
                    { 4, true, 1, "CNN", "https://www.cnn.com/" },
                    { 3, true, 1, "Edition CNN", "https://www.edition.cnn.com/" },
                    { 2, true, 1, "BBC US", "https://www.bbc.com/" },
                    { 17, true, 1, "Forbes", "https://www.forbes.com/" },
                    { 18, true, 1, "ABC AU", "https://www.abc.net.au/" },
                    { 19, true, 1, "Mirror UK", "https://www.mirror.co.uk/" },
                    { 20, true, 1, "TechCrunch", "https://www.techcrunch.com/" },
                    { 36, true, 1, "Euro News", "https://www.euronews.com/" },
                    { 35, true, 1, "The Sun", "https://www.thesun.co.uk/" },
                    { 34, true, 1, "Time", "https://www.time.com/" },
                    { 33, true, 1, "LA Times", "https://www.latimes.com/" },
                    { 32, true, 1, "CBC", "https://www.cbc.ca/" },
                    { 31, true, 1, "The Independent", "https://www.independent.co.uk/" },
                    { 30, true, 1, "Sputnik", "https://www.sputniknews.com/" },
                    { 38, true, 1, "The Seattle Times", "https://www.seattletimes.com/" },
                    { 29, true, 1, "CBS", "https://www.cbsnews.com/" },
                    { 27, true, 1, "National Public Radio", "https://www.npr.org/" },
                    { 26, true, 1, "RT World News", "https://www.rt.com/news/" },
                    { 25, true, 1, "World News Super Fast", "https://www.worldnewssuperfast.blogspot.com/" },
                    { 24, true, 1, "The Cipher Brief", "https://www.thecipherbrief.com/" },
                    { 23, true, 1, "Global Issues", "https://www.globalissues.org/" },
                    { 22, true, 1, "Defence blog", "https://www.defence-blog.com/" },
                    { 21, true, 1, "Al Jazeera", "https://www.aljazeera.com/" },
                    { 28, true, 1, "ABC News", "https://www.abcnews.go.com/" },
                    { 77, true, 1, "Gizmodo", "https://www.gizmodo.com/" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "SourceId",
                keyValue: 77);
        }
    }
}
