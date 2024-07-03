using ManageMe.BusinessLogic;
using ManageMe.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ManageMe.Controllers
{
    public class MethodologiesController : Controller
    {
        private readonly MethodologyService _methodologyService;
        private readonly ChapterService _chapterService;
        private readonly SectionService _sectionService;
        private readonly ParagraphService _paragraphService;
        private readonly DetailService _detailService;
        private readonly ArticleService _articleService;
        private readonly ProvisionService _provisionService;

        public MethodologiesController(MethodologyService methodologyService, ChapterService chapterService, SectionService sectionService, ParagraphService paragraphService, DetailService detailService, ArticleService articleService, ProvisionService provisionService)
        {
            _methodologyService = methodologyService;
            _chapterService = chapterService;
            _sectionService = sectionService;
            _paragraphService = paragraphService;
            _detailService = detailService;
            _articleService = articleService;
            _provisionService = provisionService;
        }

        public IActionResult Index()
        {
            var methodologies = _methodologyService.GetIndexMethodologies();

            return View(methodologies);
        }

        public IActionResult Details(int id)
        {
            var methodology = _methodologyService.GetMethodologyById(id);

            return View(methodology);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("~/Views/Methodologies/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(CreateMethodologyVM createMethodologyVM)
        {
            try
            {
                _methodologyService.CreateMethodology(createMethodologyVM);
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _methodologyService.DeleteMethodology(id);
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

        [HttpPut]
        public IActionResult Update(EditMethodologyVM editMethodologyVM)
        {
            try
            {
                _methodologyService.UpdateMethodology(editMethodologyVM);
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

        [HttpPost]
        public IActionResult AddChapter(CreateChapterVM createChapterVM)
        {
            try
            {
                _chapterService.CreateChapter(createChapterVM);
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

        [HttpDelete]
        public IActionResult DeleteChapter(int id)
        {
            try
            {
                _chapterService.DeleteChapter(id);
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

        [HttpPut]
        public IActionResult UpdateChapter(EditChapterVM editChapterVM)
        {
            try
            {
                _chapterService.UpdateChapter(editChapterVM);
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

        [HttpPost]
        public IActionResult AddSection(CreateSectionVM createSectionVM)
        {
            try
            {
                _sectionService.CreateSection(createSectionVM);
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

        [HttpDelete]
        public IActionResult DeleteSection(int id)
        {
            try
            {
                _sectionService.DeleteSection(id);
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

        [HttpPut]
        public IActionResult UpdateSection(EditSectionVM editSectionVM)
        {
            try
            {
                _sectionService.UpdateSection(editSectionVM);
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

        [HttpPost]
        public IActionResult AddParagraph(CreateParagraphVM createParagraphVM)
        {
            try
            {
                _paragraphService.CreateParagraph(createParagraphVM);
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

        [HttpDelete]
        public IActionResult DeleteParagraph(int id)
        {
            try
            {
                _paragraphService.DeleteParagraph(id);
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

        [HttpPut]
        public IActionResult UpdateParagraph(EditParagraphVM editParagraphVM)
        {
            try
            {
                _paragraphService.UpdateParagraph(editParagraphVM);
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

        [HttpPost]
        public IActionResult AddDetail(CreateDetailVM createDetailVM)
        {
            try
            {
                _detailService.CreateDetail(createDetailVM);
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

        [HttpDelete]
        public IActionResult DeleteDetail(int id)
        {
            try
            {
                _detailService.DeleteDetail(id);
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

        [HttpPut]
        public IActionResult UpdateDetail(EditDetailVM editDetailVM)
        {
            try
            {
                _detailService.UpdateDetail(editDetailVM);
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

        [HttpPost]
        public IActionResult AddArticle(CreateArticleVM createArticleVM)
        {
            try
            {
                _articleService.CreateArticle(createArticleVM);
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

        [HttpDelete]
        public IActionResult DeleteArticle(int id)
        {
            try
            {
                _articleService.DeleteArticle(id);
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

        [HttpPut]
        public IActionResult UpdateArticle(EditArticleVM editArticleVM)
        {
            try
            {
                _articleService.UpdateArticle(editArticleVM);
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

        [HttpPost]
        public IActionResult AddProvision(CreateProvisionVM createProvisionVM)
        {
            try
            {
                _provisionService.CreateProvision(createProvisionVM);
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

        [HttpDelete]
        public IActionResult DeleteProvision(int id)
        {
            try
            {
                _provisionService.DeleteProvision(id);
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

        [HttpPut]
        public IActionResult UpdateProvision(EditProvisionVM editProvisionVM)
        {
            try
            {
                _provisionService.UpdateProvision(editProvisionVM);
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
        public IActionResult AIChat()
        {
            return View();
        }

        public async Task<ActionResult> DownloadBERT()
        {
            PythonRunner runner = new PythonRunner();
            string scriptPath = @"C:\Users\RaduI\source\repos\ManageMe\ManageMe\PythonCodeBase\downloadBERT.py";
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

        public async Task<IActionResult> DownloadROMISTRAL()
        {
            PythonRunner runner = new PythonRunner();
            string scriptPath = @"C:\Users\RaduI\source\repos\ManageMe\ManageMe\PythonCodeBase\downloadROMISTRAL.py";
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

        [HttpGet]
        public async Task<Tuple<string, int>> GetAnswer(string question)
        {
            // get the value of the "UsedModel" key from the AIconfig.json file
            var config = System.IO.File.ReadAllText("AIconfig.json", Encoding.UTF8);
            dynamic configJson = JsonConvert.DeserializeObject(config);
            string usedModel = configJson.UsedModel;

            PythonRunner runner = new PythonRunner();
            string scriptPath = @$"C:\Users\RaduI\source\repos\ManageMe\ManageMe\PythonCodeBase\answer_question_{usedModel}.py";

            var inputData = question;

            try
            {
                await runner.RunPythonScriptAsync(scriptPath, inputData);
                var result = System.IO.File.ReadAllText("output.txt", Encoding.UTF8);
                return Tuple.Create(result, 1);
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, 0);
            }
        }

        [HttpGet]
        public async Task<Tuple<string, int>> GetKnowledgeBase(string question)
        {
            PythonRunner runner = new PythonRunner();
            string scriptPath = @"C:\Users\RaduI\source\repos\ManageMe\ManageMe\PythonCodeBase\get_relevant_information.py";
            var inputData = question;

            try
            {
                await runner.RunPythonScriptAsync(scriptPath, inputData);
                var result = System.IO.File.ReadAllText("output.txt", Encoding.UTF8);

                result = result.Replace("  ", "\t");

                return Tuple.Create(result, 1);
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, 0);
            }
        }
    }
}
