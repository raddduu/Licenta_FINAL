using ManageMe.Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageMe.BusinessLogic
{
    public class HallService : BaseService
    {
        public HallService(ServiceDependencies serviceDependencies): base(serviceDependencies) { }

        public bool CreateHall(HallCreateModel hall)
        {
            try
            {
                var hallToAdd = Mapper.Map<HallCreateModel, Hall>(hall);

                UnitOfWork.Halls.Insert(hallToAdd);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteHall(int id)
        {
            try
            {
                var hall = UnitOfWork.Halls.Get().SingleOrDefault(h => h.Id == id);

                if (hall == null)
                {
                    return false;
                }

                UnitOfWork.Halls.Delete(hall);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SelectListItem> GetAllHalls(int? distributionId, int? day, int? hour, int? activityId, int? duration, int? frequencyId, int? groupId)
        {
            var minimumRequieredCapacity = 0;

            if (distributionId != null && groupId != null)
            {
                var numberOfStudents = UnitOfWork.Groups.Get()
                    .Where(g => g.Id == groupId)
                    .Include(g => g.Users)
                    .SingleOrDefault()
                    ?.Users.Count ?? 0;

                switch (distributionId)
                {
                    case 1:
                        minimumRequieredCapacity = numberOfStudents;
                        break;
                    case 2:
                        minimumRequieredCapacity = numberOfStudents / 2;
                        break;
                    case 3:
                        minimumRequieredCapacity = numberOfStudents / 2;
                        break;
                    default:
                        return new List<SelectListItem>();
                }
            }

            if (activityId == 4)
            {
                minimumRequieredCapacity = 60;  //cursul are nevoie de o sala mai mare
            }

            var requieresComputers = false;

            if (activityId != null)
            {
                if (activityId == 3)
                {
                    requieresComputers = true;
                }
            }

            //var busyHalls = UnitOfWork.Schedules.Get()
            //            .Where(s => s.DayOfWeek == day
            //                    && ((s.Hour >= hour && s.Hour < hour + duration)
            //                        || (s.Hour + s.Duration > hour && s.Hour + s.Duration <= hour + duration))
            //                    && (frequencyId == 1 || s.ActivityFrequencyId == frequencyId || s.ActivityFrequencyId == 1))
            //            .Select(s => s.HallId);

            var availableHalls = UnitOfWork.Halls.Get()
                .Where(h => !(UnitOfWork.Schedules.Get()
                        .Where(s => s.DayOfWeek == day
                                && ((s.Hour >= hour && s.Hour < hour + duration)
                                    || (s.Hour + s.Duration > hour && s.Hour + s.Duration <= hour + duration))
                                && (frequencyId == 1 || s.ActivityFrequencyId == frequencyId || s.ActivityFrequencyId == 1)))
                        .Select(s => s.HallId)
                    .Contains(h.Id))
                .Where(h => h.Capacity >= minimumRequieredCapacity && (!requieresComputers || h.HasComputers))
                .Distinct()
                .OrderByDescending(h => h.Floor)
                    .ThenBy(h => h.Number);

            var result = new List<SelectListItem>();

            foreach (var hall in availableHalls)
            {
                result.Add(new SelectListItem {
                    Text = $"{hall.Number}{hall.AdditionalLetter} {hall.Name}",
                    Value = hall.Id.ToString()
                });
            }
            
            return result;
        }

        public List<HallVM> GetAllHallsInBuilding(int buildingId)
        {
            var halls = UnitOfWork.Halls.Get()
                            .Where(h => h.BuildingId == buildingId)
                            .Include(h => h.Schedules)
                            .OrderByDescending(h => h.Floor)
                            .ThenBy(h => h.Number);

            return Mapper.Map<List<HallVM>>(halls);
            
        }

        public HallVM GetHallById(int id)
        {
            var hall = UnitOfWork.Halls.Get()
                            .Where(h => h.Id == id)
                            .Include(h => h.Schedules)
                            .FirstOrDefault();

            return Mapper.Map<HallVM>(hall);
        }

        public bool Update(HallCreateModel hall)
        {
            try
            {
                var hallToUpdate = Mapper.Map<HallCreateModel, Hall>(hall);

                UnitOfWork.Halls.Update(hallToUpdate);
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
