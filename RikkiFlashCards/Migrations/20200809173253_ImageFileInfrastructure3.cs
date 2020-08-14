using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class ImageFileInfrastructure3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ImageFiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ImageFiles");
        }
    }
}
