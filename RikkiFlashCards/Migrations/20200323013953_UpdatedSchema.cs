using Microsoft.EntityFrameworkCore.Migrations;

namespace AnkiFlashCards.Migrations
{
    public partial class UpdatedSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Deck_DeckId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Deck_Resource_ResourceId",
                table: "Deck");

            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Subjects_SubjectId",
                table: "Resource");

            migrationBuilder.DropForeignKey(
                name: "FK_Revision_Deck_DeckId",
                table: "Revision");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Revision",
                table: "Revision");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resource",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deck",
                table: "Deck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.RenameTable(
                name: "Revision",
                newName: "Revisions");

            migrationBuilder.RenameTable(
                name: "Resource",
                newName: "Resources");

            migrationBuilder.RenameTable(
                name: "Deck",
                newName: "Decks");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameIndex(
                name: "IX_Revision_DeckId",
                table: "Revisions",
                newName: "IX_Revisions_DeckId");

            migrationBuilder.RenameIndex(
                name: "IX_Resource_SubjectId",
                table: "Resources",
                newName: "IX_Resources_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Deck_ResourceId",
                table: "Decks",
                newName: "IX_Decks_ResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_DeckId",
                table: "Cards",
                newName: "IX_Cards_DeckId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Revisions",
                table: "Revisions",
                column: "RevisionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decks",
                table: "Decks",
                column: "DeckId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Decks_DeckId",
                table: "Cards",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "DeckId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Resources_ResourceId",
                table: "Decks",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "ResourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Subjects_SubjectId",
                table: "Resources",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Decks_DeckId",
                table: "Revisions",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "DeckId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Decks_DeckId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Resources_ResourceId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Subjects_SubjectId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Decks_DeckId",
                table: "Revisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Revisions",
                table: "Revisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decks",
                table: "Decks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "Revisions",
                newName: "Revision");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "Resource");

            migrationBuilder.RenameTable(
                name: "Decks",
                newName: "Deck");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameIndex(
                name: "IX_Revisions_DeckId",
                table: "Revision",
                newName: "IX_Revision_DeckId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_SubjectId",
                table: "Resource",
                newName: "IX_Resource_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Decks_ResourceId",
                table: "Deck",
                newName: "IX_Deck_ResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_DeckId",
                table: "Card",
                newName: "IX_Card_DeckId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Revision",
                table: "Revision",
                column: "RevisionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resource",
                table: "Resource",
                column: "ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deck",
                table: "Deck",
                column: "DeckId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Deck_DeckId",
                table: "Card",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "DeckId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_Resource_ResourceId",
                table: "Deck",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "ResourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Subjects_SubjectId",
                table: "Resource",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revision_Deck_DeckId",
                table: "Revision",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "DeckId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
