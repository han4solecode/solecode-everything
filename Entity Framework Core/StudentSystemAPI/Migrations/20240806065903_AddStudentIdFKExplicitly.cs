using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdFKExplicitly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_Students_StudentId",
                table: "Hobbies");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Hobbies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_Students_StudentId",
                table: "Hobbies",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_Students_StudentId",
                table: "Hobbies");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Hobbies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_Students_StudentId",
                table: "Hobbies",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
