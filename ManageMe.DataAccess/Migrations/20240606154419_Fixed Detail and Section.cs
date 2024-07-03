using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedDetailandSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Paragraphs_ParagraphId",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Chapters_ChapterId",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Sections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParagraphId",
                table: "Details",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Paragraphs_ParagraphId",
                table: "Details",
                column: "ParagraphId",
                principalTable: "Paragraphs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Chapters_ChapterId",
                table: "Sections",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Paragraphs_ParagraphId",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Chapters_ChapterId",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParagraphId",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Paragraphs_ParagraphId",
                table: "Details",
                column: "ParagraphId",
                principalTable: "Paragraphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Chapters_ChapterId",
                table: "Sections",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
