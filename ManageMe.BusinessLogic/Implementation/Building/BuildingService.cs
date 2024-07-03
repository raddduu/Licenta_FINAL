using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class BuildingService : BaseService
    {
        public BuildingService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public List<BuildingVM> GetAll()
        {
            var buildings = UnitOfWork.Buildings.Get();
            var buildingsVM = Mapper.Map<List<BuildingVM>>(buildings);
            return buildingsVM;
        }

        public BuildingVM GetById(int id)
        {
            var building = UnitOfWork.Buildings.Get().Where(b => b.Id == id).SingleOrDefault();
            var buildingVM = Mapper.Map<BuildingVM>(building);
            return buildingVM;
        }

        public bool Create(BuildingCreateModel buildingCreateVM)
        {
            try
            {
                var building = Mapper.Map<Building>(buildingCreateVM);
                UnitOfWork.Buildings.Insert(building);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(BuildingCreateModel buildingCreateVM)
        {
            try
            {
                var building = UnitOfWork.Buildings.Get().Where(b => b.Id == buildingCreateVM.Id);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var building = UnitOfWork.Buildings.Get().Where(b => b.Id == id).SingleOrDefault();
                if (building == null)
                {
                    return false;
                }
                UnitOfWork.Buildings.Delete(building);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
