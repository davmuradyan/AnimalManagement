using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261115171324_init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Animals",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Age = table.Column<int>(type: "int", nullable: false),
                Weight = table.Column<double>(type: "float", nullable: false),
                Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Animals", x => x.Id);
                table.CheckConstraint("CK_Animal_Age", "[Weight] > 0");
                table.CheckConstraint("CK_Animal_Weight", "[Weight] > 0");
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Animals");
    }
}
