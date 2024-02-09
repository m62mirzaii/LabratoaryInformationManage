using Core.Services.Measures;
using Core.Services.ProcessTypes;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{
  //  [Authorize(systemName = "Process")]

    public class ProcessTypeController : Controller
    {
        public IProcessTypeRepository _processTypeService;

        public ProcessTypeController(IProcessTypeRepository processTypeService)
        {
            _processTypeService = processTypeService;
        }

        public async Task<IActionResult> Index()
        { 
            return View( );
        }

         [HttpPost]
        public JsonResult ProcessTypeForSelect2(string searchTerm)
        {
            var result = _processTypeService.GetProcessTypeForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost] 
        public IActionResult GetProcessTypes()
        {
            var result = _processTypeService.GetProcessTypes();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        } 

        [HttpPost]
        public void AddProcessType()
        {
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var processType = new ProcessType
            {
                Name = name,
                IsActive = IsActive,
            };
            _processTypeService.AddProcessType(processType);
        }

        [HttpPost]
        public void UpdateProcessType()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var processType = new ProcessType
            {
                Id = id,
                Name = name,
                IsActive = IsActive,
            };
            _processTypeService.UpdateProcessType(processType); 
        }

        public IActionResult Delete(int id)
        {
            _processTypeService.DeleteProcessType(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}