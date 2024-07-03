using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Web.Controllers
{
    public class HallsController : Controller
    {
        private readonly HallService _hallService;

        public HallsController(HallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        public IActionResult Index(int buildingId)
        {
            var halls = _hallService.GetAllHallsInBuilding(buildingId);
            return View(Tuple.Create(halls, buildingId));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hall = _hallService.GetHallById(id.Value);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        [HttpGet]
        public IActionResult Create(int buildingId)
        {
            var hall = new HallCreateModel
            {
                BuildingId = buildingId
            };
            return View(hall);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HallCreateModel hall)
        {
            if (ModelState.IsValid)
            {
                var status = _hallService.CreateHall(hall);

                if (status)
                {
                    return RedirectToAction("Index", "Halls", new { buildingId = hall.BuildingId });
                }

                return View(hall);
            }

            return View(hall);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                _hallService.DeleteHall(id.Value);
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

        [HttpPost]
        public IActionResult Edit(HallCreateModel hall)
        {
            if (ModelState.IsValid)
            {
                var status = _hallService.Update(hall);

                if (status)
                {
                    return RedirectToAction("Index", "Halls", new { buildingId = hall.BuildingId });
                }

                return View(hall);
            }

            return View(hall);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Dean,Secretary")]
        public IActionResult GetAllHalls(int? distributionId, int? day, int? hour, int? activityId, int? duration, int? frequencyId, int? groupId)
        {
            var halls = _hallService.GetAllHalls(distributionId, day, hour, activityId, duration, frequencyId, groupId);
            return Ok(halls);
        }
    }
}
