using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Methodology_MethodologyId",
                table: "Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Detail_Detail_ParentDetailId",
                table: "Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Detail_Paragraph_ParagraphId",
                table: "Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraph_Section_SectionId",
                table: "Paragraph");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Chapter_ChapterId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Section_ParentSectionId",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paragraph",
                table: "Paragraph");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail",
                table: "Detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter");

            migrationBuilder.RenameTable(
                name: "Section",
                newName: "Sections");

            migrationBuilder.RenameTable(
                name: "Paragraph",
                newName: "Paragraphs");

            migrationBuilder.RenameTable(
                name: "Detail",
                newName: "Details");

            migrationBuilder.RenameTable(
                name: "Chapter",
                newName: "Chapters");

            migrationBuilder.RenameIndex(
                name: "IX_Section_ParentSectionId",
                table: "Sections",
                newName: "IX_Sections_ParentSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Section_ChapterId",
                table: "Sections",
                newName: "IX_Sections_ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Paragraph_SectionId",
                table: "Paragraphs",
                newName: "IX_Paragraphs_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Detail_ParentDetailId",
                table: "Details",
                newName: "IX_Details_ParentDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Detail_ParagraphId",
                table: "Details",
                newName: "IX_Details_ParagraphId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapter_MethodologyId",
                table: "Chapters",
                newName: "IX_Chapters_MethodologyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paragraphs",
                table: "Paragraphs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Details",
                table: "Details",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Methodology_MethodologyId",
                table: "Chapters",
                column: "MethodologyId",
                principalTable: "Methodology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Details_ParentDetailId",
                table: "Details",
                column: "ParentDetailId",
                principalTable: "Details",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Paragraphs_ParagraphId",
                table: "Details",
                column: "ParagraphId",
                principalTable: "Paragraphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Sections_SectionId",
                table: "Paragraphs",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Chapters_ChapterId",
                table: "Sections",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_ParentSectionId",
                table: "Sections",
                column: "ParentSectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Methodology_MethodologyId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Details_Details_ParentDetailId",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Details_Paragraphs_ParagraphId",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Sections_SectionId",
                table: "Paragraphs");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Chapters_ChapterId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_ParentSectionId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paragraphs",
                table: "Paragraphs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Details",
                table: "Details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "Section");

            migrationBuilder.RenameTable(
                name: "Paragraphs",
                newName: "Paragraph");

            migrationBuilder.RenameTable(
                name: "Details",
                newName: "Detail");

            migrationBuilder.RenameTable(
                name: "Chapters",
                newName: "Chapter");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_ParentSectionId",
                table: "Section",
                newName: "IX_Section_ParentSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_ChapterId",
                table: "Section",
                newName: "IX_Section_ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Paragraphs_SectionId",
                table: "Paragraph",
                newName: "IX_Paragraph_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Details_ParentDetailId",
                table: "Detail",
                newName: "IX_Detail_ParentDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Details_ParagraphId",
                table: "Detail",
                newName: "IX_Detail_ParagraphId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_MethodologyId",
                table: "Chapter",
                newName: "IX_Chapter_MethodologyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                table: "Section",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paragraph",
                table: "Paragraph",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail",
                table: "Detail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Methodology_MethodologyId",
                table: "Chapter",
                column: "MethodologyId",
                principalTable: "Methodology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_Detail_ParentDetailId",
                table: "Detail",
                column: "ParentDetailId",
                principalTable: "Detail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_Paragraph_ParagraphId",
                table: "Detail",
                column: "ParagraphId",
                principalTable: "Paragraph",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraph_Section_SectionId",
                table: "Paragraph",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Chapter_ChapterId",
                table: "Section",
                column: "ChapterId",
                principalTable: "Chapter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Section_ParentSectionId",
                table: "Section",
                column: "ParentSectionId",
                principalTable: "Section",
                principalColumn: "Id");
        }
    }
}
