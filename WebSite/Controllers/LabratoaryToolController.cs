using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace WebSite.Controllers
{
    public class LabratoaryToolController : Controller
    {
        public ILabratoaryToolRepository _labratoaryToolService;

        public LabratoaryToolController(ILabratoaryToolRepository toolRepository)
        {
            _labratoaryToolService = toolRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _labratoaryToolService.GetLabratoaryTools();
            return View(result);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            LabratoaryTool labratoaryTool = new LabratoaryTool();
            return PartialView("_InsertPartial", labratoaryTool);
        }

        public IActionResult AddLabratoaryTool(LabratoaryTool labratoaryTool)
        {
            _labratoaryToolService.AddLabratoaryTool(labratoaryTool);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Show_UpdatePartial([FromBody] LabratoaryTool lab)
        {
            int id = lab.Id;
            LabratoaryTool labratoaryTool = await _labratoaryToolService.GetLabratoaryToolById(id);
            return PartialView("_UpdatePartial", labratoaryTool);
        }

        public IActionResult UpdateLabratoaryTool(LabratoaryTool labratoaryTool)
        {
            int id = labratoaryTool.Id;
            bool result = _labratoaryToolService.UpdateLabratoaryTool(id, labratoaryTool);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _labratoaryToolService.DeleteLabratoaryTool(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}