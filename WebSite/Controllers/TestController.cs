using Core.Services.Tests;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;
using Share.Generators;
using WebSite.Filters;

namespace WebSite.Controllers
{

   // [Authorize(systemName = "Test")]

    public class TestController : Controller
    {
        public ITestRepository _testService;

        public TestController(ITestRepository testService)
        {
            _testService = testService;
        }

        public IActionResult Index()
        {
            ViewBag.MyDate = DateTimeGenerator.GetShamsiDate(DateTime.Now.Date);
            return View();
        }

        [HttpPost]
        public JsonResult GetTestForSelect2(string searchTerm)
        {
            var result = _testService.GetTestForSelect2(searchTerm);
            return Json(result);
        }


        [HttpPost]
        public IActionResult GetTests()
        {
            var result = _testService.GetTests();
            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public void AddTest()
        {
            var testConditionId = Convert.ToInt32(Request.Form["TestConditionId"].ToString());
            var testImportanceId = Convert.ToInt32(Request.Form["TestImportanceId"].ToString());
            var labratoaryToolId = Convert.ToInt32(Request.Form["LabratoaryToolId"].ToString());
            var testName = Request.Form["TestName"].ToString();
            var fromDate = Request.Form["FromDate"].ToString();
            var endDate = Request.Form["EndDate"].ToString();
            var amount = Convert.ToUInt32(Request.Form["Amount"].ToString());
            var minimum = Convert.ToDecimal(Request.Form["Minimum"].ToString());
            var maximum = Convert.ToDecimal(Request.Form["Maximum"].ToString());
            var measureId = Convert.ToInt32(Request.Form["MeasureId"].ToString());
            var standardId = Convert.ToInt32(Request.Form["StandardId"].ToString());
            var testDescriptionId = Convert.ToInt32(Request.Form["TestDescriptionId"].ToString());

            var test = new TestViewModel
            {
                TestConditionId = testConditionId,
                TestImportanceId = testImportanceId,
                LabratoaryToolId = labratoaryToolId,
                TestName = testName,
                FromDate = fromDate,
                EndDate = endDate,
                Amount = amount,
                Minimum = minimum,
                Maximum = maximum,
                MeasureId = measureId,
                StandardId = standardId,
                TestDescriptionId = testDescriptionId
            };

            _testService.AddTest(test);

        }

        [HttpPost]
        public void UpdateTest()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var testImportanceId = Convert.ToInt32(Request.Form["TestImportanceId"].ToString());
            var testConditionId = Convert.ToInt32(Request.Form["TestConditionId"].ToString());
            var labratoaryToolId = Convert.ToInt32(Request.Form["LabratoaryToolId"].ToString());
            var testName = Request.Form["TestName"].ToString();
            var fromDate = Request.Form["FromDate"].ToString();
            var endDate = Request.Form["EndDate"].ToString();
            var amount = Convert.ToInt32(Request.Form["Amount"].ToString());
            var minimum = Convert.ToDecimal(Request.Form["Minimum"].ToString());
            var maximum = Convert.ToDecimal(Request.Form["Maximum"].ToString());
            var measureId = Convert.ToInt32(Request.Form["MeasureId"].ToString());
            var standardId = Convert.ToInt32(Request.Form["StandardId"].ToString());
            var testDescriptionId = Convert.ToInt32 (Request.Form["TestDescriptionId"].ToString());

            var test = new TestViewModel
            {
                Id = id,
                TestConditionId = testConditionId,
                TestImportanceId = testImportanceId,
                LabratoaryToolId = labratoaryToolId,
                TestName = testName,
                FromDate = fromDate,
                EndDate = endDate,
                Amount = amount,
                Minimum =  minimum,
                Maximum = maximum,
                MeasureId = measureId,
                StandardId = standardId,
                TestDescriptionId = testDescriptionId
            };  
            _testService.UpdateTest(test);
        }

        public IActionResult Delete(int id)
        {
            _testService.DeleteTest(id);
            return RedirectToAction(nameof(Index));
        }
    }
}