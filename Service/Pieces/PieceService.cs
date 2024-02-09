using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Core.Services.Pieces
{
    public class PieceService : IPieceRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Piece> _piece;
        private readonly DbSet<PieceUsage> _pieceusage;

        public PieceService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _piece = dbContext.Set<Piece>();
            _pieceusage = dbContext.Set<PieceUsage>();
        }

        public async Task<Piece> GetPieceById(int id)
        {
            return await _piece.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public  List<Piece>  GetPieceForSelect2(string searchTerm)
        {
            var query = _piece.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.PieceName.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public List<PieceViewModel> GetPieces()
        {
            var pieces = _piece.AsQueryable();
            var result = pieces.Select(e => new
            {
                e.Id,
                e.Code,
                e.IsActive,
                e.PieceName,
                e.PieceUsageId,
                UsageName = e.PieceUsage.UsageName ?? "",
            }).AsEnumerable()
            .OrderByDescending(e => e.Id)
            .Select(e => new PieceViewModel
            {
                Id = e.Id,
                Code = e.Code,
                IsActive = e.IsActive,
                PieceName = e.PieceName,
                PieceUsageId = e.PieceUsageId ?? 0,
                PieceUsageName = e.UsageName
            })
            .ToList();

            return result;
        }


        public void AddPiece(PieceViewModel piece)
        {
            var _item = new Piece
            {
                Code = piece.Code,
                PieceName = piece.PieceName,
                PieceUsageId = piece.PieceUsageId,
                IsActive = piece.IsActive,
            };

            _piece.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdatePiece(PieceViewModel piece)
        {
            var _item = _piece.Find(piece.Id);
            if (_item != null)
            {
                _item.Code = piece.Code;
                _item.PieceName = piece.PieceName;
                _item.PieceUsageId = piece.PieceUsageId;
                _item.IsActive = piece.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeletePiece(int id)
        {
            var _item = _piece.Find(id);
            if (_item != null)
            {
                _piece.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
