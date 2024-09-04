using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class BookRequestAndProcessUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_Requests_RequestId",
                table: "WorkflowActions");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Processes");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "WorkflowActions",
                newName: "ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowActions_RequestId",
                table: "WorkflowActions",
                newName: "IX_WorkflowActions_ProcessId");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Processes",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "ProcessName",
                table: "Processes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Processes",
                newName: "RequestType");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStepId",
                table: "Processes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequesterId",
                table: "Processes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkflowId",
                table: "Processes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookRequests",
                columns: table => new
                {
                    BookRequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ISBN = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Publisher = table.Column<string>(type: "text", nullable: true),
                    ProcessId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRequests", x => x.BookRequestId);
                    table.ForeignKey(
                        name: "FK_BookRequests_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "ProcessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_CurrentStepId",
                table: "Processes",
                column: "CurrentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_RequesterId",
                table: "Processes",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_WorkflowId",
                table: "Processes",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRequests_ProcessId",
                table: "BookRequests",
                column: "ProcessId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_AspNetUsers_RequesterId",
                table: "Processes",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_WorkflowSequences_CurrentStepId",
                table: "Processes",
                column: "CurrentStepId",
                principalTable: "WorkflowSequences",
                principalColumn: "StepId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Workflows_WorkflowId",
                table: "Processes",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "WorkflowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_Processes_ProcessId",
                table: "WorkflowActions",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "ProcessId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_AspNetUsers_RequesterId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_WorkflowSequences_CurrentStepId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Workflows_WorkflowId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_Processes_ProcessId",
                table: "WorkflowActions");

            migrationBuilder.DropTable(
                name: "BookRequests");

            migrationBuilder.DropIndex(
                name: "IX_Processes_CurrentStepId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_RequesterId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_WorkflowId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "CurrentStepId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "Processes");

            migrationBuilder.RenameColumn(
                name: "ProcessId",
                table: "WorkflowActions",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowActions_ProcessId",
                table: "WorkflowActions",
                newName: "IX_WorkflowActions_RequestId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Processes",
                newName: "ProcessName");

            migrationBuilder.RenameColumn(
                name: "RequestType",
                table: "Processes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Processes",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Processes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentStepId = table.Column<int>(type: "integer", nullable: false),
                    ProcessId = table.Column<int>(type: "integer", nullable: false),
                    RequesterId = table.Column<string>(type: "text", nullable: false),
                    WorkflowId = table.Column<int>(type: "integer", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestType = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "ProcessId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_WorkflowSequences_CurrentStepId",
                        column: x => x.CurrentStepId,
                        principalTable: "WorkflowSequences",
                        principalColumn: "StepId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "WorkflowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CurrentStepId",
                table: "Requests",
                column: "CurrentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProcessId",
                table: "Requests",
                column: "ProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_WorkflowId",
                table: "Requests",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_Requests_RequestId",
                table: "WorkflowActions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
