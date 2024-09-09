using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddNextStepRuleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "NextStepRules",
                columns: table => new
                {
                    RuleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentStepId = table.Column<int>(type: "integer", nullable: false),
                    NextStepId = table.Column<int>(type: "integer", nullable: false),
                    ConditionType = table.Column<string>(type: "text", nullable: false),
                    ConditionValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextStepRules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_NextStepRules_WorkflowSequences_CurrentStepId",
                        column: x => x.CurrentStepId,
                        principalTable: "WorkflowSequences",
                        principalColumn: "StepId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NextStepRules_WorkflowSequences_NextStepId",
                        column: x => x.NextStepId,
                        principalTable: "WorkflowSequences",
                        principalColumn: "StepId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NextStepRules_CurrentStepId",
                table: "NextStepRules",
                column: "CurrentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_NextStepRules_NextStepId",
                table: "NextStepRules",
                column: "NextStepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NextStepRules");

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
    }
}
