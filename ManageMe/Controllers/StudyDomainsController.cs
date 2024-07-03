using Microsoft.AspNetCore.Mvc;
using ManageMe.Entities;
using ManageMe.Services;
using ManageMe.BusinessLogic.Implementation.Subject;
using Microsoft.AspNetCore.Authorization;
using ManageMe.BusinessLogic;

namespace ManageMe.Controllers
{
    public class StudyDomainsController : Controller
    {
        private readonly StudyDomainService _studyDomainService;

        public StudyDomainsController(StudyDomainService studyDomainService)
        {
            _studyDomainService = studyDomainService;
        }

        public ActionResult GetAllStudyDomainsAsSelectList()
        {
            var studyDomainsSelectList = _studyDomainService.GetAllStudyDomainsAsSelectList();

            return Ok(studyDomainsSelectList);
        }

        public ActionResult GetAllSubjectTypes()
        {
            var subjects = _studyDomainService.GetAllSubjectTypes();

            return Ok(subjects);
        }

        public ActionResult GetAllEvaluationTypes()
        {
            var evaluations = _studyDomainService.GetAllEvaluationTypes();

            return Ok(evaluations);
        }

        public IActionResult Index()
        {
            var studyDomains =  _studyDomainService.GetAllStudyDomains();

            var currentSemester = _studyDomainService.GetCurrentSemester();

            ViewBag.CurrentSemester = currentSemester;

            return View(studyDomains);
        }

        [HttpPost]
        public ActionResult SetSemester(int semester)
        {
            var result = _studyDomainService.SetSemester(semester);

            return Ok(result);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyDomain =  _studyDomainService.GetDetailsStudyDomainVM(id.Value);
            if (studyDomain == null)
            {
                return NotFound();
            }

            ViewBag.Frequencies = _studyDomainService.GetAllFrequencies();

            return View(studyDomain);
        }

        [Authorize(Roles = "Admin,Dean")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult Create(StudyDomainCreateModel studyDomain)
        {
            if (ModelState.IsValid)
            {
                 _studyDomainService.AddStudyDomain(studyDomain);
                return RedirectToAction(nameof(Index));
            }
            return View(studyDomain);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_studyDomainService.StudyDomainExists(id))
            {
                return NotFound();
            }

             _studyDomainService.DeleteStudyDomain(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
