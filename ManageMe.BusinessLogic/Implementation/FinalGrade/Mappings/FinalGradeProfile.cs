using AutoMapper;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class FinalGradeProfile : Profile
    {
        public FinalGradeProfile()
        {
            CreateMap<FinalGrade, FinalGradeVM>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.FirstName} {src.Student.LastName}"))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));

            CreateMap<FinalGradeCreateModel, FinalGrade>();

            CreateMap<ClassbookStudentVM, FinalGradeCreateModel>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.RoundedGrade));
        }
    }
}
