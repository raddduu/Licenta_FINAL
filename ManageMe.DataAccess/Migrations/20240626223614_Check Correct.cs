using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class CheckCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyPlan",
                table: "StudyPlan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyPlan",
                table: "StudyPlan",
                columns: new[] { "StudyDomainId", "SubjectId", "Semester" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyPlan",
                table: "StudyPlan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyPlan",
                table: "StudyPlan",
                columns: new[] { "StudyDomainId", "SubjectId" });

            migrationBuilder.CreateTable(
                name: "SubjectActivityFrequency",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudyDomainId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    SubjectActivityFrequencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectActivityFrequency", x => new { x.SubjectId, x.StudyDomainId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_SubjectActivityFrequency_Activity",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectActivityFrequency_StudyPlan",
                        columns: x => new { x.StudyDomainId, x.SubjectId },
                        principalTable: "StudyPlan",
                        principalColumns: new[] { "StudyDomainId", "SubjectId" });
                });

            migrationBuilder.CreateIndex(
                name: "index_SubjectActivityFrequency_Activity",
                table: "SubjectActivityFrequency",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "index_SubjectActivityFrequency_StudyDomain",
                table: "SubjectActivityFrequency",
                column: "StudyDomainId");

            migrationBuilder.CreateIndex(
                name: "index_SubjectActivityFrequency_Subject",
                table: "SubjectActivityFrequency",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectActivityFrequency_StudyDomainId_SubjectId",
                table: "SubjectActivityFrequency",
                columns: new[] { "StudyDomainId", "SubjectId" });
        }
    }
}
