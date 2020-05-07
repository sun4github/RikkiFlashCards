using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class ResourceIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Resources",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResources",
                table: "Resources",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SearchResources",
                table: "Resources");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Resources",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
