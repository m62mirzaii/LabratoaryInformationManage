using Core.Services.RequestUnits;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{

   // [Authorize(systemName = "RequestUnit")]

    public class RequestUnitController : Controller
    {
        public IRequestUnitRepository _requestUnitService;

        public RequestUnitController(IRequestUnitRepository RequestUnitService)
        {
            _requestUnitService = RequestUnitService;
        }

        public async Task<IActionResult> Index()
        { 
            return View( );
        } 

        [HttpPost]
        public IActionResult GetRequestUnits()
        {
            var result = _requestUnitService.GetRequestUnits();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public JsonResult GetRequestUnitForSelect2(string searchTerm)
        {
            var result = _requestUnitService.GetRequestUnitForSelect2(searchTerm);
            return Json(result);
        }


        [HttpPost]  
        public IActionResult AddRequestUnit(    )
        {
            var name = Request.Form["Name"].ToString();
            var requestUnit = new RequestUnit
            {
                Name = name,
            };
            _requestUnitService.AddRequestUnit(requestUnit);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost] 
        public IActionResult UpdateRequestUnit( )
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();
            var requestUnit = new RequestUnit
            {
                Id = id,
                Name = name,
            };
            bool result = _requestUnitService.UpdateRequestUnit(requestUnit);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _requestUnitService.DeleteRequestUnit(id);
            return RedirectToAction(nameof(Index));
        }
         
         
    }
}