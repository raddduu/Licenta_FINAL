using ManageMe.Common.Extensions;
using ManageMe.Entities.Entities;
using ManageMe.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ManageMe.Common;

namespace ManageMe.BusinessLogic
{
    public class ScheduleService : BaseService
    {
        public ScheduleService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }

        public bool ScheduleExists()
        {
            return UnitOfWork.Schedules.Get().Any();
        }

        public Tuple<List<ScheduleVM>, List<Tuple<string, string>>> GetScheduleVMs(int? groupId, int? subjectId, int? activityId, string? teacherId, int? hallId, string scope)
        {
            var colorCodesWithTextColors = new List<Tuple<string, bool>>
            {
                Tuple.Create("#020887", false),
                Tuple.Create("#8EB8E5", true),
                Tuple.Create("#334195", false),
                Tuple.Create("#3E7AE9", true),
                Tuple.Create("#0E7C7B", false),
                Tuple.Create("#114B5F", false),
                Tuple.Create("#B8E1FF", true),
                Tuple.Create("#A9FFF7", true),
                Tuple.Create("#82ABA1", true),
                Tuple.Create("#3D52D5", false),
                Tuple.Create("#090C9B", false),
                Tuple.Create("#0C7489", false),
            };

            var schedulesFromDb = UnitOfWork.Schedules.Get()
                .Include(s => s.Group)
                .Include(s => s.Subject)
                .Include(s => s.Activity)
                .Include(s => s.Teacher)
                .Include(s => s.Hall)
                .Where(s =>
                    (groupId == null || s.GroupId == groupId) &&
                    (subjectId == null || s.SubjectId == subjectId) &&
                    (activityId == null || s.ActivityId == activityId) &&
                    (teacherId == null || s.TeacherId == teacherId) &&
                    (hallId == null || s.HallId == hallId)
                );

            var schedulesWithColorDb = schedulesFromDb
                .Join(
                    UnitOfWork.ScheduleColors.Get(),
                    s => new
                    {
                        s.SubjectId,
                        s.TeacherId,
                        s.ActivityId,
                        s.GroupId,
                        s.HallId,
                        s.ActivityFrequencyId,
                        s.DistributionId
                    },
                    color => new
                    {
                        color.SubjectId,
                        color.TeacherId,
                        color.ActivityId,
                        color.GroupId,
                        color.HallId,
                        color.ActivityFrequencyId,
                        color.DistributionId
                    },
                    (s, color) => new
                    {
                        Schedule = s,
                        Color = color.Color
                    }
                );

            var fullInfoGatheredSchedules = schedulesWithColorDb
                .OrderBy(s => scope == "group"
                    ? s.Schedule.Group.Number
                    : scope == "teacher"
                        ? s.Schedule.Teacher.LastName
                        : scope == "hall"
                            ? s.Schedule.Hall.Number.ToString()
                            : s.Schedule.Group.Number)
                .ThenBy(s => s.Schedule.DayOfWeek)
                .ThenBy(s => s.Schedule.Hour)
                .ThenBy(s => s.Schedule.Minute)
                .ThenBy(s => s.Schedule.ActivityFrequencyId)
                .ThenBy(s => s.Schedule.DistributionId);

            var nonColredSchedules = fullInfoGatheredSchedules
                .Select(s => new ScheduleVM
                {
                    SubjectId = s.Schedule.SubjectId,
                    TeacherId = s.Schedule.TeacherId,
                    ActivityId = s.Schedule.ActivityId,
                    GroupId = s.Schedule.GroupId,
                    HallId = s.Schedule.HallId,
                    DistributionId = s.Schedule.DistributionId,
                    ActivityFrequencyId = s.Schedule.ActivityFrequencyId,
                    TeacherName = $"{s.Schedule.Teacher.FirstName} {s.Schedule.Teacher.LastName}",
                    HallName = $"{s.Schedule.Hall.Number}{s.Schedule.Hall.AdditionalLetter} {s.Schedule.Hall.Name}",
                    SubjectName = s.Schedule.Subject.ShortName,
                    GroupNumber = Int32.Parse(s.Schedule.Group.Number),
                    ActivityName = s.Schedule.Activity.Name,
                    StartHour = s.Schedule.Hour,
                    StartMinute = s.Schedule.Minute,
                    EndHour = s.Schedule.Hour + s.Schedule.Duration,
                    EndMinute = s.Schedule.Minute + s.Schedule.Duration,
                    DayOfWeek = s.Schedule.DayOfWeek,
                    ColorCode = s.Color,
                    //Color = colorCodesWithTextColors[s.Color].Item1,
                    //HasLightModeText = colorCodesWithTextColors[s.Color].Item2
                })
                .ToList();

            var scheduleGraph = new Graph<ScheduleVM>(nonColredSchedules.Count());

            for (int i = 0; i < nonColredSchedules.Count(); i++)
            {
                scheduleGraph.AddNode(new Node<ScheduleVM>(nonColredSchedules[i]), i);
            }

            for (int i = 0; i < nonColredSchedules.Count(); i++)
            {
                for (int j = i + 1; j < nonColredSchedules.Count(); j++)
                {
                    var schedule1 = nonColredSchedules[i];
                    var schedule2 = nonColredSchedules[j];

                    // check if the schedules are on the same day of the week and if they touch each other (one ends when the other starts)
                    if (schedule1.GroupId == schedule2.GroupId &&
                        schedule1.DayOfWeek == schedule2.DayOfWeek
                        && ((schedule1.StartHour == schedule2.EndHour)
                        || (schedule1.EndHour == schedule2.StartHour)))
                    {
                        scheduleGraph.AddEdge(scheduleGraph.Nodes[i], scheduleGraph.Nodes[j]);
                    }

                    // check if the schedules are on two consecutive days of the week and if they overlap (one starts when the other is still going)
                    if (schedule1.GroupId == schedule2.GroupId &&
                        ((schedule1.DayOfWeek == schedule2.DayOfWeek - 1 || schedule1.DayOfWeek == 6 && schedule2.DayOfWeek == 0)
                         && ((schedule1.StartHour < schedule2.StartHour && schedule1.EndHour > schedule2.StartHour)
                         || (schedule2.StartHour < schedule1.StartHour && schedule2.EndHour > schedule1.StartHour))))
                    {
                        scheduleGraph.AddEdge(scheduleGraph.Nodes[i], scheduleGraph.Nodes[j]);
                    }

                    // check if the schedules are on the same day of the week and if they overlap (one starts when the other is still going)
                    if (schedule1.GroupId == schedule2.GroupId &&
                        schedule1.DayOfWeek == schedule2.DayOfWeek
                        && ((schedule1.StartHour <= schedule2.StartHour && schedule1.EndHour >= schedule2.StartHour)
                        || (schedule2.StartHour <= schedule1.StartHour && schedule2.EndHour >= schedule1.StartHour)))
                    {
                        scheduleGraph.AddEdge(scheduleGraph.Nodes[i], scheduleGraph.Nodes[j]);
                    }
                }
            }

            int maximumColor = 4; // graf planar

            var coloring_result = scheduleGraph.GraphColoring(maximumColor);

            if (!coloring_result)
            {
                foreach (var schedule in nonColredSchedules)
                {
                    schedule.Color = colorCodesWithTextColors[0].Item1;
                    schedule.HasLightModeText = colorCodesWithTextColors[0].Item2;
                }
            }

            else
            {
                for (int i = 0; i < nonColredSchedules.Count(); i++)
                {
                    nonColredSchedules[i].ColorCode = scheduleGraph.Nodes[i].Data.ColorCode - 1;
                }

                foreach (var schedule in nonColredSchedules)
                {
                    schedule.Color = colorCodesWithTextColors[schedule.ColorCode].Item1;
                    schedule.HasLightModeText = colorCodesWithTextColors[schedule.ColorCode].Item2;
                }
            }


            List<Tuple<string, string>> scopeNames = new List<Tuple<string, string>>();

            switch (scope)
            {
                case "group":
                    scopeNames = UnitOfWork.Groups.Get()
                        .Where(g => groupId == null || g.Id == groupId)
                        .OrderBy(g => g.Number)
                        .Select(g => Tuple.Create(g.Number, g.Id.ToString())).ToList();
                    break;
                case "teacher":
                    var assistantRole = UnitOfWork.ApplicationRoles.Get().Where(r => r.Name == "Assistant").SingleOrDefault();
                    var lectorRole = UnitOfWork.ApplicationRoles.Get().Where(r => r.Name == "Lector").SingleOrDefault();
                    scopeNames = UnitOfWork.ApplicationUsers.Get()
                        .Where(u => teacherId == null || u.Id == teacherId)
                        .OrderBy(u => u.LastName)
                        .Where(u => u.Roles.Contains(assistantRole) || u.Roles.Contains(lectorRole))
                        .Select(u => Tuple.Create($"{u.FirstName} {u.LastName}", u.Id)).ToList();
                    break;
                case "hall":
                    scopeNames = UnitOfWork.Halls.Get().OrderBy(h => h.Number).Select(h => Tuple.Create($"{h.Number}{h.AdditionalLetter} {h.Name}", h.Id.ToString())).ToList();
                    break;
            }

            return Tuple.Create(Mapper.Map<List<ScheduleVM>>(nonColredSchedules), scopeNames);
        }

        public bool AddNewSchedule(ScheduleCreateModel schedule)
        {
            try
            {
                var scheduleToAdd = Mapper.Map<ScheduleCreateModel, Schedule>(schedule);

                UnitOfWork.Schedules.Insert(scheduleToAdd);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSchedule(ScheduleCreateModel schedule)
        {
            try
            {
                var scheduleToUpdate = Mapper.Map<ScheduleCreateModel, Schedule>(schedule);

                UnitOfWork.Schedules.Update(scheduleToUpdate);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSchedule(int subjectId, int groupId, int hallId, int activityId, string teacherId, int frequencyId, int distributionId)
        {
            try
            {
                var dbSchedule = UnitOfWork.Schedules.Get()
                .SingleOrDefault(s => s.TeacherId == teacherId
                    && s.ActivityId == activityId
                    && s.HallId == hallId
                    && s.GroupId == groupId
                    && s.SubjectId == subjectId
                    && s.ActivityFrequencyId == frequencyId
                    && s.DistributionId == distributionId);

                if (dbSchedule == null)
                {
                    return false;
                }

                UnitOfWork.Schedules.Delete(dbSchedule);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SelectListItem> GetAllActivities(int subjectId, int groupId)
        {
            var studyDomainId = UnitOfWork.Groups.Get()
                .Where(g => g.Id == groupId)
                .Include(g => g.Batch)
                .Select(g => g.Batch.StudyDomainId).SingleOrDefault();

            var activityNames = UnitOfWork.Activities.Get().Select(a => a.Name).ToList();

            var studyPlanForThisSubject = UnitOfWork.StudyPlans.Get()
                .Where(sp => sp.SubjectId == subjectId && sp.StudyDomainId == studyDomainId)
                .SingleOrDefault();

            if (studyPlanForThisSubject == null)
            {
                return new List<SelectListItem>();
            }

            var activitiesAtThisSubject = new List<string>();

            foreach (var property in studyPlanForThisSubject.GetType().GetProperties())
            {
                if (property.GetValue(studyPlanForThisSubject) != null
                    && property.PropertyType.Name == typeof(Int32).Name
                    && (int)property.GetValue(studyPlanForThisSubject) != 0)
                {
                    if (property.Name.Contains("Credits"))
                    {
                        activitiesAtThisSubject.Add(property.Name.Replace("Credits", ""));
                    }
                }
            }

            var result = new List<SelectListItem>();

            foreach (var activity in activitiesAtThisSubject.Intersect(activityNames.Except(new List<string> { "Project" })))
            {
                result.Add(new SelectListItem
                {
                    Value = UnitOfWork.Activities.Get().Where(a => a.Name == activity).Select(a => a.Id).SingleOrDefault().ToString(),
                    Text = activity
                });
            }

            result = result.OrderByDescending(r => r.Text).ToList();

            return result;
        }

        public List<SelectListItem> GetAllActivityFrequencies()
        {
            var activityFrequencies = Enum.GetValues(typeof(ActivityFrequencyEnum)).Cast<ActivityFrequencyEnum>();

            var selectListItems = activityFrequencies.Select(activityFrequency => new SelectListItem
            {
                Value = ((int)activityFrequency).ToString(),
                Text = activityFrequency.GetDisplayName()
            }).ToList();

            return selectListItems;

        }

        //public List<ScheduleVM>? GetStudentSchedule(string userId)
        //{
        //    var groupId = UnitOfWork.ApplicationUsers.Get().Where(s => s.Id == userId).Select(s => s.GroupId).SingleOrDefault();

        //    if (groupId == null)
        //    {
        //        return null;
        //    }

        //    var Schedules

        //}
    }
}