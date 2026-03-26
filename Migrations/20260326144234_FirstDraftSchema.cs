using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contest_Management.Migrations
{
    /// <inheritdoc />
    public partial class FirstDraftSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LEADERBOARD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEADERBOARD", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LEADERBOARD_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QUESTIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QUESTIONS_CONTESTS_ContestID",
                        column: x => x.ContestID,
                        principalTable: "CONTESTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LEADERBOARD_UserID",
                table: "LEADERBOARD",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONS_ContestID",
                table: "QUESTIONS",
                column: "ContestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LEADERBOARD");

            migrationBuilder.DropTable(
                name: "QUESTIONS");
        }
    }
}
