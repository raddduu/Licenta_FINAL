using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM Activity;
                DBCC CHECKIDENT (Activity, RESEED, 0);
                INSERT INTO Activity VALUES ('Exam');
                INSERT INTO Activity VALUES ('Verification');
                INSERT INTO Activity VALUES ('Laboratory');
                INSERT INTO Activity VALUES ('Course');
                INSERT INTO Activity VALUES ('Project');
                INSERT INTO Activity VALUES ('Seminary');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
