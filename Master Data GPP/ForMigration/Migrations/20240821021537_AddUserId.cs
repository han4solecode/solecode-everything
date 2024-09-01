using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForMigration.Migrations
{
    /// <inheritdoc />
    public partial class AddUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_LibraryCards_LibraryCardId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LibraryCardId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LibraryCardId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LibraryCards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryCards_UserId",
                table: "LibraryCards",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryCards_Users_UserId",
                table: "LibraryCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryCards_Users_UserId",
                table: "LibraryCards");

            migrationBuilder.DropIndex(
                name: "IX_LibraryCards_UserId",
                table: "LibraryCards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LibraryCards");

            migrationBuilder.AddColumn<int>(
                name: "LibraryCardId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LibraryCardId",
                table: "Users",
                column: "LibraryCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LibraryCards_LibraryCardId",
                table: "Users",
                column: "LibraryCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id");
        }
    }
}
