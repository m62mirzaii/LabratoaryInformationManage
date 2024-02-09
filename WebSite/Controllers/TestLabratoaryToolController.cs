using Core.Services.Pieces;
using Core.Services.TestLabratoryTools;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;

namespace WebSite.Controllers
{
    public class TestLabratoaryToolController : Controller
    {
        private ITestLabratoaryToolRepository _testLabratoaryToolRepository;

        public TestLabratoaryToolController(ITestLabratoaryToolRepository testLabratoaryToolRepository)
        {
            _testLabratoaryToolRepository = testLabratoaryToolRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetTestLabratoaryTooles()
        {
            var result = _testLabratoaryToolRepository.Get();
            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public void InsertTestLabratoaryTool()
        {
            
            var labratoaryToolId = Convert.ToInt32(Request.Form["LabratoaryToolId"].ToString());
            var testId = Convert.ToInt32(Request.Form["TestId"].ToString()); 

            var testLabratoaryTool  = new TestLabratoaryToolViewModel
            {
                LabratoaryToolId = labratoaryToolId,
                TestId = testId
            };
            _testLabratoaryToolRepository.Add(testLabratoaryTool);
        }

        [HttpPost]
        public void UpdateTestLabratoaryTool()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var labratoaryToolId = Convert.ToInt32(Request.Form["LabratoaryToolId"].ToString());
            var testId = Convert.ToInt32(Request.Form["TestId"].ToString()); 

            var testLabratoaryTool = new TestLabratoaryToolViewModel
            {
                Id = id,
                LabratoaryToolId = labratoaryToolId,
                TestId = testId
            };
            _testLabratoaryToolRepository.Update(testLabratoaryTool);
        }

        public IActionResult Delete(int id)
        {
            _testLabratoaryToolRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
