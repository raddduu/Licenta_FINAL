using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Removedchapterparagraphs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Chapters_ChapterId",
                table: "Paragraphs");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Sections_SectionId",
                table: "Paragraphs");

            migrationBuilder.DropIndex(
                name: "IX_Paragraphs_ChapterId",
                table: "Paragraphs");

            migrationBuilder.DropColumn(
                name: "ChapterId",
                table: "Paragraphs");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "Paragraphs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Sections_SectionId",
                table: "Paragraphs",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Sections_SectionId",
                table: "Paragraphs");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "Paragraphs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ChapterId",
                table: "Paragraphs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paragraphs_ChapterId",
                table: "Paragraphs",
                column: "ChapterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Chapters_ChapterId",
                table: "Paragraphs",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Sections_SectionId",
                table: "Paragraphs",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }
    }
}
