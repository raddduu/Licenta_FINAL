using Microsoft.AspNetCore.Mvc;
using ManageMe.Entities;
using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ManageMe.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageMe.Controllers
{
    public class GroupsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly GroupService _groupService;

        public GroupsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, GroupService groupService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _groupService = groupService;
        }

        [HttpGet]
        public IActionResult Index(int? batchId)
        {
            var viewGroups = _groupService.GetAllGroups(batchId);
            ViewBag.BatchId = batchId;
            return View(viewGroups);
        }

        [HttpGet]
        public ActionResult GetGroupsWhereUserIsLectorAtThisSubject(string userId, int subjectId)
        {
            var currentUserId = _userManager.GetUserId(User);

            // check if the user is a lector
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                return NotFound();
            }

            var userIsLector = _userManager.IsInRoleAsync(user, "Lector").Result;

            if (!userIsLector)
            {
                return Unauthorized();
            }

            var groups = _groupService.GetGroupsWhereUserIsLectorAtThisSubject(userId, subjectId);

            var result = new List<SelectListItem>();

            foreach (var group in groups)
            {
                result.Add(new SelectListItem
                {
                    Value = group.Id.ToString(),
                    Text = group.Number
                });
            }

            return Ok(result);
        }

        public IActionResult TeacherGroups(string? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId != userId)
            {
                return Unauthorized();
            }

            var teacherGroups = _groupService.GetGroupsByTeacherId(userId);

            return View(teacherGroups);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult ClassbookForSpecificGradingActivity(int? groupId, int? subjectId, int? gradingActivityId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            if (groupId == null || subjectId == null || gradingActivityId == null)
            {
                return NotFound();
            }

            var currentUserHasThePermissionToCoordinateThisGradingActivity =
                _groupService.CurrentUserHasThePermissionToCoordinateThisGradingActivity(currentUserId, (int)groupId, (int)subjectId, (int)gradingActivityId);

            if (!currentUserHasThePermissionToCoordinateThisGradingActivity)
            {
                return Unauthorized();
            }

            var getGroupGradingActivitiesByGradingActivityId = _groupService.GetGroupGradingActivitiesByGradingActivityId((int)groupId, (int)subjectId, (int)gradingActivityId);

            var classbook = _groupService.GetClassbook((int)groupId, (int)subjectId, (int)gradingActivityId);

            return View("Classbook", Tuple.Create(classbook, getGroupGradingActivitiesByGradingActivityId));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult Classbook(int? groupId, int? subjectId, int? activityId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            if (groupId == null || subjectId == null || activityId == null)
            {
                return NotFound();
            }

            var currentUserHasThePermissionToCoordinateThisActivity =
                _groupService.CurrentUserHasThePermissionToCoordinateThisActivity(currentUserId, (int)groupId, (int)subjectId, (int)activityId);

            if (!currentUserHasThePermissionToCoordinateThisActivity)
            {
                return Unauthorized();
            }

            var getGroupGradingActivitiesByActivityId = _groupService.GetGroupGradingActivitiesByActivityId((int)groupId, (int) subjectId, (int)activityId);

            var classbook = _groupService.GetClassbook((int)groupId, (int)subjectId, getGroupGradingActivitiesByActivityId.First().Id);

            return View(Tuple.Create(classbook, getGroupGradingActivitiesByActivityId));
        }

        [Authorize(Roles = "Student")]
        public IActionResult MyGroup(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId != userId)
            {
                return Unauthorized();
            }

            var userGroupId = _groupService.GetGroupByUserId(user.Id);

            if (userGroupId == null)
            {
                return View("MessageForUser", "At the moment you are not part of a group.\nPlease wait to be added");
            }

            return RedirectToAction("Details", new { id = userGroupId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupDetails = _groupService.GetCompleteGroup(id);
            if (groupDetails == null)
            {
                return NotFound();
            }

            return View(groupDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult Create(int? batchId)
        {
            if (batchId == null)
            {
                return NotFound();
            }

            ViewBag.BatchId = batchId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult Create(CreateGroupVM createGroup)
        {
            if (ModelState.IsValid)
            {
                _groupService.CreateGroup(createGroup);
                return RedirectToAction(nameof(Index), new { batchId = createGroup.BatchId });
            }

            return View(createGroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                _groupService.DeleteGroup(id.Value);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }

        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult AddStudentToGroup(int groupId, string studentId)
        {
            try
            {
                _groupService.AddStudentToGroup(groupId, studentId);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }

        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult RemoveStudentFromGroup(int groupId, string studentId)
        {
            try
            {
                _groupService.RemoveStudentFromGroup(groupId, studentId);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }

        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult AddTeacherToGroup(int groupId, string teacherId, int subjectId, int activityId)
        {
            try
            {
                _groupService.AddTeacherToGroup(groupId, teacherId, subjectId, activityId);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }

        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult RemoveTeacherFromGroup(int groupId, string teacherId, int subjectId, int activityId)
        {
            try
            {
                _groupService.RemoveTeacherFromGroup(groupId, teacherId, subjectId, activityId);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }


    }
}
