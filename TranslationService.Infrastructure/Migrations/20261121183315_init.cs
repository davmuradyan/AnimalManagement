using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TranslationService.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261121183315_init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TranslationGroup",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                IsCustom = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TranslationGroup", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "GroupItem",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GroupId = table.Column<int>(type: "int", nullable: false),
                ItemCode = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupItem", x => x.Id);
                table.ForeignKey(
                    name: "FK_GroupItem_TranslationGroup_GroupId",
                    column: x => x.GroupId,
                    principalTable: "TranslationGroup",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Translation",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GroupItemId = table.Column<int>(type: "int", nullable: false),
                LanguageTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TranslatedText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Translation", x => x.Id);
                table.ForeignKey(
                    name: "FK_Translation_GroupItem_GroupItemId",
                    column: x => x.GroupItemId,
                    principalTable: "GroupItem",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "TranslationGroup",
            columns: new[] { "Id", "ClosedDate", "CreatedAt", "IsCustom", "Name", "UpdatedAt" },
            values: new object[,]
            {
                { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Color", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gender", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Language", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
            });

        migrationBuilder.InsertData(
            table: "GroupItem",
            columns: new[] { "Id", "ClosedDate", "CreatedAt", "GroupId", "ItemCode", "UpdatedAt" },
            values: new object[,]
            {
                { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
            });

        migrationBuilder.InsertData(
            table: "Translation",
            columns: new[] { "Id", "ClosedDate", "CreatedAt", "GroupItemId", "LanguageTag", "TranslatedText", "UpdatedAt" },
            values: new object[,]
            {
                { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "En", "Red", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hy", "Կարմիր", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "En", "Green", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Hy", "Կանաչ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "En", "Blue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Hy", "Կապույտ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "En", "Male", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Hy", "Արու", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "En", "Female", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Hy", "Էգ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "En", "English", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 12, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Hy", "Անգլերեն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 13, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "En", "Armenian", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 14, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Hy", "Հայերեն", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
            });

        migrationBuilder.CreateIndex(
            name: "IX_GroupItem_GroupId",
            table: "GroupItem",
            column: "GroupId");

        migrationBuilder.CreateIndex(
            name: "IX_Translation_GroupItemId",
            table: "Translation",
            column: "GroupItemId");

        migrationBuilder.CreateIndex(
            name: "IX_TranslationGroup_Name",
            table: "TranslationGroup",
            column: "Name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Translation");

        migrationBuilder.DropTable(
            name: "GroupItem");

        migrationBuilder.DropTable(
            name: "TranslationGroup");
    }
}
