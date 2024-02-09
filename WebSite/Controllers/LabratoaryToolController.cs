using Core.Services.LabratoryTools;
using Core.Services.Tests;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{

    //[Authorize(systemName = "LabratoaryTool")]

    public class LabratoaryToolController : Controller
    {
        public ILabratoaryToolRepository _labratoaryToolService;

        public LabratoaryToolController(ILabratoaryToolRepository toolRepository)
        {
            _labratoaryToolService = toolRepository;
        }

        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult GetActiveTools()
        {  
            var result = _labratoaryToolService.GetLabratoaryTools();
            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }


        [HttpPost]
        public JsonResult GetLabratoaryToolForSelect2(string searchTerm)
        {
            var result = _labratoaryToolService.GetLabratoaryToolForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost] 
        public void AddLabratoaryTool()
        {
            var toolName = Request.Form["ToolName"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var labratoaryTool = new LabratoaryTool
            {
                ToolName = toolName,
                IsActive = IsActive,
            };
            _labratoaryToolService.AddLabratoaryTool(labratoaryTool); 
        }

        [HttpPost] 
        public void UpdateLabratoaryTool( )
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString()); 
            var toolName = Request.Form["ToolName"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var labratoaryTool = new LabratoaryTool
            {
                Id = id,
                ToolName = toolName,
                IsActive = IsActive,
            };
            _labratoaryToolService.UpdateLabratoaryTool(  labratoaryTool); 
        }

        public IActionResult Delete(int id)
        {
            _labratoaryToolService.DeleteLabratoaryTool(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}