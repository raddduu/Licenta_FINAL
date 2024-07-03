using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedSection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sections",
                type: "nvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
