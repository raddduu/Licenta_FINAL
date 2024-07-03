using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManageMe.DataAccess;
using ManageMe.Entities;
using Microsoft.AspNetCore.Identity;
using ManageMe.Common;
using ManageMe.BusinessLogic.Implementation.Subject;
using ManageMe.BusinessLogic;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;

namespace ManageMe.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly GeneralAlgorithm _algorithms;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SubjectService _subjectService;
        private readonly StudyPlanService _studyPlanService;
        
        public SubjectsController(ApplicationDbContext context, IWebHostEnvironment env, GeneralAlgorithm algorithms, UserManager<ApplicationUser> userManager, SubjectService subjectService, StudyPlanService studyPlanService)
        {
            _context = context;
            _env = env;
            _algorithms = algorithms;
            _userManager = userManager;
            _subjectService = subjectService;
            _studyPlanService = studyPlanService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var search = "";

            if (Convert.ToString(HttpContext.Request.Query["search"]) != null)
            {
                search = Convert.ToString(HttpContext.Request.Query["search"]).Trim();
            }

            var viewSubjects = _subjectService.GetSubjects(search);
            int totalItems = viewSubjects.Count();
            viewSubjects = viewSubjects.Skip((page - 1) * pageSize).Take(pageSize);

            var currentPage = Convert.ToInt32(HttpContext.Request.Query["page"]);

            if (currentPage <= 0)
            {
                currentPage = 1;
            }

            var paginationBaseUrl = "";

            if (search != "")
            {
                paginationBaseUrl = "/Subjects/Index/?search=" + search + "&page";
            }

            else
            {
                paginationBaseUrl = "/Subjects/Index/?page";
            }

            var paginationSettings = new PaginationSettings
            {
                SearchString = search,
                PaginationBaseUrl = paginationBaseUrl,
                PageSize = pageSize,
                LastPage = Math.Ceiling((float)totalItems / (float)pageSize)
            };

            ViewBag.PaginationSettings = paginationSettings;

            if (currentPage > paginationSettings.LastPage)
            {
                return RedirectToAction("Index", new { page = paginationSettings.LastPage });
            }

            return View(viewSubjects);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllSubjects(int? groupId)
        {
            var subjects = _subjectService.GetAllSubjects(groupId);

            return Ok(subjects);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_subjectService.SubjectExists())
            {
                return NotFound();
            }

            var subject = _subjectService.GetSubjectById(id);

            if (subject == null)
            {
                return NotFound();
            }

            var completeSubject = _subjectService.GetCompleteSubject(id);

            if (completeSubject == null)
            {
                return NotFound();
            }

            completeSubject = _studyPlanService.CheckSubjectCourseLaboratorySeminaryExistsAtAnyStudyDomain(completeSubject);

            return View(completeSubject);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Lector,Assistant")]
        public IActionResult TeacherPermissions(string? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var teacherPermissions = _subjectService.GetTeacherPermissions(userId);

            return View(teacherPermissions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Dean")]
        public async Task<IActionResult> Create(SubjectCreateModel subject)
        {
            if (ModelState.IsValid)
            {
                _subjectService.AddNewSubject(subject);
                
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult Update(UpdateSubjectVM updateStudyPlanVM)
        {
            try
            {
                _subjectService.UpdateSubject(updateStudyPlanVM);
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

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult Delete(int id)
        {
            if (! _subjectService.SubjectExists())
            {
                return Problem("Entity set 'ApplicationDbContext.Subjects'  is null.");
            }

            var subject = _subjectService.GetSubjectById(id);

            if (subject == null)
            {
                return NotFound();
            }
            
            var status = _subjectService.DeleteSubject(subject);

            return Ok(new
            {
                success = status
            });
        }

        //public IActionResult AddShortNames()
        //{
        //    _subjectService.AddShortNames();
        //    return Ok();
        //}
    }
}
