using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedStudyPlanPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //drop the SubjectActivityFrequency table
            migrationBuilder.DropTable(
                name: "SubjectActivityFrequency"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyPlan",
                table: "StudyPlan"
            );

            // Add the new primary key to the StudyPlan table
            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyPlan",
                table: "StudyPlan",
                columns: new[] { "StudyDomainId", "SubjectId", "Semester" });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
