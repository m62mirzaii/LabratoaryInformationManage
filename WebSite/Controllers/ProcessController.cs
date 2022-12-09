using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace WebSite.Controllers
{
    public class ProcessController : Controller
    {
        public IProcessRepository _processService;

        public ProcessController(IProcessRepository usageService)
        {
            _processService = usageService;
        }

        public  IActionResult Index()
        {
            var result = _processService.GetProcessViewModel();
            return View(result);
        }

        public IActionResult GetProcessViewModel()
        {
            var result = _processService.GetProcessViewModel();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public JsonResult GetAllProcess()
        {
            var result = _processService.GetAllProcess();
            return Json(result);
        } 

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            Process process = new Process();
            return PartialView("_InsertPartial", process);
        }

        public IActionResult AddProcess(Process process)
        {
            _processService.AddProcess(process);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Show_UpdatePartial([FromBody] Process lab)
        {
            int id = lab.Id;
            Process process = await _processService.GetProcessById(id);
            return PartialView("_UpdatePartial", process);
        }

        public IActionResult UpdateProcess(Process process)
        {
            int id = process.Id;
            bool result = _processService.UpdateProcess(id, process);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _processService.DeleteProcess(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}