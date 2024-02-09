using Share.Generators;
using Core.Services.TestRequests;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModel;
using WebSite.Filters;

namespace WebSite.Controllers
{

  //  [Authorize(systemName = "TestRequest")]
    public class TestRequestController : Controller
    {
        public ITestRequestRepository _testRequestRepository;

        public TestRequestController(ITestRequestRepository TestRequestRepository)
        {
            _testRequestRepository = TestRequestRepository;
        }


        public IActionResult Index()
        {
            ViewBag.MyDate = DateTimeGenerator.GetShamsiDate(DateTime.Now.Date);
            return View();
        }


        #region ============================= TestRequest =============================
        public IActionResult GetTestRequests()
        {
            var result = _testRequestRepository.GetTesRequests();
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public JsonResult GetTestRequestForSelect2(string searchTerm)
        {
            var result = _testRequestRepository.GetTestRequestForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]
        public IActionResult AddTestRequest(int requestNumber, string requestDate, int requestUnitId, int requestUserId, int pieceId, int companyId)
        {
            _testRequestRepository.AddTestRequest(  requestNumber,   requestDate,   requestUnitId, requestUserId,   pieceId, companyId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost] 
        public IActionResult UpdateTestRequest(int id, int requestNumber, string requestDate, int requestUnitId, int requestUserId, int pieceId, int companyId)
        {
            _testRequestRepository.UpdateTestRequest(id, requestNumber, requestDate, requestUnitId, requestUserId, pieceId, companyId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteTestRequest(int id)
        {
            _testRequestRepository.DeleteTestRequest(id);
            return RedirectToAction(nameof(Index));
        }

    
        #endregion 

        #region ============================= TestRequestDetail =============================

        public IActionResult GetTestRequestDetails(int testRequestId)
        {
            var result = _testRequestRepository.GetTesRequestDetails(testRequestId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }  

        [HttpPost] 
        public IActionResult AddTestRequestDetail(int testRequestId, List<int> testIds)
        {
            _testRequestRepository.AddTestRequestDetail(  testRequestId, testIds);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost] 
        public IActionResult UpdateTestRequestDetail(int id,   string testName )
        {
            _testRequestRepository.UpdateTestRequestDetail(id, testName);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteTestRequestDetail(int id)
        {
            _testRequestRepository.DeleteTestRequestDetail(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}