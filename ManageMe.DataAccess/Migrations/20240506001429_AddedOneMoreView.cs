using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneMoreView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                create view [dbo].[VwTeacherGroupSubjectActivities] as
                select tpg.TeacherId, tpg.GroupId, tpg.SubjectId, s.[Name] SubjectName, tpg.ActivityId, a.[Name] ActivityName from 
                dbo.TeacherPermissionGroup tpg
                join dbo.Activity a on a.Id = tpg.ActivityId
                join dbo.[Subject] s on s.Id = tpg.SubjectId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop view [dbo].[VwTeacherGroupSubjectActivities];");
        }
    }
}
