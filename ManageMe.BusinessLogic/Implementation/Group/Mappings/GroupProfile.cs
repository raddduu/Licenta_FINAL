using AutoMapper;
using ManageMe.Entities;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class GroupProfile : Profile
    {
        public GroupProfile ()
        {
            CreateMap<Group, DetailsGroupVM>()
                .ForMember(dest => dest.StudyYear, opt => opt.MapFrom(src => src.Batch.Year));


            CreateMap<Group, IndexGroupVM>()
                .ForMember(dest => dest.StudyYear, opt => opt.MapFrom(src => src.Batch.Year))
                .ForMember(dest => dest.StudyDomainName, opt => opt.MapFrom(src => src.Batch.StudyDomain.Name));

            CreateMap<Group, Classbook>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Users));

            CreateMap<CreateGroupVM, Group>();

            CreateMap<TeacherPermission, ActivityVM>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.ActivityTypeId, opt => opt.MapFrom(src => src.ActivityId))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.Activity.Name));

            CreateMap<Group, TeacherGroupVM>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Users))
                .ForMember(dest => dest.StudyDomainName, opt => opt.MapFrom(src => src.Batch.StudyDomain.Name))
                .ForMember(dest => dest.ActivityList, opt => opt.MapFrom(src => src.TeacherPermissions))
                .ForMember(dest => dest.StudyYear, opt => opt.MapFrom(src => src.Batch.Year));

            CreateMap<Group, MinimalGroupInfo>();
        }
    }
}
