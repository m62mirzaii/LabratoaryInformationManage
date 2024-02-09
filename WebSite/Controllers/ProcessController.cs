using Core.Services.Processes;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModel;
using WebSite.Filters;

namespace WebSite.Controllers
{

   // [Authorize(systemName = "Process")]

    public class ProcessController : Controller
    {
        public IProcessRepository _processService;

        public ProcessController(IProcessRepository usageService)
        {
            _processService = usageService;
        }

        public  IActionResult Index()
        {
            return View();
        } 

        [HttpPost]
        public IActionResult GetProcesses()
        {
            var result = _processService.GetProcess(); 
            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }  

        [HttpPost] 

        public void InsertProcess()
        { 
            var processTypeId = Convert.ToInt32(Request.Form["ProcessTypeId"].ToString());
            var processName = Request.Form["ProcessName"].ToString();
            var isActive = Convert.ToBoolean(Request.Form["IsActive"].ToString()); 

            var process = new ProcessViewModel
            { 
                ProcessName = processName,
                ProcessTypeId = processTypeId,
                IsActive = isActive,
            };
            _processService.AddProcess(process); 
        }

        [HttpPost]
        public void UpdateProcess()
        {
            //int id = process.Id;
            

            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var processTypeId = Convert.ToInt32(Request.Form["ProcessTypeId"].ToString());
            var processName = Request.Form["ProcessName"].ToString();
            var isActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());

            var process = new ProcessViewModel
            {
                Id= id,
                ProcessName = processName,
                ProcessTypeId = processTypeId,
                IsActive = isActive,
            };

             _processService.UpdateProcess(process); 
        }

        public IActionResult Delete(int id)
        {
            _processService.DeleteProcess(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}