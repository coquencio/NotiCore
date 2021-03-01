using Microsoft.EntityFrameworkCore.Migrations;

namespace NotiCore.API.Migrations
{
    public partial class TopicDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "IsActive",
                value: false);

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "TopicId", "Description", "IsActive" },
                values: new object[] { 14, "Other", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 14);

            migrationBuilder.UpdateData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 13,
                column: "IsActive",
                value: true);
        }
    }
}
