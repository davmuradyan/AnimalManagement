using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261118140850_createdByAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "CreatedByUsername",
            table: "Animals",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "UpdatedByUsername",
            table: "Animals",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedByUsername",
            table: "Animals");

        migrationBuilder.DropColumn(
            name: "UpdatedByUsername",
            table: "Animals");
    }
}
