using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class AddedFrontBackIndexA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Cards_Front_Back",
                table: "Cards",
                newName: "IX_SearchCards");

            migrationBuilder.AlterColumn<string>(
                name: "Front",
                table: "Cards",
                type: "VARCHAR(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "Back",
                table: "Cards",
                type: "VARCHAR(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldMaxLength: 5000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_SearchCards",
                table: "Cards",
                newName: "IX_Cards_Front_Back");

            migrationBuilder.AlterColumn<string>(
                name: "Front",
                table: "Cards",
                type: "longtext CHARACTER SET utf8mb4",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "Back",
                table: "Cards",
                type: "longtext CHARACTER SET utf8mb4",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldMaxLength: 5000);
        }
    }
}
