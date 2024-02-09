using Core.Services.TestConditions;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{
 //   [Authorize(systemName = "TestCondition")]

    public class TestConditionController : Controller
    {
        public ITestConditionRepository _testConditionService;

        public TestConditionController(ITestConditionRepository testConditionService)
        {
            _testConditionService = testConditionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetTestConditions()
        {
            var result = _testConditionService.GetTestConditions();
            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };
            return Ok(jsonData);
        }

        [HttpPost]
        public JsonResult GetTestConditionForSelect2(string searchTerm)
        {
            var result = _testConditionService.GetTestConditionForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]
        public void AddTestCondition()
        {
            var name = Request.Form["Name"].ToString();
            var isActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());
            var testCondition = new TestCondition
            {
                Name = name,
                IsActive = isActive,
            };
            _testConditionService.AddTestCondition(testCondition);
        }

        [HttpPost]
        public void UpdateTestCondition()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();
            var isActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var testCondition = new TestCondition
            {
                Id = id,
                Name = name,
                IsActive = isActive
            };
            _testConditionService.UpdateTestCondition(testCondition);
        }

        public IActionResult Delete(int id)
        {
            _testConditionService.DeleteTestCondition(id);
            return RedirectToAction(nameof(Index));
        }
    }
}