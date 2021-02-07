using Microsoft.EntityFrameworkCore.Migrations;

namespace NotiCore.API.Migrations
{
    public partial class SeedLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "Abbreviation", "Description", "IsActive" },
                values: new object[] { 1, "EN", "English", true });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "Abbreviation", "Description", "IsActive" },
                values: new object[] { 2, "ES", "Spanish", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: 2);
        }
    }
}
