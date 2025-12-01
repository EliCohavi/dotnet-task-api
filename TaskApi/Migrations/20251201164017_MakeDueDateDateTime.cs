using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeDueDateDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Add a new temporary DateTime column
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDateTemp",
                table: "TaskItems",
                type: "datetime2",
                nullable: true);

            // 2) Convert existing int yyyyMMdd -> datetime into the temp column
            migrationBuilder.Sql(@"
                UPDATE TaskItems
                SET DueDateTemp = TRY_CONVERT(datetime2, 
                    CONCAT(
                      SUBSTRING(CAST(DueDate AS varchar(8)),1,4), '-', 
                      SUBSTRING(CAST(DueDate AS varchar(8)),5,2), '-', 
                      SUBSTRING(CAST(DueDate AS varchar(8)),7,2)
                    ), 23)
                WHERE DueDate IS NOT NULL;
            ");

            // 3) Drop the old int column
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "TaskItems");

            // 4) Rename temp column to the original name
            migrationBuilder.RenameColumn(
                name: "DueDateTemp",
                table: "TaskItems",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the process: add an int temp column, convert datetime -> yyyyMMdd int, drop datetime column, rename temp back.

            migrationBuilder.AddColumn<int>(
                name: "DueDateTemp",
                table: "TaskItems",
                type: "int",
                nullable: true);

            // Convert datetime -> yyyyMMdd (112 style yields YYYYMMDD)
            migrationBuilder.Sql(@"
                UPDATE TaskItems
                SET DueDateTemp = TRY_CAST(CONVERT(varchar(8), DueDate, 112) AS int)
                WHERE DueDate IS NOT NULL;
            ");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "DueDateTemp",
                table: "TaskItems",
                newName: "DueDate");
        }
    }
}