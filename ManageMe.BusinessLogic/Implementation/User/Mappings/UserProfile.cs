using AutoMapper;
using ManageMe.Entities;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserMinimalInfo>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Name)))
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureByteArray, opt => opt.MapFrom(u => u.ProfilePicture));

            CreateMap<ApplicationUser, StudentGradesForSubjectVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Name)))
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureByteArray, opt => opt.MapFrom(u => u.ProfilePicture));


            CreateMap<TeacherPermission, UserMinimalInfo>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Teacher.Email))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Teacher.Roles.Select(r => r.Name)))
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureByteArray, opt => opt.MapFrom(u => u.Teacher.ProfilePicture));

            CreateMap<PersonalDataUser, ApplicationUser>();

            CreateMap<ApplicationUser, PersonalDataUser>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureByteArray, opt => opt.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.GroupNumber, opt => opt.MapFrom(src => src.Group != null ? src.Group.Number : null));
        }
    }
}
