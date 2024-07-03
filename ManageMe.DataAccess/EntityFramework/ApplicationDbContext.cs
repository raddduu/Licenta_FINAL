using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ManageMe.Entities;

using ManageMe.Entities.Entities;

namespace ManageMe.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }

        public virtual DbSet<Channel> Channels { get; set; }

        public virtual DbSet<Grade> Grades { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Methodology> Methodologies { get; set; }

        public virtual DbSet<StudyDomain> StudyDomains { get; set; }

        public virtual DbSet<StudyPlan> StudyPlans { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public virtual DbSet<ChannelUser> ChannelUsers { get; set; }

        public virtual DbSet<ChannelRequest> ChannelRequests { get; set; }

        public virtual DbSet<TeacherPermission> TeacherPermissions { get; set; }
        
        public virtual DbSet<GradingCriterion> GradingCriteria { get; set; }

        public virtual DbSet<GradingCriterionSubject> GradingCriterionSubjects { get; set; }

        //public virtual DbSet<SubjectActivityFrequency> SubjectActivityFrequencies { get; set; }

        public virtual DbSet<GradingActivity> GradingActivities { get; set; }

        public virtual DbSet<VwStudentGradeForSubject> StudentGradeForSubjects { get; set; }

        public virtual DbSet<FinalGrade> FinalGrades { get; set; }

        public virtual DbSet<Building> Buildings { get; set; }

        public virtual DbSet<Hall> Halls { get; set; }

        public virtual DbSet<Schedule> Schedules { get; set; }

        public virtual DbSet<VwScheduleColor> ScheduleColors { get; set; }

        public virtual DbSet<Batch> Batches { get; set; }

        public virtual DbSet<Chapter> Chapters { get; set; }

        public virtual DbSet<Section> Sections { get; set; }

        public virtual DbSet<Paragraph> Paragraphs { get; set; }

        public virtual DbSet<Detail> Details { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Provision> Provisions { get; set; }

        public virtual DbSet<Semester> Semesters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.StudyDomainId, "IX_Batch_StudyDomain");

                entity.ToTable("Batch");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.HasOne(d => d.StudyDomain)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.StudyDomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batch_StudyDomain");
            });

            modelBuilder.Entity<VwScheduleColor>(VwScheduleColor =>
            {
                VwScheduleColor.HasNoKey();
                VwScheduleColor.ToView("VwScheduleColor");
            });

            modelBuilder.Entity<Building>(entity => {
                entity.HasKey(e => e.Id);

                entity.ToTable("Building");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.BuildingId, "IX_Hall_Building");

                entity.ToTable("Hall");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Halls)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hall_Building");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => new { e.SubjectId, e.TeacherId, e.ActivityId, e.GroupId, e.HallId, e.ActivityFrequencyId, e.DistributionId});

                entity.HasIndex(e => e.HallId, "IX_Schedule_Hall");

                entity.HasIndex(e => e.SubjectId, "IX_Schedule_Subject");

                entity.HasIndex(e => e.TeacherId, "IX_Schedule_Teacher");

                entity.HasIndex(e => e.ActivityId, "IX_Schedule_Activity");

                entity.HasIndex(e => e.GroupId, "IX_Schedule_Group");

                entity.ToTable("Schedule");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.HallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Hall");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Subject");

                entity.HasOne(d => d.Teacher)
                .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Teacher");

                entity.HasOne(d => d.Activity)
                .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Activity");

                entity.HasOne(d => d.Group)
                .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Group");
            });

            modelBuilder.Entity<FinalGrade>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectId });

                entity.HasIndex(e => e.SubjectId, "IX_FinalGrade_Subject");

                entity.HasIndex(e => e.StudentId, "IX_FinalGrade_Student");

                entity.ToTable("FinalGrade");

                entity.HasOne(e => e.Student)
                    .WithMany(e => e.FinalGrades)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinalGrade_Student");

                entity.HasOne(e => e.Subject)
                    .WithMany(e => e.FinalGrades)
                    .HasForeignKey(e => e.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinalGrade_Subject");
            });

            modelBuilder.Entity<VwStudentGradeForSubject>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwStudentGradeForSubject");
            });

            modelBuilder.Entity<ChannelUser>()
                .HasKey(ab => new {ab.ChannelId, ab.UserId});

            modelBuilder.Entity<ChannelUser>()
                .HasOne(ab => ab.User)
                .WithMany(ab => ab.ChannelUsers)
                .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<ChannelUser>()
                .HasOne(ab => ab.Channel)
                .WithMany(ab => ab.ChannelUsers)
                .HasForeignKey(ab => ab.ChannelId);

            modelBuilder.Entity<ChannelRequest>()
                .HasKey(ab => new { ab.RequesterId, ab.ChannelId });

            modelBuilder.Entity<ChannelRequest>()
                .HasOne(ab => ab.Requester)
                .WithMany(ab => ab.ChannelRequests)
                .HasForeignKey(ab => ab.RequesterId);

            modelBuilder.Entity<ChannelRequest>()
                .HasOne(ab => ab.Channel)
                .WithMany(ab => ab.ChannelRequests)
                .HasForeignKey(ab => ab.ChannelId);


            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC07383AA6F9");

                entity.ToTable("Activity");

                entity.HasIndex(e => e.Name, "UQ__Activity__737584F6143B801E").IsUnique();

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<GradingActivity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_GradingActivity");

                entity.ToTable("GradingActivity");

                entity.HasIndex(e => e.ActivityId, "index_GradingActivity_Activity");

                entity.HasIndex(e => e.Name, "UQ_GradingActivity").IsUnique();

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.Activity).WithMany(p => p.GradingActivities)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradingActivity_Activity");
            });

            //modelBuilder.Entity<Calendar>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Calendar__3214EC07F27C28DA");

            //    entity.ToTable("Calendar");

            //    entity.HasIndex(e => e.Name, "UQ__Calendar__737584F6F6D26D05").IsUnique();

            //    entity.HasIndex(e => e.CalendarTypeId, "index_Calendar_CalendarType");

            //    entity.Property(e => e.Description).HasMaxLength(500);
            //    entity.Property(e => e.Name).HasMaxLength(30);

            //    entity.HasOne(d => d.CalendarType).WithMany(p => p.Calendars)
            //        .HasForeignKey(d => d.CalendarTypeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__Calendar__Calend__0EF836A4");
            //});

            //modelBuilder.Entity<CalendarType>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Calendar__3214EC07F9524FD1");

            //    entity.ToTable("CalendarType");

            //    entity.HasIndex(e => e.Name, "UQ__Calendar__737584F6C08F7737").IsUnique();

            //    entity.Property(e => e.Name).HasMaxLength(30);
            //});

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Channel__3214EC07E13C7935");

                entity.ToTable("Channel");

                entity.HasIndex(e => e.SubjectId, "index_Channel_Subject");

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.HasOne(d => d.Subject).WithMany(p => p.Channels)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Channel__Subject__361203C5");

                entity.HasMany(c => c.ApplicationRoles).WithMany(r => r.Channels)
                    .UsingEntity<Dictionary<string, object>>(
                        "ChannelRoles",
                        r => r.HasOne<ApplicationRole>().WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_ChannelRoles_Role_RoleId"),
                        c => c.HasOne<Channel>().WithMany()
                            .HasForeignKey("ChannelId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_ChannelRoles_Channel_ChannelId"),
                        j =>
                        {
                            j.HasKey("ChannelId", "RoleId");
                            j.ToTable("ChannelRoles");
                            j.HasIndex(new[] { "ChannelId" }, "IX_ChannelRoles_ChannelId");
                            j.HasIndex(new[] { "RoleId" }, "IX_ChannelRoles_RoleId");
                        }
                    );
            });

            modelBuilder.Entity<GradingCriterion>(entity =>
            {
                entity.HasKey(e => new { e.SubjectId, e.GroupId, e.GradingActivityId })
                    .HasName("PK_GradingCriterion");

                entity.ToTable("GradingCriterion");

                entity.HasIndex(e => e.GroupId, "index_GradingCriterion_Group");

                entity.HasIndex(e => e.SubjectId, "index_GradingCriterion_Subject");

                entity.HasIndex(e => e.GradingActivityId, "index_GradingCriterion_GradingActivity");

                entity.Property(e => e.Points).HasColumnType("decimal(4, 2)");
                entity.Property(e => e.MinimumPointsRequired).HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.Group).WithMany(p => p.GradingCriteria)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradingCriterion_Group");

                entity.HasOne(d => d.Subject).WithMany(p => p.GradingCriteria)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradingCriterion_Subject");

                entity.HasOne(d => d.GradingActivity).WithMany(p => p.GradingCriteria)
                    .HasForeignKey(d => d.GradingActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradingCriterion_GradingActivity");
            });

            modelBuilder.Entity<GradingCriterionSubject>(entity =>
            {
                entity.HasKey(e => new { e.SubjectId, e.GroupId})
                    .HasName("PK_GradingCriterionSubject");

                entity.ToTable("GradingCriterionSubject");

                entity.HasIndex(e => e.GroupId, "index_GradingCriterionSubject_Group");

                entity.HasIndex(e => e.SubjectId, "index_GradingCriterionSubject_Subject");

                entity.Property(e => e.BonusPoints).HasColumnType("decimal(4, 2)");
                entity.Property(e => e.MinimumPointsRequired).HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.Group).WithMany(p => p.GradingCriterionSubjects)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradingCriterionSubject_Group");

                entity.HasOne(d => d.Subject).WithMany(p => p.GradingCriterionSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradingCriterionSubject_Subject");
            });

            //modelBuilder.Entity<SubjectActivityFrequency>(entity =>
            //{
            //    entity.HasKey(e => new { e.SubjectId, e.StudyDomainId, e.ActivityId })
            //        .HasName("PK_SubjectActivityFrequency");

            //    entity.ToTable("SubjectActivityFrequency");

            //    entity.HasIndex(e => e.ActivityId, "index_SubjectActivityFrequency_Activity");

            //    entity.HasIndex(e => e.SubjectId, "index_SubjectActivityFrequency_Subject");

            //    entity.HasIndex(e => e.StudyDomainId, "index_SubjectActivityFrequency_StudyDomain");

            //    entity.HasOne(d => d.Activity).WithMany(p => p.SubjectActivityFrequencies)
            //        .HasForeignKey(d => d.ActivityId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SubjectActivityFrequency_Activity");

            //    entity.HasOne(d => d.StudyPlan).WithMany(p => p.SubjectActivityFrequencies)
            //        .HasForeignKey(d => new { d.StudyDomainId, d.SubjectId })
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SubjectActivityFrequency_StudyPlan");
            //});

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectId, e.GradingActivityId, e.WeekNumber });

                entity.ToTable("Grade");

                entity.HasIndex(e => e.GradingActivityId, "index_Grade_GradingActivity");

                entity.HasIndex(e => e.StudentId, "index_Grade_Student");

                entity.HasIndex(e => e.SubjectId, "index_Grade_Subject");

                //entity.HasIndex(e => e.WeekNumber, "index_Grade_WeekNumber");

                entity.Property(e => e.Value).HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.GradingActivity).WithMany(p => p.Grades)
                    .HasForeignKey(d => d.GradingActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_GradingActivity");

                entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Student");

                entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Subject");
            });

            modelBuilder.Entity<VwTeacherGroupSubjectActivities>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwTeacherGroupSubjectActivities");
            });


            modelBuilder.Entity<TeacherPermission>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.SubjectId, e.ActivityId });

                entity.ToTable("TeacherPermission");

                entity.HasIndex(e => e.ActivityId, "index_TeacherPermission_Activity");

                entity.HasIndex(e => e.TeacherId, "index_TeacherPermission_Teacher");

                entity.HasIndex(e => e.SubjectId, "index_TeacherPermission_Subject");

                entity.HasOne(d => d.Activity).WithMany(p => p.TeacherPermissions)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPermission_Activity");

                entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherPermissions)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPermission_Teacher");

                entity.HasOne(d => d.Subject).WithMany(p => p.TeacherPermissions)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPermission_Subject");

                entity.HasMany(c => c.Groups).WithMany(r => r.TeacherPermissions)
                    .UsingEntity<Dictionary<string, object>>(
                        "TeacherPermissionGroup",
                        g => g.HasOne<Group>().WithMany()
                            .HasForeignKey("GroupId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_TeacherPermissionGroup_Group"),
                        tp => tp.HasOne<TeacherPermission>().WithMany()
                            .HasForeignKey("TeacherId", "SubjectId", "ActivityId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_TeacherPermissionGroup_TeacherPermission"),
                        j =>
                        {
                            j.HasKey("TeacherId", "ActivityId", "SubjectId", "GroupId");
                            j.ToTable("TeacherPermissionGroup");
                            j.HasIndex(new[] { "TeacherId" }, "IX_TeacherPermissionGroup_TeacherId");
                            j.HasIndex(new[] { "ActivityId" }, "IX_TeacherPermissionGroup_ActivityId");
                            j.HasIndex(new[] { "SubjectId" }, "IX_TeacherPermissionGroup_SubjectId");
                            j.HasIndex(new[] { "GroupId" }, "IX_TeacherPermissionGroup_GroupId");
                        }
                    );
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Group__3214EC07AA267893");

                entity.ToTable("Group");

                entity.HasIndex(e => e.Number, "UQ__Group__78A1A19D3536E801").IsUnique();

                entity.HasIndex(e => e.BatchId, "index_Group_Batch");

                entity.Property(e => e.Number).HasMaxLength(50);

                entity.HasOne(d => d.Batch).WithMany(p => p.Groups)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Group__Batch__7BE56230");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Message__3214EC07E325146B");

                entity.ToTable("Message");

                entity.HasIndex(e => e.AuthorId, "index_Message_Author");

                entity.HasIndex(e => e.MessageTypeId, "index_Message_MessageType");

                entity.HasIndex(e => e.ParentMessageId, "index_Message_ParentMessage");

                entity.Property(e => e.Time)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Author).WithMany(p => p.Messages)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__AuthorI__3EA749C6");

                entity.HasOne(d => d.ParentMessage).WithMany(p => p.ChildrenMessages)
                    .HasForeignKey(d => d.ParentMessageId)
                    .HasConstraintName("FK__Message__ParentM__3CBF0154");
            });

            modelBuilder.Entity<Methodology>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Methodol__3214EC07D38F9247");

                entity.ToTable("Methodology");

                entity.HasIndex(e => e.Name, "UQ__Methodol__737584F68D2AB986").IsUnique();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.Name, "UQ__Role__737584F66C18D99F").IsUnique();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<StudyDomain>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__StudyDom__3214EC07F9B105B0");

                entity.ToTable("StudyDomain");

                entity.HasIndex(e => e.Name, "UQ__StudyDom__737584F6CE28CABC").IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<StudyPlan>(entity =>
            {
                entity.HasKey(e => new { e.StudyDomainId, e.SubjectId, e.Semester });

                entity.ToTable("StudyPlan");

                entity.HasIndex(e => e.StudyDomainId, "index_StudyPlan_StudyDomain");

                entity.HasIndex(e => e.SubjectId, "index_StudyPlan_Subject");

                entity.Property(e => e.EvaluationForm).HasMaxLength(1);
                entity.Property(e => e.SubjectType).HasMaxLength(2);

                entity.HasOne(d => d.StudyDomain).WithMany(p => p.StudyPlans)
                    .HasForeignKey(d => d.StudyDomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudyPlan_StudyDomain");

                entity.HasOne(d => d.Subject).WithMany(p => p.StudyPlans)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudyPlan_Subject");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Subject__3214EC07E8D1AEAB");

                entity.ToTable("Subject");

                entity.HasIndex(e => e.Name, "UQ__Subject__737584F63299DB1E").IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                //entity.HasKey(e => e.Id).HasName("PK__User__3214EC072A1052AD");

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534956297FA").IsUnique();

                entity.HasIndex(e => e.GroupId, "index_User_Group");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.BirthDate).HasColumnType("date");
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.FirstName).HasMaxLength(20);
                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.HasOne(d => d.Group).WithMany(p => p.Users)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__User__GroupId__02925FBF");

                entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        r => r.HasOne<ApplicationRole>().WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_UserRole_Role"),
                        l => l.HasOne<ApplicationUser>().WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_UserRole_User"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");
                            j.ToTable("UserRole");
                            j.HasIndex(new[] { "RoleId" }, "index_UserRole_Role");
                            j.HasIndex(new[] { "UserId" }, "index_UserRole_User");
                        });
            });
        }
    }
}