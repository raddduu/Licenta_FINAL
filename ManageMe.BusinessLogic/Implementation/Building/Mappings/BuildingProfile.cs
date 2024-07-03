using AutoMapper;

namespace ManageMe.BusinessLogic
{
    public class BuildingProfile : Profile
    {
        public BuildingProfile()
        {
            CreateMap<Entities.Entities.Building, BuildingVM>();

            CreateMap<BuildingCreateModel, Entities.Entities.Building>();
        }

    }
}
