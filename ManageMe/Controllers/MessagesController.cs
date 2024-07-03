using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManageMe.DataAccess;
using ManageMe.Entities;
using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ManageMe.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly MessageService _messageService;
        //private readonly GroupService _groupService;
        private readonly ChannelService _channelService;

        public MessagesController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, MessageService messageService, ChannelService channelService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _messageService = messageService;
            _channelService = channelService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index(int channelId, int pageNumber = 1)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null)
            {
                return NotFound();
            }

            var userIsInChannel = _channelService.UserIsInChannel(currentUserId, channelId);

            if (!userIsInChannel && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var channelMessages = _messageService.GetMessages(pageNumber, 10, channelId);
            return View(Tuple.Create(channelMessages, channelId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult? AddNewMessage(CreateMessageVM model)
        {
            var authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userIsInChannel = _channelService.UserIsInChannel(authorId, model.ChannelId);

            if (!userIsInChannel && !User.IsInRole("Admin"))
            {
                return Unauthorized(new
                {
                    ErrorMessage = "You are not in this channel!"
                });
            }

            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                return BadRequest(new
                {
                    ErrorMessage = error
                });
            }

            var result = _messageService.AddNewMessage(model, authorId);

            return Ok(new 
            {
                success = result
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult? Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userIsAuthor = _messageService.UserIsAuthor(currentUserId, id);

            if (!userIsAuthor && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var status = _messageService.DeleteMessage((int)id);

            return Ok(new
            {
                success = status
            });

        }
    }
}
