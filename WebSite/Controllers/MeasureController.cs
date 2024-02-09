using Core.Services.Measures;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{

    //[Authorize(systemName = "Measure")]

    public class MeasureController : Controller
    {
        public IMeasureRepository _measureService;

        public MeasureController(IMeasureRepository measureService)
        {
            _measureService = measureService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMeasureForSelect2(string searchTerm)
        {
            var result = _measureService.GetMeasureForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetMeasures()
        {
            var result = _measureService.GetMeasures();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public void AddMeasure()
        {
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var measure = new Measure
            {
                Name = name,
                IsActive = IsActive,
            };
            _measureService.AddMeasure(measure);
        }

        [HttpPost]
        public void UpdateMeasure()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var measure = new Measure
            {
                Id = id,
                Name = name,
                IsActive = IsActive,
            };
            _measureService.UpdateMeasure(measure);
        }

        public IActionResult Delete(int id)
        {
            _measureService.DeleteMeasure(id);
            return RedirectToAction(nameof(Index));
        }
    }
}