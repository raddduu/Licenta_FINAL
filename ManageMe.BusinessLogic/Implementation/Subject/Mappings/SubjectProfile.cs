using AutoMapper;
using ManageMe.Common.Extensions;

namespace ManageMe.BusinessLogic.Implementation
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile() { 
            CreateMap<Entities.Subject, DetailsSubjectVM>()
                .ForMember(dest => dest.Lectors, opt => opt.MapFrom(src => src.TeacherPermissions
                            .Where(tp => tp.ActivityId == 4).Select(tp => tp.Teacher)))
                .ForMember(dest => dest.LaboratoryAssistants, opt => opt.MapFrom(src => src.TeacherPermissions
                            .Where(tp => tp.ActivityId == 3).Select(tp => tp.Teacher)))
                .ForMember(dest => dest.SeminaryAssistants, opt => opt.MapFrom(src => src.TeacherPermissions
                            .Where(tp => tp.ActivityId == 6).Select(tp => tp.Teacher)))
                .ForMember(dest => dest.StudyPlans, opt => opt.MapFrom(src => src.StudyPlans.Select(sp => sp.StudyDomain.Name)))
                .ForMember(dest => dest.Channels, opt => opt.MapFrom(src => src.Channels));

            CreateMap<SubjectCreateModel, Entities.Subject>();

            CreateMap<Entities.Subject, UpdateSubjectVM>();

            CreateMap<UpdateSubjectVM, Entities.Subject>()
                .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.Name.GetAcronimFromSubjectName()));
        }
    }
}
