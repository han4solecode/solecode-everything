using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddHobbiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobby",
                table: "Hobby");

            migrationBuilder.RenameTable(
                name: "Hobby",
                newName: "Hobbies");

            migrationBuilder.RenameIndex(
                name: "IX_Hobby_StudentId",
                table: "Hobbies",
                newName: "IX_Hobbies_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies",
                column: "HobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_Students_StudentId",
                table: "Hobbies",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_Students_StudentId",
                table: "Hobbies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies");

            migrationBuilder.RenameTable(
                name: "Hobbies",
                newName: "Hobby");

            migrationBuilder.RenameIndex(
                name: "IX_Hobbies_StudentId",
                table: "Hobby",
                newName: "IX_Hobby_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobby",
                table: "Hobby",
                column: "HobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
