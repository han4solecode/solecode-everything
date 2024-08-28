using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LMS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddLendingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Penalty",
                table: "AspNetUsers",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lendings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    BorrowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false),
                    DateDeleted = table.Column<DateOnly>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteReason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lendings_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lendings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_AppUserId",
                table: "Lendings",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_BookId",
                table: "Lendings",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lendings");

            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "AspNetUsers");
        }
    }
}
