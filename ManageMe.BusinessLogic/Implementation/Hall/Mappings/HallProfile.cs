using AutoMapper;
using ManageMe.Entities.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ManageMe.BusinessLogic
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<Hall, HallVM>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => String.Join(' ', new List<string?> { src.Number.ToString(),
                                                                                   src.AdditionalLetter,
                                                                                   src.Name }
                                                               .Where(lv => !lv.IsNullOrEmpty()))));

            CreateMap<HallCreateModel, Hall>();
        }
    }
}
