namespace ManageMe.BusinessLogic
{
    public class CreateGradingCriterionModel
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public decimal? CourseActivityPoints { get; set; }
        public decimal? MinimumPointsForPassCourseActivity { get; set; }
        public bool CourseActivityMattersForPassingTheSubject { get; set; }
        public decimal? CourseHomeworksPoints { get; set; }
        public decimal? MinimumPointsForPassCourseHomeworks { get; set; }
        public bool CourseHomeworksMattersForPassingTheSubject { get; set; }
        public decimal? CourseExamPoints { get; set; }
        public decimal? MinimumPointsForPassCourseExam { get; set; }
        public bool CourseExamMattersForPassingTheSubject { get; set; }
        public decimal? SeminaryActivityPoints { get; set; }
        public decimal? MinimumPointsForPassSeminaryActivity { get; set; }
        public bool SeminaryActivityMattersForPassingTheSubject { get; set; }
        public decimal? SeminaryHomeworksPoints { get; set; }
        public decimal? MinimumPointsForPassSeminaryHomeworks { get; set; }
        public bool SeminaryHomeworksMattersForPassingTheSubject { get; set; }
        public decimal? SeminaryExamPoints { get; set; }
        public decimal? MinimumPointsForPassSeminaryExam { get; set; }
        public bool SeminaryExamMattersForPassingTheSubject { get; set; }
        public decimal? LaboratoryActivityPoints { get; set; }
        public decimal? MinimumPointsForPassLaboratoryActivity { get; set; }
        public bool LaboratoryActivityMattersForPassingTheSubject { get; set; }
        public decimal? LaboratoryHomeworksPoints { get; set; }
        public decimal? MinimumPointsForPassLaboratoryHomeworks { get; set; }
        public bool LaboratoryHomeworksMattersForPassingTheSubject { get; set; }
        public decimal? LaboratoryExamPoints { get; set; }
        public decimal? MinimumPointsForPassLaboratoryExam { get; set; }
        public bool LaboratoryExamMattersForPassingTheSubject { get; set; }
        public decimal? BonusPoints { get; set; }
        public bool BonusMattersForPassingTheSubject { get; set; }
        public decimal? MinimumPointsForPassSubject { get; set; }
    }
}
