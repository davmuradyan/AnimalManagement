using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261116210924_FixAnimalConstraints : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(
            name: "CK_Animal_Age",
            table: "Animals");

        migrationBuilder.AddCheckConstraint(
            name: "CK_Animal_Age",
            table: "Animals",
            sql: "[Age] > 0");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(
            name: "CK_Animal_Age",
            table: "Animals");

        migrationBuilder.AddCheckConstraint(
            name: "CK_Animal_Age",
            table: "Animals",
            sql: "[Weight] > 0");
    }
}
