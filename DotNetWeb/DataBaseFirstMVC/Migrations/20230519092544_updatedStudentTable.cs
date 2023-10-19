using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseFirstMVC.Migrations
{
    /// <inheritdoc />
    public partial class updatedStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "students",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "students");
        }
    }
}
