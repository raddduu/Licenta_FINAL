using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastViewForColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[VwScheduleColor] AS
                    WITH CTE AS (
                        SELECT
                            [SubjectId],
                            [TeacherId],
                            [ActivityId],
                            [GroupId],
                            [HallId],
                            [DistributionId],
                            [DayOfWeek],
                            [Hour],
                            [Minute],
                            [Duration],
                            [ActivityFrequencyId],
                            ROW_NUMBER() OVER (ORDER BY [DayOfWeek], [Hour], [Minute]) AS rn
                        FROM [dbo].[Schedule]
                    ),
                    RecursiveCTE AS (
                        SELECT
                            C1.*,
                            1 AS Number
                        FROM CTE C1
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM CTE C2
                            WHERE C2.rn = C1.rn - 1
                            AND (
                                C1.[DayOfWeek] = C2.[DayOfWeek]
                                AND (
                                    (C1.[Hour] = C2.[Hour] AND C1.[Minute] = C2.[Minute])
                                    OR
                                    (C1.[Hour] * 60 + C1.[Minute] + C1.[Duration] = C2.[Hour] * 60 + C2.[Minute])
                                )
                                AND (C1.[ActivityFrequencyId] = C2.[ActivityFrequencyId] OR C1.[DistributionId] = C2.[DistributionId])
                            )
                        )
                        UNION ALL
                        SELECT
                            C1.*,
                            CASE
                                WHEN R.Number + 1 > 8 THEN 1
                                ELSE R.Number + 1
                            END AS Number
                        FROM CTE C1
                        JOIN RecursiveCTE R ON C1.rn = R.rn + 1
                    )
                    SELECT distinct
                        [SubjectId],
                        [TeacherId],
                        [ActivityId],
                        [GroupId],
                        [HallId],
                        [DistributionId],
                        [ActivityFrequencyId],
                        MAX(Number) as Color
                    FROM RecursiveCTE
                    group by [SubjectId],
                        [TeacherId],
                        [ActivityId],
                        [GroupId],
                        [HallId],
                        [DistributionId],
                        [ActivityFrequencyId];");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[VwScheduleColor];");
        }
    }
}
