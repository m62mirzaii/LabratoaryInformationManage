using Core.Services.PieceUsages;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{

    //[Authorize(systemName = "PieceUsage")]
    public class PieceUsageController : Controller
    { 
        public IPieceUsageRepository _usageService; 
        public PieceUsageController(IPieceUsageRepository usageService)
        {
            _usageService = usageService; 
        } 

        public IActionResult Index()
        {  
            return View();
        } 

        [HttpPost]
        public IActionResult GetPieceUsages()
        {
            var result = _usageService.GetPieceUsages();
            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        } 
        //[HttpPost]
        //public JsonResult GetActivePieceUsages()
        //{
        //    var result = _usageService.GetActivePieceUsages();
        //    return Json(result);
        //}

        [HttpPost]
        public JsonResult PieceUsageForSelect2(string searchTerm)
        {
            var result = _usageService.PieceUsageForSelect2(searchTerm);
            return Json(result);
        }  

        public void AddPieceUsage( )
        { 
            var usageName = Request.Form["UsageName"].ToString();            
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var pieceUsage = new PieceUsage
            { 
                UsageName = usageName, 
                IsActive = IsActive,
            }; 
            _usageService.AddPieceUsage(pieceUsage); 
        }  

        public void UpdatePieceUsage( )
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var usageName = Request.Form["UsageName"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var pieceUsage = new PieceUsage
            {
                Id= id,
                UsageName = usageName,
                IsActive = IsActive,
            };
           _usageService.UpdatePieceUsage( pieceUsage); 
        }

        public IActionResult Delete(int id)
        {
            _usageService.DeletePieceUsage(id);
            return RedirectToAction(nameof(Index));
        }

    }
}