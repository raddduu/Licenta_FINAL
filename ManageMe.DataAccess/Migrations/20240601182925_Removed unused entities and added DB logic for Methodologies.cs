using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedunusedentitiesandaddedDBlogicforMethodologies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "MethodologyTheme");

            migrationBuilder.DropTable(
                name: "NumeFamilie");

            migrationBuilder.DropTable(
                name: "Prenume");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "UniversitaryYears");

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    MethodologyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapter_Methodology_MethodologyId",
                        column: x => x.MethodologyId,
                        principalTable: "Methodology",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(2048)", nullable: true),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    ParentSectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Section_Section_ParentSectionId",
                        column: x => x.ParentSectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Paragraph",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1024)", nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paragraph", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paragraph_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(512)", nullable: false),
                    ParagraphId = table.Column<int>(type: "int", nullable: false),
                    ParentDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detail_Detail_ParentDetailId",
                        column: x => x.ParentDetailId,
                        principalTable: "Detail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Detail_Paragraph_ParagraphId",
                        column: x => x.ParagraphId,
                        principalTable: "Paragraph",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_MethodologyId",
                table: "Chapter",
                column: "MethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_ParagraphId",
                table: "Detail",
                column: "ParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_ParentDetailId",
                table: "Detail",
                column: "ParentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Paragraph_SectionId",
                table: "Paragraph",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_ChapterId",
                table: "Section",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_ParentSectionId",
                table: "Section",
                column: "ParentSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detail");

            migrationBuilder.DropTable(
                name: "Paragraph");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidingTeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FinalGrade = table.Column<decimal>(type: "decimal(7,5)", nullable: true),
                    Thesis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "Homework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "NumeFamilie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prenume = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prenume__3214EC27B7AC0E5B", x => x.ID);
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
                    FirstSemesterEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstSemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondSemesterEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondSemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversitaryYears", x => x.Id);
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
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SolverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false),
                    IsAproved = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    IsDeclined = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    IsPending = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    Text = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
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
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversitaryYearId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false)
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
                    UniversitaryYearId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    SessionTypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false)
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
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExamTypeId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false)
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
                name: "index_Session_SessionType",
                table: "Session",
                column: "SessionTypeId");

            migrationBuilder.CreateIndex(
                name: "index_Session_UniversitaryYear",
                table: "Session",
                column: "UniversitaryYearId");

            migrationBuilder.CreateIndex(
                name: "UQ__Theme__737584F667099D1F",
                table: "Theme",
                column: "Name",
                unique: true);
        }
    }
}
