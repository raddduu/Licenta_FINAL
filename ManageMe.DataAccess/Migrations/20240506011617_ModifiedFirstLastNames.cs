using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedFirstLastNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                               name: "PK__Prenume__3214EC27B7AC0E5B",
                               table: "Prenume");

            migrationBuilder.DropPrimaryKey(
                               name: "PK__NumeFami__3214EC27F4163969",
                               table: "NumeFamilie");

            migrationBuilder.DropColumn(
                               name: "ID",
                               table: "Prenume");

            migrationBuilder.DropColumn(
                               name: "ID",
                               table: "NumeFamilie");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Prenume",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "NumeFamilie",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                               name: "PK_Prenume",
                               table: "Prenume",
                               column: "ID");

            migrationBuilder.AddPrimaryKey(
                               name: "PK_NumeFamilie",
                               table: "NumeFamilie",
                               column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Prenume",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "NumeFamilie",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
