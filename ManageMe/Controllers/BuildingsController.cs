using Microsoft.AspNetCore.Mvc;
using ManageMe.BusinessLogic;

namespace ManageMe.Web.Controllers
{
    public class BuildingsController : Controller
    {
        private readonly BuildingService _buildingService;

        public BuildingsController(BuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var buildings = _buildingService.GetAll();

            return View(buildings);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BuildingCreateModel createBuildingVM)
        {
            if (ModelState.IsValid)
            {
                var status = _buildingService.Create(createBuildingVM);

                if (status)
                {
                    return RedirectToAction("Index");
                }

                return BadRequest();
            }

            return View(createBuildingVM);
        }

        [HttpGet]
        public IActionResult Details (int id)
        {
            var building = _buildingService.GetById(id);

            return View(building);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var status = _buildingService.Delete(id);

            return Ok(new
            {
                success = status,
            });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var building = _buildingService.GetById(id);

            return View(building);
        }

        [HttpPost]
        public IActionResult Edit(BuildingCreateModel editBuildingVM)
        {
            if (ModelState.IsValid)
            {
                var status = _buildingService.Update(editBuildingVM);

                if (status)
                {
                    return RedirectToAction("Index");
                }

                return BadRequest();
            }

            return View(editBuildingVM);
        }
    }
}
