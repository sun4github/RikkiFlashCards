using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class AddFlashCardUsersFKToResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlashCardUserId",
                table: "Resources",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_FlashCardUserId",
                table: "Resources",
                column: "FlashCardUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_FlashCardUserId",
                table: "Resources",
                column: "FlashCardUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_FlashCardUserId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_FlashCardUserId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "FlashCardUserId",
                table: "Resources");
        }
    }
}
