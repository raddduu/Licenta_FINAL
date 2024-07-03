using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using System.Security.Claims;
using ManageMe.Entities.Enums;
using ManageMe.Web.Code.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Channels;

namespace ManageMe.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly GeneralAlgorithm _algorithms;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ChannelService _channelService;
        
        public ChannelsController(ApplicationDbContext context, IWebHostEnvironment env, GeneralAlgorithm algorithms, UserManager<ApplicationUser> userManager, ChannelService channelService)
        {
            _context = context;
            _env = env;
            _algorithms = algorithms;
            _userManager = userManager;
            _channelService = channelService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllAccessTypes()
        {
            var acccTypes = _channelService.GetAllAccessTypes();

            return Ok(acccTypes);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllRoles()
        {
            var roles = _channelService.GetAllRoles();

            return Ok(roles);
        }

        [Authorize]
        public IActionResult Index(int page = 1, int pageSize = 9)
        {
            var isAdmin = User.IsInRole("Admin");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var search = Convert.ToString(HttpContext.Request.Query["search"])?.Trim() ?? "";

            if (userId == null)
            {
                return NotFound();
            }

            var viewChennels = _channelService.GetChannels(page, pageSize, userId, isAdmin);

            int totalItems = viewChennels != null ? viewChennels.Count() : 0;

            var filteredChannels = viewChennels?.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();

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

            if (currentPage > paginationSettings.LastPage && paginationSettings.LastPage > 0)
            {
                return RedirectToAction("Index", new { page = paginationSettings.LastPage });
            }

            return View(viewChennels);
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null || !_channelService.ChannelExists())
            {
                return NotFound();
            }

            var channel = _channelService.GetDetailsChannelVM(id);
                
            if (channel == null)
            {
                return NotFound();
            }

            return View(channel);
        }

        [Authorize]
        public IActionResult? AddNewMember (int channelId, string userJoinCode)
        {
            if (CheckChannelJoinCode(channelId, userJoinCode) == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    return NotFound();
                }

                _channelService.AddNewMember(channelId, userId);

                return Ok(true);
            }

            return Ok(false);
        }

        [Authorize]
        public IActionResult AcceptRequest(int channelId, string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserIsGroupModerator = _channelService.UserIsGroupModerator(currentUserId, channelId);

            if (!currentUserIsGroupModerator)
            {
                return Unauthorized();
            }

            _channelService.AcceptRequest(channelId, userId);
            return RedirectToAction("Details", new { id = channelId });
        }

        [Authorize]
        public IActionResult RemoveMember(int channelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }

            var currentUserIsInChannel = _channelService.UserIsInChannel(userId, channelId);

            if (!currentUserIsInChannel)
            {
                return Unauthorized();
            }

            _channelService.RemoveMember(channelId, userId);

            return Ok();
        }

        [Authorize]
        public IActionResult PromoteMember(int channelId, string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserIsGroupModerator = _channelService.UserIsGroupModerator(currentUserId, channelId);

            if (!currentUserIsGroupModerator)
            {
                return Unauthorized();
            }

            var userIsInChannel = _channelService.UserIsInChannel(userId, channelId);

            if (!userIsInChannel)
            {
                return NotFound();
            }

            _channelService.PromoteMember(channelId, userId);

            return RedirectToAction("Details", new { id = channelId });
        }

        [Authorize]
        public IActionResult KickMember(int channelId, string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserIsGroupModerator = _channelService.UserIsGroupModerator(currentUserId, channelId);

            if (!currentUserIsGroupModerator)
            {
                return Unauthorized();
            }

            var userIsInChannel = _channelService.UserIsInChannel(userId, channelId);

            if (!userIsInChannel)
            {
                return NotFound();
            }

            _channelService.RemoveMember(channelId, userId);

            return RedirectToAction("Details", new { id = channelId });
        }

        [Authorize]
        public IActionResult RejectRequest(int channelId, string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserIsGroupModerator = _channelService.UserIsGroupModerator(currentUserId, channelId);

            if (!currentUserIsGroupModerator)
            {
                return Unauthorized();
            }

            var userHasRequestedChannelMembership = _channelService.UserHasRequestedChannelMembership(userId, channelId);

            if (!userHasRequestedChannelMembership)
            {
                return NotFound();
            }

            _channelService.RejectRequest(channelId, userId);

            return RedirectToAction("Details", new { id = channelId });
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromForm] CreateChannelVM newChannel)
        {
            var currentUserRolesNames = User.FindAll(ClaimTypes.Role).Select(rc => rc.Value);

            var channelRolesIds = newChannel.Roles;

            var userHasAtLeastOneRoleInCommonWithChannel = _channelService.UserHasAtLeastOneRoleInCommonWithChannel(currentUserRolesNames, channelRolesIds);

            if (!userHasAtLeastOneRoleInCommonWithChannel)
            {
                ViewBag.Error = "You must have at least one role in common with the channel you are creating";
                return View(newChannel);
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                newChannel.ChannelPicByteArray = newChannel.ChannelPicFromForm.ToByteArray();

                if (userId == null)
                {
                    return NotFound();
                }

                _channelService.Create(newChannel, userId);

                return RedirectToAction(nameof(Index));
            }

            return View(newChannel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, [Bind("Id,Title,SubjectId,ChannelPic")] EditChannelVM channel)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserRoles = User.FindAll(ClaimTypes.Role).Select(rc => rc.Value);

            var currentUserIsGroupModerator = _channelService.UserIsGroupModerator(currentUserId, id);

            if (!currentUserIsGroupModerator)
            {
                return Unauthorized();
            }

            if (id != channel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var channelRoles = channel.Roles ?? new List<string>();

                    if (currentUserRoles.Intersect(channelRoles).Count() == 0)
                    {
                        ViewBag.Error = "You must have at least one role in common with the channel you are editing";
                        return View(channel);
                    }

                    if (channel.ChannelPicFromForm != null)
                    {
                        channel.ChannelPicByteArray = channel.ChannelPicFromForm.ToByteArray();
                    }

                    _channelService.UpdateChannel(channel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_channelService.GetChannelById(channel.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Details", new { id = channel.Id });
            }

            return View(channel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var channel = _channelService.GetChannelById(id);

            if (channel == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserIsGroupModerator = _channelService.UserIsGroupModerator(currentUserId, id);

            if (!currentUserIsGroupModerator)
            {
                return Unauthorized();
            }

            if (channel != null)
            {
                _channelService.DeleteChannel(channel);
                return Ok(new
                {
                    success = true
                });
            }

            return Ok(new {
                success = false
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddChannelParticipationRequest(int channelId, string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != userId)
            {
                return Unauthorized();
            }

            var userIsAlreadyInGroup = _channelService.UserIsInChannel(userId, channelId);
            var userHadAlreadyRequestedMembership = _channelService.UserHasRequestedChannelMembership(userId, channelId);

            if (userIsAlreadyInGroup || userHadAlreadyRequestedMembership)
            {
                return NotFound();
            }

            try
            {
                _channelService.AddChannelParticipationRequest(channelId, userId);

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

        [HttpGet]
        [Authorize]
        public IActionResult? GetChannelPic(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var channel = _channelService.GetChannelById(id);

            if (channel == null)
            {
                return null;
            }

            if (channel?.ChannelPic != null)
            {
                return File(channel.ChannelPic, "image/jpg");
            }

            return null;
        }

        [NonAction]
        private bool? CheckChannelJoinCode(int channelId, string userInputCode)
        {
            var channel = _channelService.GetChannelById(channelId);

            if (channel == null)
            {
                return false;
            }

            if (channel.JoinCode == null)
            {
                return true;
            }

            return channel.JoinCode.Equals(userInputCode);
        }
    }
}
