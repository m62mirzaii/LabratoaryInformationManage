using Share.Generators;
using Core.Services.TestAccepts;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModel;
using WebSite.Filters;

namespace WebSite.Controllers
{

   // [Authorize(systemName = "TestAccept")]
    public class TestAcceptController : Controller
    {
        public ITestAcceptRepository _testAcceptRepository;

        public TestAcceptController(ITestAcceptRepository testAcceptRepository)
        {
            _testAcceptRepository = testAcceptRepository;
        }

        public IActionResult Index()
        {
            ViewBag.MyDate = DateTimeGenerator.GetShamsiDate(DateTime.Now.Date);
            return View();
        }

        #region ============================= TestAccept =============================
        public IActionResult GetTestAccepts()
        {
            var result = _testAcceptRepository.GetTestAccepts();
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost] 
        public IActionResult AddTestAccept(int controlPlanId, string createDate, int testRequestId, string receptionNumber)
        {
            _testAcceptRepository.AddTestAccept(controlPlanId, createDate, testRequestId, receptionNumber);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost] 
        public IActionResult UpdateTestAccept(int id, int controlPlanId, string createDate, int testRequestId,  string receptionNumber)
        {
            _testAcceptRepository.UpdateTestAccept(id, controlPlanId, createDate, testRequestId,   receptionNumber);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteTestAccept(int id)
        {
            _testAcceptRepository.DeleteTestAccept(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SendToKartabl(int id)
        {
            _testAcceptRepository.SendToKartabl(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public bool CheckReceptionNumber(string receptionNumber)
        {
          var result=  _testAcceptRepository.CheckReceptionNumber(receptionNumber);
            return result;
        }
        #endregion 

        #region ============================= TestAcceptDetail =============================

        public IActionResult GetTestAcceptDetails(int TestAcceptId)
        {
            var result = _testAcceptRepository.GetTestAcceptDetails(TestAcceptId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        public IActionResult GetTestAcceptDetailsForInsertPopup(int controlPlanId, int testRequestId)
        {
            var result = _testAcceptRepository.GetTestAcceptDetailsForInsertPopup(controlPlanId, testRequestId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        public IActionResult GetTesRequestDetailByControlPlanId(int controlPlanId)
        {
            var result = _testAcceptRepository.GetTestAcceptDetailByControlPlanId(controlPlanId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]  
        public IActionResult AddTestAcceptDetail(List<TestAcceptDetailViewModel> TestAcceptDetails)
        {
            _testAcceptRepository.AddTestAcceptDetail(TestAcceptDetails);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost] 
        public IActionResult UpdateTestAcceptDetail(int id, decimal avarage, string toolCode, string fromDate, string endDate, int humidity, int temperature)
        {
            _testAcceptRepository.UpdateTestAcceptDetail(id, avarage, toolCode, fromDate, endDate, humidity, temperature);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteTestAcceptDetail(int id)
        {
            _testAcceptRepository.DeleteTestAcceptDetail(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}