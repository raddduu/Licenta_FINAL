using AutoMapper;
using ManageMe.Entities;
using ManageMe.Entities.Enums;
using ManageMe.Common.Extensions;

namespace ManageMe.BusinessLogic
{
    public class StudyPlanProfile : Profile
    {
        public StudyPlanProfile()
        {
            CreateMap<StudyPlan, DetailsStudyPlanVM>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.StudyYear, opt => opt.MapFrom(src => (src.Semester + 1) / 2))
                .ForMember(dest => dest.StudySemester, opt => opt.MapFrom(src => src.Semester % 2 == 1 ? 1 : 2))
                .ForMember(dest => dest.SubjectTypeName, opt => opt.MapFrom(src => GetSubjectTypeName(src.SubjectType)))
                .ForMember(dest => dest.EvaluationFormName, opt => opt.MapFrom(src => GetEvaluationTypeName(src.EvaluationForm)));

            CreateMap<CreateStudyPlanVM, StudyPlan>();

            CreateMap<UpdateStudyPlanVM, StudyPlan>();

            CreateMap<StudyPlan, UpdateStudyPlanVM>();
        }

        public string GetSubjectTypeName(int subjectType)
        {
            switch (subjectType)
            {
                case 1:
                    return "Complementary";
                case 2:
                    return "Fundamental";
                case 3:
                    return "Specialization";
                default:
                    return "Unknown";
            }
        }
        public string GetEvaluationTypeName(int evaluationType)
        {
            switch (evaluationType)
            {
                case 1:
                    return "Exam";
                case 2:
                    return "Verification";
                default:
                    return "Unknown";
            }
        }

        public int GetSubjectType(string subjectTypeName)
        {
            switch (subjectTypeName)
            {
                case "Complementary":
                    return 1;
                case "Fundamental":
                    return 2;
                case "Specialization":
                    return 3;
                default:
                    return 0;
            }
        }

        public int GetEvaluationType(string evaluationTypeName)
        {
            switch (evaluationTypeName)
            {
                case "Exam":
                    return 1;
                case "Verification":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
