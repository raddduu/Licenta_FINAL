using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageMe.DataAccess;
using ManageMe.Entities;
using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Identity;
using ManageMe.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ManageMe.Web.Code.ExtensionMethods;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Authorization;
using ManageMe.Entities.Entities;

namespace ManageMe.Web.Controllers
{
    public class GradingCriteriaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GradingCriterionService _gradingCriterionService;

        public GradingCriteriaController(UserManager<ApplicationUser> userManager, GradingCriterionService gradingCriterionService)
        {
            _userManager = userManager;
            _gradingCriterionService = gradingCriterionService;
        }


        [Authorize(Roles = "Admin,Dean,Lector")]
        [HttpGet]
        public IActionResult Create(int groupId, int subjectId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return NotFound();
            }

            var currentUserIsLectorForThisGroupForThisSubject = _gradingCriterionService.CurrentUserIsLectorForThisGroupForThisSubject(currentUserId, groupId, subjectId);

            if (!currentUserIsLectorForThisGroupForThisSubject)
            {
                return Unauthorized();
            }

            var gradingCriterion = new CreateGradingCriterionModel
            {
                GroupId = groupId,
                SubjectId = subjectId
            };

            var gradingCriterionExists = _gradingCriterionService.GradingCriterionExists(subjectId, groupId);

            if (gradingCriterionExists)
            {
                gradingCriterion = _gradingCriterionService.GetGradingCriterionBySubjectIdAndGroupId(subjectId, groupId);

                return View(gradingCriterion);
            }

            return View(gradingCriterion);
        }

        [Authorize(Roles = "Admin,Dean,Lector")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGradingCriterionModel gradingCriterion)
        {
            if (!ModelState.IsValid)
            {
                return View(gradingCriterion);
            }

            var status = _gradingCriterionService.CreateGradingCriterion(gradingCriterion);

            if (status == false)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            return RedirectToAction("TeacherGroups", "Groups", new { userId = currentUserId});
        }
    }
}
