using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemSheetAnswer.Migrations
{
    public partial class ulfile6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_oldConversations_questions_QuesId",
                table: "oldConversations");

            migrationBuilder.DropIndex(
                name: "IX_oldConversations_QuesId",
                table: "oldConversations");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "oldConversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "oldConversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "solvedBy",
                table: "oldConversations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "oldConversations");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "oldConversations");

            migrationBuilder.DropColumn(
                name: "solvedBy",
                table: "oldConversations");

            migrationBuilder.CreateIndex(
                name: "IX_oldConversations_QuesId",
                table: "oldConversations",
                column: "QuesId");

            migrationBuilder.AddForeignKey(
                name: "FK_oldConversations_questions_QuesId",
                table: "oldConversations",
                column: "QuesId",
                principalTable: "questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
