using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursa4.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addToReview_Grade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Grade",
                table: "Reviews",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Reviews");
        }
    }
}
