using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TranslationService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261116160330_languagesAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "EnumTranslations",
            columns: new[] { "Id", "ClosedDate", "CreatedAt", "EnumCode", "EnumType", "Language", "Translation", "UpdatedAt" },
            values: new object[,]
            {
                { 25, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Language", "Eng", "English", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 26, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Language", "Hy", "Անգլերեն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 27, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Language", "Eng", "Armenian", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 28, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Language", "Hy", "Հայերեն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "EnumTranslations",
            keyColumn: "Id",
            keyValue: 25);

        migrationBuilder.DeleteData(
            table: "EnumTranslations",
            keyColumn: "Id",
            keyValue: 26);

        migrationBuilder.DeleteData(
            table: "EnumTranslations",
            keyColumn: "Id",
            keyValue: 27);

        migrationBuilder.DeleteData(
            table: "EnumTranslations",
            keyColumn: "Id",
            keyValue: 28);
    }
}
