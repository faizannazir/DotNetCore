using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abby.Migrations
{
    /// <inheritdoc />
    public partial class updatedTableColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
