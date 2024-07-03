using Microsoft.AspNetCore.Mvc;
using ManageMe.Entities;
using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace ManageMe.Web.Controllers
{
    public class GradesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GradeService _gradeService;
        private readonly GroupService _groupService;

        public GradesController(UserManager<ApplicationUser> userManager, GradeService gradeService, GroupService groupService)
        {
            _userManager = userManager;
            _gradeService = gradeService;
            _groupService = groupService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult UpdateGrades(List<GradeCreateModel> gradeCreateModels, int subjectId, int gradingActivityId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            if (gradeCreateModels == null || gradeCreateModels.Count == 0)
            {
                return Ok();
            }

            var allStudentsAreInTheSameGroup = _groupService.AllStudentsAreInTheSameGroup(gradeCreateModels.Select(gcm => gcm.StudentId).ToList());

            if (!allStudentsAreInTheSameGroup)
            {
                return Unauthorized();
            }

            var groupId = _groupService.GetGroupByUserId(gradeCreateModels.First().StudentId);

            if (groupId == null)
            {
                return NotFound();
            }

            var currentUserHasThePermissionToCoordinateThisGradingActivity =
                _groupService.CurrentUserHasThePermissionToCoordinateThisGradingActivity(currentUserId, (int)groupId, subjectId, gradingActivityId);

            if (!currentUserHasThePermissionToCoordinateThisGradingActivity)
            {
                return Unauthorized();
            }

            var grades = _gradeService.UpdateGrades(gradeCreateModels, subjectId, gradingActivityId);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult FinalGrades(int? groupId, int? subjectId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            if (groupId == null || subjectId == null)
            {
                return NotFound();
            }

            //var currentUserHasThePermissionToCoordinateThisSubject =
            //    _groupService.CurrentUserHasThePermissionToCoordinateThisSubject(currentUserId, (int)groupId, (int)subjectId);

            //if (!currentUserHasThePermissionToCoordinateThisSubject)
            //{
            //    return Unauthorized();
            //}

            var finalGrades = _gradeService.GetFinalGrades((int)groupId, (int)subjectId);

            return View(finalGrades);
        }
    }
}
