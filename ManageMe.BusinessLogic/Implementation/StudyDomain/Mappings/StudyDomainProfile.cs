using AutoMapper;
using ManageMe.Entities;

namespace ManageMe.BusinessLogic
{
    public class StudyDomainProfile : Profile
    {
        public StudyDomainProfile()
        {
            CreateMap<StudyDomain, DetailsStudyDomainVM>()
                .ForMember(dest => dest.StudyPlans, opt => opt.MapFrom(src => src.StudyPlans));

            CreateMap<StudyDomainCreateModel, StudyDomain>();
        }
    }
}
