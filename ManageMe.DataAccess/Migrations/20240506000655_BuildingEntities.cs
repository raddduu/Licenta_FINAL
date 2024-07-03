using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class BuildingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Activity__3214EC07383AA6F9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Methodology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Methodol__3214EC07D38F9247", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumeFamilie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NumeFami__3214EC27F4163969", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prenume",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Prenume = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prenume__3214EC27B7AC0E5B", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_AspNetRoles_Id",
                        column: x => x.Id,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyDomain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudyDom__3214EC07F9B105B0", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subject__3214EC07E8D1AEAB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Theme__3214EC07737EACA1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniversitaryYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstSemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstSemesterEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondSemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondSemesterEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversitaryYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradingActivity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradingActivity_Activity",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    AdditionalLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    HasComputers = table.Column<bool>(type: "bit", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hall_Building",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Batch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsForOlympiadParticipants = table.Column<bool>(type: "bit", nullable: false),
                    StudyDomainId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batch_StudyDomain",
                        column: x => x.StudyDomainId,
                        principalTable: "StudyDomain",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AccessTypeId = table.Column<int>(type: "int", nullable: false),
                    ChannelPic = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    JoinCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Channel__3214EC07E13C7935", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Channel__Subject__361203C5",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudyPlan",
                columns: table => new
                {
                    StudyDomainId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectType = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    CourseCredits = table.Column<int>(type: "int", nullable: false),
                    SeminaryCredits = table.Column<int>(type: "int", nullable: false),
                    LaboratoryCredits = table.Column<int>(type: "int", nullable: false),
                    ProjectCredits = table.Column<int>(type: "int", nullable: false),
                    EvaluationForm = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    TotalCredits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlan", x => new { x.StudyDomainId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_StudyPlan_StudyDomain",
                        column: x => x.StudyDomainId,
                        principalTable: "StudyDomain",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyPlan_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MethodologyTheme",
                columns: table => new
                {
                    MethodologyId = table.Column<int>(type: "int", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodologyTheme", x => new { x.MethodologyId, x.ThemeId });
                    table.ForeignKey(
                        name: "FK_MethodologyTheme_Methodology",
                        column: x => x.MethodologyId,
                        principalTable: "Methodology",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MethodologyTheme_Theme",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UniversitaryYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Event__3214EC078E412436", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Event__UniversitaryYear__2116E6DF",
                        column: x => x.UniversitaryYearId,
                        principalTable: "UniversitaryYears",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionTypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    UniversitaryYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Session__3214EC07361FB95C", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Session__UniversitaryYear__25DB9BFC",
                        column: x => x.UniversitaryYearId,
                        principalTable: "UniversitaryYears",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Group__3214EC07AA267893", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Group__Batch__7BE56230",
                        column: x => x.BatchId,
                        principalTable: "Batch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChannelRoles",
                columns: table => new
                {
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelRoles", x => new { x.ChannelId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ChannelRoles_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChannelRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "GradingCriterion",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    GradingActivityId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    MinimumPointsRequired = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    MattersForPassingTheSubject = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingCriterion", x => new { x.SubjectId, x.GroupId, x.GradingActivityId });
                    table.ForeignKey(
                        name: "FK_GradingCriterion_GradingActivity",
                        column: x => x.GradingActivityId,
                        principalTable: "GradingActivity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GradingCriterion_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GradingCriterion_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GradingCriterionSubject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    MinimumPointsRequired = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    BonusPoints = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    BonusPointsMattersForPassingSubject = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingCriterionSubject", x => new { x.SubjectId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GradingCriterionSubject_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GradingCriterionSubject_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    EnrollmentYear = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: true),
                    WorkHours = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK__User__GroupId__02925FBF",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChannelRequests",
                columns: table => new
                {
                    RequesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelRequests", x => new { x.RequesterId, x.ChannelId });
                    table.ForeignKey(
                        name: "FK_ChannelRequests_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelRequests_User_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    IsModerator = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelUsers", x => new { x.ChannelId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ChannelUsers_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelUsers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuidingTeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Thesis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FinalGrade = table.Column<decimal>(type: "decimal(7,5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Degree__3214EC07A242F3CD", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Degree__GuidingT__53A266AC",
                        column: x => x.GuidingTeacherId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Degree__StudentI__52AE4273",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExamTypeId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exam__3214EC078C26998E", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Exam__AuthorId__2D7CBDC4",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Exam__HallId__2A4B4B5E",
                        column: x => x.HallId,
                        principalTable: "Hall",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Exam__SessionId__2B947552",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Exam__SubjectId__2C88998B",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinalGrade",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalGrade", x => new { x.StudentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_FinalGrade_Student",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalGrade_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GradingActivityId = table.Column<int>(type: "int", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => new { x.StudentId, x.SubjectId, x.GradingActivityId, x.WeekNumber });
                    table.ForeignKey(
                        name: "FK_Grade_GradingActivity",
                        column: x => x.GradingActivityId,
                        principalTable: "GradingActivity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grade_Student",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grade_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Homework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Homework__3214EC07CE060F46", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Homework__Author__3335971A",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Homework__Subjec__324172E1",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invitation__3214EC178C26998E", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Invitation__ChannelId__5B947552",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Invitation__ReceiverId__4D7CBDC4",
                        column: x => x.ReceiverId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Invitation__SenderId__3D7CBDC4",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentMessageId = table.Column<int>(type: "int", nullable: true),
                    IsAnnouncement = table.Column<bool>(type: "bit", nullable: true),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ChannelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Message__3214EC07E325146B", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Message__AuthorI__3EA749C6",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Message__ParentM__3CBF0154",
                        column: x => x.ParentMessageId,
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    IsPending = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeclined = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    IsAproved = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    SolverId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Request__3214EC07F5F33126", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Request__Request__4A18FC72",
                        column: x => x.RequesterId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Request__SolverI__4EDDB18F",
                        column: x => x.SolverId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Request__ThemeId__4B0D20AB",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    DistributionId = table.Column<int>(type: "int", nullable: false),
                    ActivityFrequencyId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => new { x.SubjectId, x.TeacherId, x.ActivityId, x.GroupId, x.HallId, x.ActivityFrequencyId, x.DistributionId });
                    table.ForeignKey(
                        name: "FK_Schedule_Activity",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_Hall",
                        column: x => x.HallId,
                        principalTable: "Hall",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_Teacher",
                        column: x => x.TeacherId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeacherPermission",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherPermission", x => new { x.TeacherId, x.SubjectId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_TeacherPermission_Activity",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherPermission_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherPermission_Teacher",
                        column: x => x.TeacherId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeacherPermissionGroup",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherPermissionGroup", x => new { x.TeacherId, x.ActivityId, x.SubjectId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_TeacherPermissionGroup_Group",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherPermissionGroup_TeacherPermission",
                        columns: x => new { x.TeacherId, x.SubjectId, x.ActivityId },
                        principalTable: "TeacherPermission",
                        principalColumns: new[] { "TeacherId", "SubjectId", "ActivityId" });
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Activity__737584F6143B801E",
                table: "Activity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Batch_StudyDomain",
                table: "Batch",
                column: "StudyDomainId");

            migrationBuilder.CreateIndex(
                name: "index_Channel_Subject",
                table: "Channel",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelRequests_ChannelId",
                table: "ChannelRequests",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelRoles_ChannelId",
                table: "ChannelRoles",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelRoles_RoleId",
                table: "ChannelRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelUsers_UserId",
                table: "ChannelUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "index_Degree_GuidingTeacher",
                table: "Degree",
                column: "GuidingTeacherId");

            migrationBuilder.CreateIndex(
                name: "index_Degree_Student",
                table: "Degree",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "UQ__Degree__9B78415BCA0B6C85",
                table: "Degree",
                column: "Thesis",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index_Event_UniversitaryYear",
                table: "Event",
                column: "UniversitaryYearId");

            migrationBuilder.CreateIndex(
                name: "UQ__Event__737584F62DE15053",
                table: "Event",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index_Exam_Author",
                table: "Exam",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "index_Exam_ExamType",
                table: "Exam",
                column: "ExamTypeId");

            migrationBuilder.CreateIndex(
                name: "index_Exam_Hall",
                table: "Exam",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "index_Exam_Session",
                table: "Exam",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "index_Exam_Subject",
                table: "Exam",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_Student",
                table: "FinalGrade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_Subject",
                table: "FinalGrade",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "index_Grade_GradingActivity",
                table: "Grade",
                column: "GradingActivityId");

            migrationBuilder.CreateIndex(
                name: "index_Grade_Student",
                table: "Grade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "index_Grade_Subject",
                table: "Grade",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "index_GradingActivity_Activity",
                table: "GradingActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "UQ_GradingActivity",
                table: "GradingActivity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index_GradingCriterion_GradingActivity",
                table: "GradingCriterion",
                column: "GradingActivityId");

            migrationBuilder.CreateIndex(
                name: "index_GradingCriterion_Group",
                table: "GradingCriterion",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "index_GradingCriterion_Subject",
                table: "GradingCriterion",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "index_GradingCriterionSubject_Group",
                table: "GradingCriterionSubject",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "index_GradingCriterionSubject_Subject",
                table: "GradingCriterionSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "index_Group_Batch",
                table: "Group",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "UQ__Group__78A1A19D3536E801",
                table: "Group",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hall_Building",
                table: "Hall",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "index_Homework_Author",
                table: "Homework",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "index_Homework_Subject",
                table: "Homework",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "index_Invitation_Channel",
                table: "Invitation",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "index_Invitation_Receiver",
                table: "Invitation",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "index_Invitation_Sender",
                table: "Invitation",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "index_Message_Author",
                table: "Message",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "index_Message_MessageType",
                table: "Message",
                column: "MessageTypeId");

            migrationBuilder.CreateIndex(
                name: "index_Message_ParentMessage",
                table: "Message",
                column: "ParentMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChannelId",
                table: "Message",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "UQ__Methodol__737584F68D2AB986",
                table: "Methodology",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index_MethodologyTheme_Methodology",
                table: "MethodologyTheme",
                column: "MethodologyId");

            migrationBuilder.CreateIndex(
                name: "index_MethodologyTheme_Theme",
                table: "MethodologyTheme",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "index_Request_Requester",
                table: "Request",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "index_Request_Solver",
                table: "Request",
                column: "SolverId");

            migrationBuilder.CreateIndex(
                name: "index_Request_Theme",
                table: "Request",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Activity",
                table: "Schedule",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Group",
                table: "Schedule",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Hall",
                table: "Schedule",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Subject",
                table: "Schedule",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Teacher",
                table: "Schedule",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "index_Session_SessionType",
                table: "Session",
                column: "SessionTypeId");

            migrationBuilder.CreateIndex(
                name: "index_Session_UniversitaryYear",
                table: "Session",
                column: "UniversitaryYearId");

            migrationBuilder.CreateIndex(
                name: "UQ__StudyDom__737584F6CE28CABC",
                table: "StudyDomain",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index_StudyPlan_StudyDomain",
                table: "StudyPlan",
                column: "StudyDomainId");

            migrationBuilder.CreateIndex(
                name: "index_StudyPlan_Subject",
                table: "StudyPlan",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "UQ__Subject__737584F63299DB1E",
                table: "Subject",
                column: "Name",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "index_TeacherPermission_Activity",
                table: "TeacherPermission",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "index_TeacherPermission_Subject",
                table: "TeacherPermission",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "index_TeacherPermission_Teacher",
                table: "TeacherPermission",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPermissionGroup_ActivityId",
                table: "TeacherPermissionGroup",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPermissionGroup_GroupId",
                table: "TeacherPermissionGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPermissionGroup_SubjectId",
                table: "TeacherPermissionGroup",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPermissionGroup_TeacherId",
                table: "TeacherPermissionGroup",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPermissionGroup_TeacherId_SubjectId_ActivityId",
                table: "TeacherPermissionGroup",
                columns: new[] { "TeacherId", "SubjectId", "ActivityId" });

            migrationBuilder.CreateIndex(
                name: "UQ__Theme__737584F667099D1F",
                table: "Theme",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "index_User_Group",
                table: "User",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__A9D10534956297FA",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "index_UserRole_Role",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "index_UserRole_User",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChannelRequests");

            migrationBuilder.DropTable(
                name: "ChannelRoles");

            migrationBuilder.DropTable(
                name: "ChannelUsers");

            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "FinalGrade");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "GradingCriterion");

            migrationBuilder.DropTable(
                name: "GradingCriterionSubject");

            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MethodologyTheme");

            migrationBuilder.DropTable(
                name: "NumeFamilie");

            migrationBuilder.DropTable(
                name: "Prenume");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "SubjectActivityFrequency");

            migrationBuilder.DropTable(
                name: "TeacherPermissionGroup");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "GradingActivity");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Methodology");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "Hall");

            migrationBuilder.DropTable(
                name: "StudyPlan");

            migrationBuilder.DropTable(
                name: "TeacherPermission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UniversitaryYears");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Batch");

            migrationBuilder.DropTable(
                name: "StudyDomain");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
