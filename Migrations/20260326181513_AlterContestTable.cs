using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contest_Management.Migrations
{
    /// <inheritdoc />
    public partial class AlterContestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CONTESTS_AspNetUsers_UserID",
                table: "CONTESTS");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "CONTESTS",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CONTESTS_AspNetUsers_UserID",
                table: "CONTESTS",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CONTESTS_AspNetUsers_UserID",
                table: "CONTESTS");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "CONTESTS",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CONTESTS_AspNetUsers_UserID",
                table: "CONTESTS",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
