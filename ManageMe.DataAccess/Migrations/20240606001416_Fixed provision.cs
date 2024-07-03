using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixedprovision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provisions_Articles_ArticleId",
                table: "Provisions");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Provisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Provisions_Articles_ArticleId",
                table: "Provisions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provisions_Articles_ArticleId",
                table: "Provisions");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Provisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Provisions_Articles_ArticleId",
                table: "Provisions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
