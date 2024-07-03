using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Web.Controllers
{
    public class StudyPlanController : Controller
    {
        private readonly StudyPlanService _studyPlanService;

        public StudyPlanController(StudyPlanService studyPlanService)
        {
            _studyPlanService = studyPlanService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index(int studyDomainId)
        {
            var viewStudyPlans = _studyPlanService.GetAllStudyPlans(studyDomainId);
            return View(viewStudyPlans);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult AddNewStudyPlan(int studyDomainId, int semester, int optionality)
        {
            var model = new CreateStudyPlanVM
            {
                StudyDomainId = studyDomainId,
                Semester = semester,
                SubjectOptionality = optionality
            };
            return PartialView("~/Views/StudyDomains/AddStudyPlan.cshtml", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult Create (CreateStudyPlanVM createStudyPlanVM)
        {
            try
            {
                _studyPlanService.AddNewStudyPlan(createStudyPlanVM);
                return Ok(new {
                    success = true,
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false,
                });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult DeleteStudyPlan(int studyDomainId, int subjectid)
        {
            try
            {
                _studyPlanService.DeleteStudyPlan(studyDomainId, subjectid);
                return Ok(new
                {
                    success = true,
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false,
                });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult EditStudyPlan(int studyDomainId, int subjectId)
        {
            var studyPlan = _studyPlanService.GetStudyPlan(studyDomainId, subjectId);

            if (studyPlan == null)
            {
                return NotFound();
            }

            ViewBag.SubjectTypeNames = _studyPlanService.GetSubjectTypeNames();
            ViewBag.EvaluationFormNames = _studyPlanService.GetEvaluationFormNames();

            return PartialView("~/Views/StudyDomains/EditStudyPlan.cshtml", studyPlan);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult UpdateStudyPlan(UpdateStudyPlanVM updateStudyPlanVM)
        {
            try
            {
                _studyPlanService.UpdateStudyPlan(updateStudyPlanVM);
                return Ok(new
                {
                    success = true,
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false,
                });
            }
        }
    }
}
