using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAppRoleColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_AppRoleId",
                table: "WorkflowSequences");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowSequences_AppRoleId",
                table: "WorkflowSequences");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "WorkflowSequences");

            migrationBuilder.RenameColumn(
                name: "RequiredRole",
                table: "WorkflowSequences",
                newName: "RequiredRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSequences_RequiredRoleId",
                table: "WorkflowSequences",
                column: "RequiredRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_RequiredRoleId",
                table: "WorkflowSequences",
                column: "RequiredRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_RequiredRoleId",
                table: "WorkflowSequences");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowSequences_RequiredRoleId",
                table: "WorkflowSequences");

            migrationBuilder.RenameColumn(
                name: "RequiredRoleId",
                table: "WorkflowSequences",
                newName: "RequiredRole");

            migrationBuilder.AddColumn<string>(
                name: "AppRoleId",
                table: "WorkflowSequences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSequences_AppRoleId",
                table: "WorkflowSequences",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_AppRoleId",
                table: "WorkflowSequences",
                column: "AppRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
