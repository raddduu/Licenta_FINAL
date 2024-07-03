using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageMe.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ScheduleService _scheduleService;
        private readonly GroupService _groupService;
        private readonly UserService _userService;

        public SchedulesController(ScheduleService scheduleService, GroupService groupService, UserService userService)
        {
            _scheduleService = scheduleService;
            _groupService = groupService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index(int? groupId, string? teacherId, int? hallId, string? scope)
        {
            if (scope == null)
            {
                scope = "group";
            }

            else if (scope != "group" && scope != "teacher" && scope != "hall")
            {
                return NotFound();

            }

            var schedules = _scheduleService.GetScheduleVMs(groupId, null, null, teacherId, hallId, scope);

            ViewBag.Scope = scope;

            return View(Tuple.Create(schedules.Item1, schedules.Item2));
        }

        [HttpGet]
        public IActionResult Create(int groupId, int dayOfWeek, int hour)
        {
            var schedule = new ScheduleCreateModel
            {
                GroupId = groupId,
                DayOfWeek = dayOfWeek,
                Hour = hour
            };

            return PartialView(schedule);
        }

        [HttpPost]
        public IActionResult Create(ScheduleCreateModel scheduleCreateModel)
        {
            if (ModelState.IsValid)
            {
                var status = _scheduleService.AddNewSchedule(scheduleCreateModel);

                return Ok(new
                {
                    succses = status
                });
            }

            return Ok(new
            {
                succses = false
            });
        }

        [HttpPost]
        public IActionResult Edit(ScheduleCreateModel scheduleEditModel)
        {
            if (ModelState.IsValid)
            {
                var status = _scheduleService.UpdateSchedule(scheduleEditModel);

                return Ok(new
                {
                    succses = status
                });
            }

            return Ok(new
            {
                succses = false
            });
        }

        [HttpPost]
        public IActionResult Delete(int activityId, int groupId, int subjectId, int hallId, string teacherId, int frequencyId, int distributionId)
        {
            var status = _scheduleService.DeleteSchedule(subjectId, groupId, hallId, activityId, teacherId, frequencyId, distributionId);

            return Ok(new
            {
                succses = status
            });
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllActivities(int subjectId, int groupId)
        {
            var items = _scheduleService.GetAllActivities(subjectId, groupId);

            return Ok(items);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllActivityFrequencies()
        {
            var items = _scheduleService.GetAllActivityFrequencies();

            return Ok(items);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllDistributions()
        {
            var result = new List<SelectListItem> {
                new SelectListItem
                {
                    Value=1.ToString(),
                    Text="All group"
                },
                new SelectListItem
                {
                    Value=2.ToString(),
                    Text="First Half"
                },
                new SelectListItem
                {
                    Value=3.ToString(),
                    Text="Second Half"
                }
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult UserSchedule(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var studentGroupId = _groupService.GetStudentGroupId(userId);

            if (studentGroupId == null)
            {
                var userIsLectorOrAssistant = _userService.UserHasRole(userId, "Lector") || _userService.UserHasRole(userId, "Assistant");

                if (userIsLectorOrAssistant)
                {
                    return RedirectToAction("Index", new { teacherId = userId, scope = "teacher" });
                }

                return NotFound();
            }

            return RedirectToAction("Index", new { groupId = studentGroupId, scope = "group"});
        }
    }
}