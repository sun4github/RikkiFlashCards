using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class ImageFileInfrastructure2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "ImageFiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImageFiles_CardId",
                table: "ImageFiles",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_Cards_CardId",
                table: "ImageFiles",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_Cards_CardId",
                table: "ImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_ImageFiles_CardId",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "ImageFiles");
        }
    }
}
