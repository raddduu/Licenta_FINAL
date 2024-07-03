using ManageMe.Common;
using ManageMe.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ManageMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public async Task<ActionResult> DownloadBERT()
        {
            PythonRunner runner = new PythonRunner();
            string scriptPath = @"C:\Users\RaduI\source\repos\ManageMe\ManageMe\PythonCodeBase\build_text_tensors.py";
            var inputData = "Cati ani am?";

            try
            {
                string result = await runner.RunPythonScriptAsync(scriptPath, inputData);
                ViewBag.ScriptResult = result;
            }
            catch (Exception ex)
            {
                ViewBag.ScriptResult = ex.Message;
            }

            return View();
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return NotFound();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}