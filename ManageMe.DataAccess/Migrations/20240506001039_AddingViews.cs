using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER     view [dbo].[VwStudentGradeForSubject] as
									select
									tpg.*,
									CAST(round(tpg.TotalPoints + 0.0000001, 0) AS int) as TotalPointsRounded,
									ga.Id as GradingActivityId,
									ga.Name as GradingActivityName,
									sum(g.Value) as GradingActivityTotalPoints,
										case when
											gc.Points is null
											or gc.MinimumPointsRequired is null
											or gc.MattersForPassingTheSubject = 0
											or IIF(gc.Points <= sum(g.Value), gc.Points, sum(g.Value)) >= gc.MinimumPointsRequired 
										then CAST(1 AS BIT) else CAST(0 AS BIT) end as GradingActivityPassed,

									 case when
										(
											gcs.BonusPointsMattersForPassingSubject = 0
											and tpgamg.TotalPointsForGradingActivitiesThatMatter >= gcs.MinimumPointsRequired
										)
										or
										(
											gcs.BonusPointsMattersForPassingSubject = 1
											and tpgamg.TotalPointsForGradingActivitiesThatMatter + isnull(gcs.BonusPoints, 0) >= gcs.MinimumPointsRequired
										)
									 then 'Passed' else 'Failed' end as [Status]

									from (
										select
										u.Id as StudentId,
										g.SubjectId,
										u.FirstName,
										u.LastName,
										u.Email,
										u.GroupId,
										sum(g.Value) as TotalPoints

										from Grade g
										join [User] u on u.Id = g.StudentId

										group by 
										u.Id,
										g.SubjectId,
										u.FirstName,
										u.LastName,
										u.Email,
										u.GroupId
									) tpg
									join (
										select
										u.Id as StudentId,
										g.SubjectId,
										sum(g.Value) as TotalPointsForGradingActivitiesThatMatter

										from Grade g
										join [User] u on u.Id = g.StudentId
										join GradingCriterion gc on u.GroupId = gc.GroupId and g.SubjectId = gc.SubjectId and gc.MattersForPassingTheSubject = 1

										group by 
										u.Id,
										g.SubjectId
									) tpgamg on tpg.StudentId = tpgamg.StudentId and tpg.SubjectId = tpgamg.SubjectId

									join Grade g on g.StudentId = tpg.StudentId and g.SubjectId = tpg.SubjectId 
									join GradingActivity ga on g.GradingActivityId = ga.Id
									join GradingCriterion gc on gc.SubjectId = tpg.SubjectId and gc.GroupId = tpg.GroupId and gc.GradingActivityId = g.GradingActivityId
									join GradingCriterionSubject gcs on gcs.SubjectId = tpg.SubjectId and gcs.GroupId = tpg.GroupId

									group by
									tpg.StudentId,
									tpg.SubjectId,
									tpg.FirstName,
									tpg.LastName,
									tpg.Email,
									tpg.TotalPoints,
									ga.Id,
									ga.Name,
									tpg.GroupId,
									gc.Points,
									gc.MinimumPointsRequired,
									gc.MattersForPassingTheSubject,
									gcs.BonusPoints,
									gcs.BonusPointsMattersForPassingSubject,
									gcs.MinimumPointsRequired,
									tpgamg.TotalPointsForGradingActivitiesThatMatter
									;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW [dbo].[VwStudentGradeForSubject];");
        }
    }
}
