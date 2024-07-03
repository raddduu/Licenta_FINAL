using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedprovisions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Sections");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Provisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    ParentProvisionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provisions_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provisions_Provisions_ParentProvisionId",
                        column: x => x.ParentProvisionId,
                        principalTable: "Provisions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provisions_ArticleId",
                table: "Provisions",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Provisions_ParentProvisionId",
                table: "Provisions",
                column: "ParentProvisionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provisions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
