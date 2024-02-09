using Share.Generators; 
using Core.Services.TestAccepts;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;
using WebSite.Filters;

namespace WebSite.Controllers
{

  //  [Authorize(systemName = "TestTestAccept_Kartabl")]

    public class TestAccept_KartablController : Controller
    {
        public ITestAcceptRepository _testAcceptRepository;

        public TestAccept_KartablController(ITestAcceptRepository testAcceptRepository)
        {
            _testAcceptRepository = testAcceptRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTestAcceptForKartabl(int controlPlanId)
        {
            var result = _testAcceptRepository.GetTestAcceptForKartabl(controlPlanId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        public IActionResult GetTestAcceptDetails(int testTestAcceptId)
        {
            var result = _testAcceptRepository.GetTestAcceptDetails(testTestAcceptId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        public void ConfirmTestAccept(int id, List<TestAcceptDetail> TestAcceptDetails)
        {
            _testAcceptRepository.Confirm_TestAccept(id, TestAcceptDetails);
        }

        public void Update_TestAccept_ConfirmCode(int id)
        {
            _testAcceptRepository.Update_TestAccept_ConfirmCode(id);
        }

        public void Return_TestAccept(int id)
        {
            _testAcceptRepository.Return_TestAccept(id);
        }
    }
}