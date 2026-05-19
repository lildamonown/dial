using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursa4.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addToSubservice_Description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subservices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subservices");
        }
    }
}
