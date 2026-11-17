using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261115230658_init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "CloseDate", "CreatedAt", "Name", "PasswordHash", "Surname", "UpdatedAt", "Username" },
            values: new object[] { 1, null, new DateTime(2026, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Davit", "$2y$12$K9Fx74bcVht7wJa7j2ka/ubruvsKxTCzu1MpAkbpIMV4LjouNwjLK", "Muradyan", new DateTime(2026, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc), "testuser" });

        migrationBuilder.CreateIndex(
            name: "IX_Users_Username",
            table: "Users",
            column: "Username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users");
    }
}
