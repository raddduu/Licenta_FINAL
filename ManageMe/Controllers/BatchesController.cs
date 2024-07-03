using ManageMe.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Web.Controllers
{
    public class BatchesController : Controller
    {
        private readonly BatchService _batchService;

        public BatchesController(BatchService batchService)
        {
            _batchService = batchService;
        }

        public IActionResult Index()
        {
            var batches = _batchService.GetBatchVMs();
            return View(batches);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = _batchService.GetBatchVM(id.Value);

            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var batch = new BatchCreateModel();
            return View(batch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BatchCreateModel batch)
        {
            if (ModelState.IsValid)
            {
                var status = _batchService.AddBatch(batch);

                if (status)
                {
                    return RedirectToAction("Index", "Batches");
                }
            }

            return View(batch);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = _batchService.GetBatchVM(id.Value);

            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BatchCreateModel batch)
        {
            if (id != batch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var status = _batchService.EditBatch(batch);

                if (status)
                {
                    return RedirectToAction("Index", "Batches");
                }
            }

            return View(batch);
        }
    }
}
