using AutoMapper;
using ManageMe.Entities;

namespace ManageMe.BusinessLogic
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, WeekGrade>();
        }
    }
}
