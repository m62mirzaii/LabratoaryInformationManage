using Core.Services.Standards;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{

 //   [Authorize(systemName = "Standard")]

    public class StandardController : Controller
    {
        public IStandardRepository _standardService;

        public StandardController(IStandardRepository standardService)
        {
            _standardService = standardService;
        }

        public IActionResult Index()
        {
            return View();
        } 
       

        [HttpPost]
        public JsonResult GetStandardForSelect2(string searchTerm)
        {
            var result = _standardService.GetStandardForSelect2(searchTerm);
            return Json(result);
        }

        [HttpGet]
        public IActionResult GetStandards()
        {
            var result = _standardService.GetStandards();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public void AddStandard()
        {
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var standard = new Standard
            {
                Name = name,
                IsActive = IsActive,
            };
            _standardService.AddStandard(standard);

        }

        [HttpPost]
        public void UpdateStandard(Standard pieceUsage)
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var standard = new Standard
            {
                Id = id,
                Name = name,
                IsActive = IsActive,
            };
            _standardService.UpdateStandard(standard);

        }

        public IActionResult Delete(int id)
        {
            _standardService.DeleteStandard(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}