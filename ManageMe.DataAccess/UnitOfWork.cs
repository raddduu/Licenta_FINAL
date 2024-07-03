using ManageMe.Common.Interfaces;
using ManageMe.DataAccess;
using ManageMe.Entities;
using ManageMe.Entities.Entities;

namespace ManageMe.DataAccess
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext Context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.Context = context;
        }


        private IRepository<Activity> activities;
        public IRepository<Activity> Activities => activities ?? (activities = new BaseRepository<Activity>(Context));

        private IRepository<ApplicationRole> applicationroles;
        public IRepository<ApplicationRole> ApplicationRoles => applicationroles ?? (applicationroles = new BaseRepository<ApplicationRole>(Context));

        private IRepository<ApplicationUser> applicationusers;
        public IRepository<ApplicationUser> ApplicationUsers => applicationusers ?? (applicationusers = new BaseRepository<ApplicationUser>(Context));

        private IRepository<Channel> channels;
        public IRepository<Channel> Channels => channels ?? (channels = new BaseRepository<Channel>(Context));

        private IRepository<ChannelRequest> channelrequests;
        public IRepository<ChannelRequest> ChannelRequests => channelrequests ?? (channelrequests = new BaseRepository<ChannelRequest>(Context));

        private IRepository<ChannelUser> channelusers;
        public IRepository<ChannelUser> ChannelUsers => channelusers ?? (channelusers = new BaseRepository<ChannelUser>(Context));

        private IRepository<Grade> grades;
        public IRepository<Grade> Grades => grades ?? (grades = new BaseRepository<Grade>(Context));

        private IRepository<Group> groups;
        public IRepository<Group> Groups => groups ?? (groups = new BaseRepository<Group>(Context));

        private IRepository<Message> messages;
        public IRepository<Message> Messages => messages ?? (messages = new BaseRepository<Message>(Context));

        private IRepository<Methodology> methodologies;
        public IRepository<Methodology> Methodologies => methodologies ?? (methodologies = new BaseRepository<Methodology>(Context));

        private IRepository<StudyDomain> studydomains;
        public IRepository<StudyDomain> StudyDomains => studydomains ?? (studydomains = new BaseRepository<StudyDomain>(Context));

        private IRepository<StudyPlan> studyplans;
        public IRepository<StudyPlan> StudyPlans => studyplans ?? (studyplans = new BaseRepository<StudyPlan>(Context));

        private IRepository<Subject> subjects;
        public IRepository<Subject> Subjects => subjects ?? (subjects = new BaseRepository<Subject>(Context));

        private IRepository<TeacherPermission> teacherPermissions;
        public IRepository<TeacherPermission> TeacherPermissions => teacherPermissions ?? (teacherPermissions = new BaseRepository<TeacherPermission>(Context));

        private IRepository<GradingCriterion> gradingCriteria;
        public IRepository<GradingCriterion> GradingCriteria => gradingCriteria ?? (gradingCriteria = new BaseRepository<GradingCriterion>(Context));

        //private IRepository<SubjectActivityFrequency> subjectActivityFrequencies;
        //public IRepository<SubjectActivityFrequency> SubjectActivityFrequencies => subjectActivityFrequencies ?? (subjectActivityFrequencies = new BaseRepository<SubjectActivityFrequency>(Context));

        private IRepository<GradingActivity> gradingActivities;
        public IRepository<GradingActivity> GradingActivities => gradingActivities ?? (gradingActivities = new BaseRepository<GradingActivity>(Context));

        private IRepository<GradingCriterionSubject> gradingCriterionSubjects;
        public IRepository<GradingCriterionSubject> GradingCriterionSubjects => gradingCriterionSubjects ?? (gradingCriterionSubjects = new BaseRepository<GradingCriterionSubject>(Context));

        private IRepository<VwStudentGradeForSubject> studentGradeSubjects;
        public IRepository<VwStudentGradeForSubject> StudentGradeSubjects => studentGradeSubjects ?? (studentGradeSubjects = new BaseRepository<VwStudentGradeForSubject>(Context));

        private IRepository<FinalGrade> finalGrades;
        public IRepository<FinalGrade> FinalGrades => finalGrades ?? (finalGrades = new BaseRepository<FinalGrade>(Context));

        private IRepository<Building> buildings;
        public IRepository<Building> Buildings => buildings ?? (buildings = new BaseRepository<Building>(Context));

        private IRepository<Hall> halls;
        public IRepository<Hall> Halls => halls ?? (halls = new BaseRepository<Hall>(Context));

        private IRepository<Schedule> schedules;
        public IRepository<Schedule> Schedules => schedules ?? (schedules = new BaseRepository<Schedule>(Context));

        private IRepository<VwScheduleColor> scheduleColors;
        public IRepository<VwScheduleColor> ScheduleColors => scheduleColors ?? (scheduleColors = new BaseRepository<VwScheduleColor>(Context));

        private IRepository<Batch> batches;
        public IRepository<Batch> Batches => batches ?? (batches = new BaseRepository<Batch>(Context));

        private IRepository<Chapter> chapters;

        public IRepository<Chapter> Chapters => chapters ?? (chapters = new BaseRepository<Chapter>(Context));

        private IRepository<Detail> details;

        public IRepository<Detail> Details => details ?? (details = new BaseRepository<Detail>(Context));

        private IRepository<Paragraph> paragraphs;

        public IRepository<Paragraph> Paragraphs => paragraphs ?? (paragraphs = new BaseRepository<Paragraph>(Context));

        private IRepository<Section> sections;

        public IRepository<Section> Sections => sections ?? (sections = new BaseRepository<Section>(Context));

        private IRepository<Article> articles;

        public IRepository<Article> Articles => articles ?? (articles = new BaseRepository<Article>(Context));

        private IRepository<Provision> provisions;

        public IRepository<Provision> Provisions => provisions ?? (provisions = new BaseRepository<Provision>(Context));

        private IRepository<Semester> semesters;

        public IRepository<Semester> Semesters => semesters ?? (semesters = new BaseRepository<Semester>(Context));

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
