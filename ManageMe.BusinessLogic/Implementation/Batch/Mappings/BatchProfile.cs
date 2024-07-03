using AutoMapper;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class BatchProfile : Profile
    {
        public BatchProfile()
        {
            CreateMap<BatchCreateModel, Batch>();

            CreateMap<Batch, BatchVM>()
                .ForMember(dest => dest.StudyDomainName, opt => opt.MapFrom(src => src.StudyDomain.Name))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Year * 10 + Int32.Parse(src.Number)));
        }

    }
}
