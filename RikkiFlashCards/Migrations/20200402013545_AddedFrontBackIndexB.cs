using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class AddedFrontBackIndexB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Front",
                table: "Cards",
                type: "TEXT",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "Back",
                table: "Cards",
                type: "TEXT",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldMaxLength: 5000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Front",
                table: "Cards",
                type: "VARCHAR(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "Back",
                table: "Cards",
                type: "VARCHAR(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 5000);
        }
    }
}
