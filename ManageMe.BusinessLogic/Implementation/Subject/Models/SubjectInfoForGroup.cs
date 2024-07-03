using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class SubjectInfoForGroup
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<Tuple<string, string>> CourseTeacherNames { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> LaboratoryTeacherNames { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> SeminaryTeacherNames { get; set; } = new List<Tuple<string, string>>();

        public bool HasCourse { get; set; }

        public bool HasLaboratory { get; set; }

        public bool HasSeminary { get; set; }

        public DetailsGradingCriterionVM SubjectGradingCriterion { get; set; } = new DetailsGradingCriterionVM();
    }
}
