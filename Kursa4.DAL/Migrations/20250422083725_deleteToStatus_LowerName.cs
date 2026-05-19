using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursa4.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deleteToStatus_LowerName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowerName",
                table: "Statuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LowerName",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
