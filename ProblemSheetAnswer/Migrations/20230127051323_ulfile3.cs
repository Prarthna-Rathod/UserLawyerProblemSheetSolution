using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemSheetAnswer.Migrations
{
    public partial class ulfile3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Assign = table.Column<bool>(type: "bit", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picked = table.Column<bool>(type: "bit", nullable: false),
                    LawyerId = table.Column<int>(type: "int", nullable: true),
                    AssignTo = table.Column<int>(type: "int", nullable: true),
                    AssignToLawyerId = table.Column<int>(type: "int", nullable: true),
                    PickedBy = table.Column<int>(type: "int", nullable: true),
                    PickedByLawyerId = table.Column<int>(type: "int", nullable: true),
                    IsUserSatisfied = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questions_Lawyers_AssignToLawyerId",
                        column: x => x.AssignToLawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_questions_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_questions_Lawyers_PickedByLawyerId",
                        column: x => x.PickedByLawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_questions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_questions_AssignToLawyerId",
                table: "questions",
                column: "AssignToLawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_LawyerId",
                table: "questions",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_PickedByLawyerId",
                table: "questions",
                column: "PickedByLawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_UserId",
                table: "questions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "questions");
        }
    }
}
