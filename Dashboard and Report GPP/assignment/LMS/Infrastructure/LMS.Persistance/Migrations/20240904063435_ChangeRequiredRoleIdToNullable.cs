﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRequiredRoleIdToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_RequiredRoleId",
                table: "WorkflowSequences");

            migrationBuilder.AlterColumn<string>(
                name: "RequiredRoleId",
                table: "WorkflowSequences",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_RequiredRoleId",
                table: "WorkflowSequences",
                column: "RequiredRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_RequiredRoleId",
                table: "WorkflowSequences");

            migrationBuilder.AlterColumn<string>(
                name: "RequiredRoleId",
                table: "WorkflowSequences",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSequences_AspNetRoles_RequiredRoleId",
                table: "WorkflowSequences",
                column: "RequiredRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
