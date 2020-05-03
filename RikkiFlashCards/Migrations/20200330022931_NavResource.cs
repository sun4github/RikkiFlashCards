using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class NavResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Subjects_SubjectId",
                table: "Resources");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Subjects_SubjectId",
                table: "Resources",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Subjects_SubjectId",
                table: "Resources");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Subjects_SubjectId",
                table: "Resources",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
