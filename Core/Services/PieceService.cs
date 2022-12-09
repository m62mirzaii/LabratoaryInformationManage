using Core.Repository; 
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore; 
using Models.Model;
using Models.ViewModel;

namespace Core.Services
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

        public async Task<List<PieceViewModel>> GetPieces()
        {
            var pieces = await _piece.ToListAsync(); 
            
            var lstPieceViewModel = pieces.Select(x=> new PieceViewModel
            {               
                Id= x.Id,
                Code = x.Code,
                IsActive = x.IsActive,
                PieceName = x.PieceName,
                PieceUsageName = _pieceusage.Where(e=>e.Id == x.PieceUsageId)?.FirstOrDefault()?.UsageName
            }).ToList(); 

            return lstPieceViewModel;
        }

        public void AddPiece(Piece piece)
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

        public bool UpdatePiece(int id, Piece piece)
        {
            var _item = _piece.Find(id);
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
