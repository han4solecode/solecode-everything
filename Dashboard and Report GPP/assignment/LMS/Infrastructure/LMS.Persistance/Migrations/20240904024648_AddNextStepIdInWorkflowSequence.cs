using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddNextStepIdInWorkflowSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StepOrder",
                table: "WorkflowSequences",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "NextStepId",
                table: "WorkflowSequences",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowSequences_NextStepId",
                table: "WorkflowSequences",
                column: "NextStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequences_WorkflowSequences_NextStepId",
                table: "WorkflowSequences",
                column: "NextStepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequences_WorkflowSequences_NextStepId",
                table: "WorkflowSequences");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowSequences_NextStepId",
                table: "WorkflowSequences");

            migrationBuilder.DropColumn(
                name: "NextStepId",
                table: "WorkflowSequences");

            migrationBuilder.AlterColumn<int>(
                name: "StepOrder",
                table: "WorkflowSequences",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
