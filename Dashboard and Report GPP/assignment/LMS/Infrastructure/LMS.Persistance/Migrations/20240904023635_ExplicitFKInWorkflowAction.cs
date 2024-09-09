using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ExplicitFKInWorkflowAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_AspNetUsers_AppUserId",
                table: "WorkflowActions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_WorkflowSequences_WorkflowSequenceStepId",
                table: "WorkflowActions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowActions_AppUserId",
                table: "WorkflowActions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowActions_WorkflowSequenceStepId",
                table: "WorkflowActions");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "WorkflowActions");

            migrationBuilder.DropColumn(
                name: "WorkflowSequenceStepId",
                table: "WorkflowActions");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowActions_ActorId",
                table: "WorkflowActions",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowActions_StepId",
                table: "WorkflowActions",
                column: "StepId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_AspNetUsers_ActorId",
                table: "WorkflowActions",
                column: "ActorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_WorkflowSequences_StepId",
                table: "WorkflowActions",
                column: "StepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_AspNetUsers_ActorId",
                table: "WorkflowActions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_WorkflowSequences_StepId",
                table: "WorkflowActions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowActions_ActorId",
                table: "WorkflowActions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowActions_StepId",
                table: "WorkflowActions");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "WorkflowActions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkflowSequenceStepId",
                table: "WorkflowActions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowActions_AppUserId",
                table: "WorkflowActions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowActions_WorkflowSequenceStepId",
                table: "WorkflowActions",
                column: "WorkflowSequenceStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_AspNetUsers_AppUserId",
                table: "WorkflowActions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_WorkflowSequences_WorkflowSequenceStepId",
                table: "WorkflowActions",
                column: "WorkflowSequenceStepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
