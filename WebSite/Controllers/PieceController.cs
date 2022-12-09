using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace WebSite.Controllers
{
    public class PieceController : Controller
    {
        public IPieceRepository _pieceService;

        public PieceController(IPieceRepository peiceService)
        {
            _pieceService = peiceService;
        }

        public async Task<IActionResult> Index()
        {
            var result =  await _pieceService.GetPieces();
            return View(result);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            Piece piece = new Piece();
            return PartialView("_InsertPartial", piece);
        }

        public IActionResult AddPiece(Piece piece)
        {
            _pieceService.AddPiece(piece);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Show_UpdatePartial([FromBody] Piece lab)
        {
            int id = lab.Id;
            Piece piece = await _pieceService.GetPieceById(id);
            return PartialView("_UpdatePartial", piece);
        }

        public IActionResult UpdatePiece(Piece piece)
        {
            int id = piece.Id;
            bool result = _pieceService.UpdatePiece(id, piece);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _pieceService.DeletePiece(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}