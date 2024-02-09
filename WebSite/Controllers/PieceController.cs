using Core.Services.Pieces;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;
using WebSite.Filters;

namespace WebSite.Controllers
{

   // [Authorize(systemName = "Piece")]

    public class PieceController : Controller
    {
        private const int SystemId = 5;
        public IPieceRepository _pieceService;

        public PieceController(IPieceRepository peiceService)
        {
            _pieceService = peiceService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPieces()
        {
            var result = _pieceService.GetPieces();
            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        public JsonResult GetPieceForSelect2(string searchTerm)
        {
            var result = _pieceService.GetPieceForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]
        public void InsertPiece()
        {
            var code = Request.Form["Code"].ToString();
            var pieceUsageId = Convert.ToInt32(Request.Form["PieceUsageId"].ToString());
            var pieceName = Request.Form["PieceName"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());


            var piece = new PieceViewModel
            {
                Code = code,
                PieceName = pieceName,
                PieceUsageId = pieceUsageId,
                IsActive = IsActive,
            };
            _pieceService.AddPiece(piece);
        }

        [HttpPost]
        public void UpdatePiece()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var code = Request.Form["Code"].ToString();
            var pieceUsageId = Convert.ToInt32(Request.Form["PieceUsageId"].ToString());
            var pieceName = Request.Form["PieceName"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());


            var piece = new PieceViewModel
            {
                Id = id,
                Code = code,
                PieceName = pieceName,
                PieceUsageId = pieceUsageId,
                IsActive = IsActive,
            };
            _pieceService.UpdatePiece(piece);
        }

        public IActionResult Delete(int id)
        {
            _pieceService.DeletePiece(id);
            return RedirectToAction(nameof(Index));
        }
         
    }
}