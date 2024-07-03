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

namespace ManageMe.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly GeneralAlgorithm _algorithms;
        private readonly IWebHostEnvironment _env;
        private readonly UserService _userService;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager
            , GeneralAlgorithm generalAlgorithm, IWebHostEnvironment env, UserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _algorithms = generalAlgorithm;
            _env = env;
            _userService = userService;
        }

        public IActionResult GetAllTeachersThatTeachThisActivityAtThisGroup(int subjectId, int activityId, int groupId)
        {
            var teachers = _userService.GetAllTeachersThatTeachThisActivityAtThisGroup(subjectId, activityId, groupId);
            return Ok(teachers);
        }

        public List<UserMinimalInfo> GetTeachersForSubjectActivity(int subjectId, int activityId, int groupId)
        {

            var teachers = _userService.GetTeachersForSubjectActivity(subjectId, activityId, groupId);
            return teachers;

        }

        public List<UserMinimalInfo>? GetAllTeachersForActivity(int activityId, int subjectId)
        {
            if (!(activityId == 3 || activityId == 4 || activityId == 6))
            {
                return null;
            }

            List<UserMinimalInfo> usersInRole;

            if (activityId == 4)
            {
                usersInRole = _userService.GetAllUsersInRole("Lector", subjectId, activityId);
            }

            else
            {
                usersInRole = _userService.GetAllUsersInRole("Assistant", subjectId, activityId); 
            }

            return usersInRole;
        }

        public List<UserMinimalInfo>? GetAllUsersInRole(string roleName)
        {
            var usersInRole = _userService.GetAllUsersInRole(roleName);
            return usersInRole;
        }

        public List<UserMinimalInfo>? GetAllAvailableStudents()
        {
            var usersInRole = _userService.GetAllStudentsNotInGroups();

            return usersInRole;
        }

        [Authorize]
        public IActionResult Index(int page = 1, int pageSize = 15)
        {
            var search = Convert.ToString(HttpContext.Request.Query["search"])?.Trim() ?? "";

            var viewUsers = _userService.GetAll(search);
            int totalItems = viewUsers.Count();
            viewUsers = viewUsers.Skip((page - 1) * pageSize).Take(pageSize);

            var paginationBaseUrl = "";

            if (search != "")
            {
                paginationBaseUrl = "/Users/Index/?search=" + search + "&page";
            }

            else
            {
                paginationBaseUrl = "/Users/Index/?page";
            }

            var currentPage = Convert.ToInt32(HttpContext.Request.Query["page"]);

            if (currentPage <= 0)
            {
                currentPage = 1;
            }

            var paginationSettings = new PaginationSettings
            {
                SearchString = search,
                PaginationBaseUrl = paginationBaseUrl,
                PageSize = pageSize,
                LastPage = Math.Ceiling((float)totalItems / (float)pageSize),
                CurrentPage = currentPage
            };

            ViewBag.PaginationSettings = paginationSettings;

            if (currentPage > paginationSettings.LastPage)
            {
                return RedirectToAction("Index", new { page = paginationSettings.LastPage });
            }

            return View(viewUsers);
        }

        [Authorize(Roles = "Admin, Dean")]
        public async Task<IActionResult> AddToRole(string userId, string roleName)
        {
            if (!User.IsInRole("Admin") && roleName == "Dean")
            {
                return Unauthorized();
            }

            var status = await _userService.AddToRoleAsync(userId, roleName);

            return Ok(new
            {
                success = status
            });
        }

        [Authorize(Roles = "Admin, Dean")]
        public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
        {
            if (!User.IsInRole("Admin") && roleName == "Dean")
            {
                return Unauthorized();
            }

            var status = await _userService.RemoveFromRole(userId, roleName);

            return Ok(new
            {
                success = status
            });
        }

        [Authorize]
        public IActionResult Details()
        {
            var id = _userManager.GetUserId(User);

            var userPersonalData = _userService.GetUserPersonalData(id);

            if (userPersonalData == null)
            {
                return NotFound();
            }

            return View(userPersonalData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] PersonalDataUser user)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId != id)
            {
                return Unauthorized();
            }

            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdatePersonalData(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_userService.GetUserById(user.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = _userService.DeleteUser(id);

            return Ok( new {
                success = status
            });
                
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProfilePicture(string id)
        {
            var profilePicture = _userService.GetProfilePicture(id);

            if (profilePicture != null)
            {
                return File(profilePicture, "image/jpeg");
            }

            var defaultImagePath = Path.Combine("wwwroot", "images", "default-profile-picture.jpg");
            var defaultImageBytes = System.IO.File.ReadAllBytes(defaultImagePath);
            return File(defaultImageBytes, "image/jpeg");
        }

        [HttpGet]
        [Authorize]
        public IActionResult? GetProfilePictureForPersonalData(string id)
        {
            var profilePicture = _userService.GetProfilePicture(id);

            if (profilePicture != null && profilePicture.Length > 0)
            {
                return File(profilePicture, "image/jpeg");
            }

            return null;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddProfilePicture(PersonalDataUser pdu)
        {
            var file = pdu.ProfilePicture;

            var userId = _userManager.GetUserId(User);

            if (file == null)
            {
                return BadRequest();
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
            {
                return BadRequest();
            }

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            var status = _userService.UploadProfilePicture(userId, fileBytes);

            return RedirectToAction("Details");
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveProfilePicture()
        {
            var currentUserId = _userManager.GetUserId(User);

            var status = _userService.RemoveProfilePicture(currentUserId);

            return Ok(new
            {
                success = status
            });
        }
    }
}
