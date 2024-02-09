using Core.Services.Measures;
using Core.Services.TestImportances;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{
   // [Authorize(systemName = "TestImportance")]

    public class TestImportanceController : Controller
    {
        public ITestImportanceRepository _testImportanceService;

        public TestImportanceController(ITestImportanceRepository testImportanceService)
        {
            _testImportanceService = testImportanceService;
        }

        public async Task<IActionResult> Index()
        {
            var result =  await _testImportanceService.GetTestImportances_Async();
            return View(result);
        }

        [HttpPost]
        public IActionResult GetTestImportances()
        {
            var result = _testImportanceService.GetTestImportances();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public JsonResult GetTestImportancForSelect2(string searchTerm)
        {
            var result = _testImportanceService.GetTestImportancForSelect2(searchTerm);
            return Json(result);
        }  

        [HttpPost]
        public IActionResult GetMeasures()
        {
            var result = _testImportanceService.GetTestImportances();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        } 

        [HttpPost] 
        public void AddTestImportance( )
        {
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var testImportance = new TestImportance
            {
                Name = name,
                IsActive = IsActive,
            };
            _testImportanceService.AddTestImportance(testImportance); 
        }

        [HttpPost] 
        public void UpdateTestImportance( )
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var testImportance = new TestImportance
            {
                Id = id,
                Name = name,
                IsActive = IsActive,
            };
            bool result = _testImportanceService.UpdateTestImportance(testImportance);          
        }

        public IActionResult Delete(int id)
        {
            _testImportanceService.DeleteTestImportance(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}