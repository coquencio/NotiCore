using Microsoft.EntityFrameworkCore.Migrations;

namespace NotiCore.API.Migrations
{
    public partial class SourceSubscriptionsTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceSubscriptions_Subscribers_SubscriberId",
                table: "SourceSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubcriberId",
                table: "SourceSubscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriberId",
                table: "SourceSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceSubscriptions_Subscribers_SubscriberId",
                table: "SourceSubscriptions",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "SubscriberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceSubscriptions_Subscribers_SubscriberId",
                table: "SourceSubscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriberId",
                table: "SourceSubscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SubcriberId",
                table: "SourceSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceSubscriptions_Subscribers_SubscriberId",
                table: "SourceSubscriptions",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "SubscriberId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
