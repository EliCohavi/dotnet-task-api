using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldToTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DueDate",
                table: "TaskItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "TaskItems");
        }
    }
}
