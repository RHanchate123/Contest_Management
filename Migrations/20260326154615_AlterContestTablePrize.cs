using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contest_Management.Migrations
{
    /// <inheritdoc />
    public partial class AlterContestTablePrize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prize",
                table: "CONTESTS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prize",
                table: "CONTESTS");
        }
    }
}
