using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class AddedIsExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomOrderList",
                table: "Revisions");

            migrationBuilder.AddColumn<string>(
                name: "CardOrderList",
                table: "Revisions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExam",
                table: "Revisions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardOrderList",
                table: "Revisions");

            migrationBuilder.DropColumn(
                name: "IsExam",
                table: "Revisions");

            migrationBuilder.AddColumn<string>(
                name: "RandomOrderList",
                table: "Revisions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
