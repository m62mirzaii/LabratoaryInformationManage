using Core.Generators;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;
using System;

namespace WebSite.Controllers
{
    public class TestController : Controller
    {
        public ITestRepository _testService;

        public TestController(ITestRepository usageService)
        {
            _testService = usageService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {   
                var result = await _testService.GetTests();
                return View(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            ViewBag.MyDate = DateTimeGenerator.GetShamsiDate(DateTime.Now.Date);
            TestViewModel test = new TestViewModel();
            return PartialView("_InsertPartial", test);
        }

        public IActionResult AddTest(TestViewModel test)
        {
            _testService.AddTest(test);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Show_UpdatePartial([FromBody] TestViewModel test)
        {
            int id = test.Id;
            TestViewModel _test = await _testService.GetTestById(id);
            return PartialView("_UpdatePartial", _test);
        }

        public IActionResult UpdateTest(TestViewModel test)
        {
            int id = test.Id;
            bool result = _testService.UpdateTest(id, test);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _testService.DeleteTest(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}