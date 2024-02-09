using Core.Services.TestDescriptions;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{

 //   [Authorize(systemName = "TestDescription")]

    public class TestDescriptionController : Controller
    {
        public ITestDescriptionRepository _testDescriptionService;

        public TestDescriptionController(ITestDescriptionRepository TestDescriptionService)
        {
            _testDescriptionService = TestDescriptionService;
        }

        public async Task<IActionResult> Index()
        { 
            return View();
        }

        [HttpPost]
        public JsonResult GetTestDescriptionForSelect2(string searchTerm)
        {
            var result = _testDescriptionService.GetTestDescriptionForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetTestDescriptions()
        {
            var result = _testDescriptionService.GetTestDescriptions();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult AddTestDescription()
        {
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var testDescription = new TestDescription
            {
                Name = name,
                IsActive = IsActive,
            };
            _testDescriptionService.AddTestDescription(testDescription);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateTestDescription()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var testDescription = new TestDescription
            {
                Id = id,
                Name = name,
                IsActive = IsActive,
            };
            bool result = _testDescriptionService.UpdateTestDescription(testDescription);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _testDescriptionService.DeleteTestDescription(id);
            return RedirectToAction(nameof(Index));
        }

    }
}