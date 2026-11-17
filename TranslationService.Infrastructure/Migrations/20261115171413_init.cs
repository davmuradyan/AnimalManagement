using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TranslationService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261115171413_init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EnumTranslations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                EnumType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                EnumCode = table.Column<int>(type: "int", nullable: false),
                Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Translation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EnumTranslations", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "EnumTranslations",
            columns: new[] { "Id", "ClosedDate", "CreatedAt", "EnumCode", "EnumType", "Language", "Translation", "UpdatedAt" },
            values: new object[,]
            {
                { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Color", "Eng", "Red", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Color", "Hy", "Կարմիր", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Color", "Eng", "Green", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Color", "Hy", "Կանաչ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Color", "Eng", "Blue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Color", "Hy", "Կապույտ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Color", "Eng", "Yellow", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Color", "Hy", "Դեղին", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Color", "Eng", "Orange", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Color", "Hy", "Նարնջագույն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Color", "Eng", "Purple", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 12, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Color", "Hy", "Մանուշակագույն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 13, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Color", "Eng", "Pink", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 14, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Color", "Hy", "Վարդագույն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 15, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Color", "Eng", "Black", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 16, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Color", "Hy", "Սև", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 17, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Color", "Eng", "White", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 18, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Color", "Hy", "Սպիտակ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 19, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Color", "Eng", "Brown", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 20, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Color", "Hy", "Շագանակագույն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 21, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Gender", "Eng", "Male", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 22, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Gender", "Hy", "Արու", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 23, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Gender", "Eng", "Female", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 24, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Gender", "Hy", "Էգ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
            });

        migrationBuilder.CreateIndex(
            name: "IX_EnumTranslations_EnumType_EnumCode_Language",
            table: "EnumTranslations",
            columns: new[] { "EnumType", "EnumCode", "Language" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EnumTranslations");
    }
}
