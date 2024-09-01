using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForMigration.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoleToPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Position");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Users",
                newName: "Role");
        }
    }
}
