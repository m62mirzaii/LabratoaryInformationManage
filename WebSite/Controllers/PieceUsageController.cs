using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace WebSite.Controllers
{
    public class PieceUsageController : Controller
    {
        public IPieceUsageRepository _usageService;

        public PieceUsageController(IPieceUsageRepository usageService)
        {
            _usageService = usageService;
        }

        public async Task<IActionResult> Index()
        {
            var result =  await _usageService.GetPieceUsages();
            return View(result);
        }

        [HttpPost]
        public JsonResult GetActivePieceUsages()
        {
            var result = _usageService.GetActivePieceUsages();
            return Json(result);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            PieceUsage pieceUsage = new PieceUsage();
            return PartialView("_InsertPartial", pieceUsage);
        }

        public IActionResult AddPieceUsage(PieceUsage pieceUsage)
        {
            _usageService.AddPieceUsage(pieceUsage);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Show_UpdatePartial([FromBody] PieceUsage lab)
        {
            int id = lab.Id;
            PieceUsage pieceUsage = await _usageService.GetPieceUsageById(id);
            return PartialView("_UpdatePartial", pieceUsage);
        }

        public IActionResult UpdatePieceUsage(PieceUsage pieceUsage)
        {
            int id = pieceUsage.Id;
            bool result = _usageService.UpdatePieceUsage(id, pieceUsage);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _usageService.DeletePieceUsage(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}