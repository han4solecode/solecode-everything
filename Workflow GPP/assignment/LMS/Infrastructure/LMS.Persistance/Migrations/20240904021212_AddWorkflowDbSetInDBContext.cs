using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkflowDbSetInDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_AppUserId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Process_ProcessId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_WorkflowSequence_WorkflowSequenceStepId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Workflow_WorkflowId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAction_AspNetUsers_AppUserId",
                table: "WorkflowAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAction_Request_RequestId",
                table: "WorkflowAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAction_WorkflowSequence_WorkflowSequenceStepId",
                table: "WorkflowAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequence_AspNetRoles_AppRoleId",
                table: "WorkflowSequence");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequence_Workflow_WorkflowId",
                table: "WorkflowSequence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowSequence",
                table: "WorkflowSequence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowAction",
                table: "WorkflowAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Process",
                table: "Process");

            migrationBuilder.RenameTable(
                name: "WorkflowSequence",
                newName: "WorkflowSequences");

            migrationBuilder.RenameTable(
                name: "WorkflowAction",
                newName: "WorkflowActions");

            migrationBuilder.RenameTable(
                name: "Workflow",
                newName: "Workflows");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "Process",
                newName: "Processes");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowSequence_WorkflowId",
                table: "WorkflowSequences",
                newName: "IX_WorkflowSequences_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowSequence_AppRoleId",
                table: "WorkflowSequences",
                newName: "IX_WorkflowSequences_AppRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAction_WorkflowSequenceStepId",
                table: "WorkflowActions",
                newName: "IX_WorkflowActions_WorkflowSequenceStepId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAction_RequestId",
                table: "WorkflowActions",
                newName: "IX_WorkflowActions_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAction_AppUserId",
                table: "WorkflowActions",
                newName: "IX_WorkflowActions_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_WorkflowSequenceStepId",
                table: "Requests",
                newName: "IX_Requests_WorkflowSequenceStepId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_WorkflowId",
                table: "Requests",
                newName: "IX_Requests_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_ProcessId",
                table: "Requests",
                newName: "IX_Requests_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_AppUserId",
                table: "Requests",
                newName: "IX_Requests_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Requests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowSequences",
                table: "WorkflowSequences",
                column: "StepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowActions",
                table: "WorkflowActions",
                column: "ActionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows",
                column: "WorkflowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "RequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processes",
                table: "Processes",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_AppUserId",
                table: "Requests",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Processes_ProcessId",
                table: "Requests",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "ProcessId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_WorkflowSequences_WorkflowSequenceStepId",
                table: "Requests",
                column: "WorkflowSequenceStepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Workflows_WorkflowId",
                table: "Requests",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "WorkflowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_AspNetUsers_AppUserId",
                table: "WorkflowActions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_Requests_RequestId",
                table: "WorkflowActions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_WorkflowSequences_WorkflowSequenceStepId",
                table: "WorkflowActions",
                column: "WorkflowSequenceStepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_AppRoleId",
                table: "WorkflowSequences",
                column: "AppRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequences_Workflows_WorkflowId",
                table: "WorkflowSequences",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "WorkflowId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_AppUserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Processes_ProcessId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_WorkflowSequences_WorkflowSequenceStepId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Workflows_WorkflowId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_AspNetUsers_AppUserId",
                table: "WorkflowActions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_Requests_RequestId",
                table: "WorkflowActions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_WorkflowSequences_WorkflowSequenceStepId",
                table: "WorkflowActions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_AppRoleId",
                table: "WorkflowSequences");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequences_Workflows_WorkflowId",
                table: "WorkflowSequences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowSequences",
                table: "WorkflowSequences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowActions",
                table: "WorkflowActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processes",
                table: "Processes");

            migrationBuilder.RenameTable(
                name: "WorkflowSequences",
                newName: "WorkflowSequence");

            migrationBuilder.RenameTable(
                name: "Workflows",
                newName: "Workflow");

            migrationBuilder.RenameTable(
                name: "WorkflowActions",
                newName: "WorkflowAction");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameTable(
                name: "Processes",
                newName: "Process");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowSequences_WorkflowId",
                table: "WorkflowSequence",
                newName: "IX_WorkflowSequence_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowSequences_AppRoleId",
                table: "WorkflowSequence",
                newName: "IX_WorkflowSequence_AppRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowActions_WorkflowSequenceStepId",
                table: "WorkflowAction",
                newName: "IX_WorkflowAction_WorkflowSequenceStepId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowActions_RequestId",
                table: "WorkflowAction",
                newName: "IX_WorkflowAction_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowActions_AppUserId",
                table: "WorkflowAction",
                newName: "IX_WorkflowAction_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_WorkflowSequenceStepId",
                table: "Request",
                newName: "IX_Request_WorkflowSequenceStepId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_WorkflowId",
                table: "Request",
                newName: "IX_Request_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ProcessId",
                table: "Request",
                newName: "IX_Request_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AppUserId",
                table: "Request",
                newName: "IX_Request_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Request",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowSequence",
                table: "WorkflowSequence",
                column: "StepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow",
                column: "WorkflowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowAction",
                table: "WorkflowAction",
                column: "ActionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "RequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Process",
                table: "Process",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_AppUserId",
                table: "Request",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Process_ProcessId",
                table: "Request",
                column: "ProcessId",
                principalTable: "Process",
                principalColumn: "ProcessId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_WorkflowSequence_WorkflowSequenceStepId",
                table: "Request",
                column: "WorkflowSequenceStepId",
                principalTable: "WorkflowSequence",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Workflow_WorkflowId",
                table: "Request",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "WorkflowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAction_AspNetUsers_AppUserId",
                table: "WorkflowAction",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAction_Request_RequestId",
                table: "WorkflowAction",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAction_WorkflowSequence_WorkflowSequenceStepId",
                table: "WorkflowAction",
                column: "WorkflowSequenceStepId",
                principalTable: "WorkflowSequence",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequence_AspNetRoles_AppRoleId",
                table: "WorkflowSequence",
                column: "AppRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequence_Workflow_WorkflowId",
                table: "WorkflowSequence",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "WorkflowId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
