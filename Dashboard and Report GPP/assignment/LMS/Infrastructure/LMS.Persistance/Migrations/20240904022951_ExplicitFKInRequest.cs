using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ExplicitFKInRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_AppUserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_WorkflowSequences_WorkflowSequenceStepId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_AppUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_WorkflowSequenceStepId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "WorkflowSequenceStepId",
                table: "Requests");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CurrentStepId",
                table: "Requests",
                column: "CurrentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId",
                table: "Requests",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_WorkflowSequences_CurrentStepId",
                table: "Requests",
                column: "CurrentStepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_WorkflowSequences_CurrentStepId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CurrentStepId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Requests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkflowSequenceStepId",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AppUserId",
                table: "Requests",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_WorkflowSequenceStepId",
                table: "Requests",
                column: "WorkflowSequenceStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_AppUserId",
                table: "Requests",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_WorkflowSequences_WorkflowSequenceStepId",
                table: "Requests",
                column: "WorkflowSequenceStepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
