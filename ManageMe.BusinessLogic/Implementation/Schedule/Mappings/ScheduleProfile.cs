using AutoMapper;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleVM>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.ShortName))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.Activity.Name))
                .ForMember(dest => dest.GroupNumber, opt => opt.MapFrom(src => Int32.Parse(src.Group.Number)))
                .ForMember(dest => dest.HallName, opt => opt.MapFrom(src => $"{src.Hall.Number}{src.Hall.AdditionalLetter} {src.Hall.Name}"))
                .ForMember(dest => dest.StartHour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(dest => dest.StartMinute, opt => opt.MapFrom(src => src.Minute))
                .ForMember(dest => dest.EndHour, opt => opt.MapFrom(src => src.Hour + src.Duration))
                .ForMember(dest => dest.EndMinute, opt => opt.MapFrom(src => src.Minute));

            CreateMap<ScheduleCreateModel, Schedule>();
        }

    }
}
