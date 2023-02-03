using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemSheetAnswer.Migrations
{
    public partial class ulfile5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "oldConversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oldConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_oldConversations_questions_QuesId",
                        column: x => x.QuesId,
                        principalTable: "questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_oldConversations_QuesId",
                table: "oldConversations",
                column: "QuesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "oldConversations");
        }
    }
}
