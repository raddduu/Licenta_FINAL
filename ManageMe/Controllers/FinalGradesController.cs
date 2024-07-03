using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManageMe.Entities;
using Microsoft.AspNetCore.Identity;
using ManageMe.BusinessLogic;

namespace ManageMe.Web.Controllers
{
    public class FinalGradesController : Controller
    {
        private readonly FinalGradeService _finalGradeService;
        private readonly GradeService _gradeService;
        private readonly GroupService _groupService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FinalGradesController(FinalGradeService finalGradeService, UserManager<ApplicationUser> userManager, GradeService gradeService, GroupService groupService)
        {
            _finalGradeService = finalGradeService;
            _userManager = userManager;
            _gradeService = gradeService;
            _groupService = groupService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult IndexForSubject(int subjectId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            if (!User.IsInRole("Admin") || !User.IsInRole("Dean"))
            {
                return Unauthorized();
            }

            var finalGrades = _finalGradeService.GetFinalGradesForSubject(subjectId);

            return View(finalGrades);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult IndexForSubejctGroup(int subjectId, int groupId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            if (!User.IsInRole("Admin") || !User.IsInRole("Dean"))
            {
                return Unauthorized();
            }

            var finalGrades = _finalGradeService.GetFinalGradesForSubjectForGroup(subjectId, groupId);

            return View(finalGrades);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult Create(FinalGradeCreateModel finalGrade)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _finalGradeService.UpdateFinalGrade(finalGrade);

            return RedirectToAction("IndexForSubject", new { subjectId = finalGrade.SubjectId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult AddFinalGradesFromClassbook(int? groupId, int? subjectId)
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

            var classbookStudentVMs = _gradeService.GetFinalGrades((int)groupId, (int)subjectId);

            var status = _finalGradeService.AddFinalGradesFromClassbook(classbookStudentVMs);

            if (!status)
            {
                return Unauthorized();
            }
            
            return View("MessageForUser", "Final grades were added successfully!");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Student")]
        public IActionResult IndexForStudent(string userId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var finalGrades = _finalGradeService.GetFinalGradesForStudent(userId);

            return View("Index",finalGrades);
        }
    }
}
