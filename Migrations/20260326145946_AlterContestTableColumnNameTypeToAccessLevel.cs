using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contest_Management.Migrations
{
    /// <inheritdoc />
    public partial class AlterContestTableColumnNameTypeToAccessLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CONTESTS",
                newName: "accessLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "accessLevel",
                table: "CONTESTS",
                newName: "Type");
        }
    }
}
