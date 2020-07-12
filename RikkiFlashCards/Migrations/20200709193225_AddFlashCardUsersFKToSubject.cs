using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class AddFlashCardUsersFKToSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlashCardUserId",
                table: "Subjects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FlashCardUserId",
                table: "Subjects",
                column: "FlashCardUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_FlashCardUserId",
                table: "Subjects",
                column: "FlashCardUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_FlashCardUserId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_FlashCardUserId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "FlashCardUserId",
                table: "Subjects");
        }
    }
}
