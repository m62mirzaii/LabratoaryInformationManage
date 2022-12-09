using Core.Repository; 
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services
{
    public class PieceUsageService : IPieceUsageRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<PieceUsage> _pieceUsage;

        public PieceUsageService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _pieceUsage = dbContext.Set<PieceUsage>();
        }

        public async Task<PieceUsage> GetPieceUsageById(int id)
        {
            return await _pieceUsage.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public async Task<List<PieceUsage>> GetPieceUsages()
        {
            return await _pieceUsage.ToListAsync();
        }

        public List<PieceUsage> GetActivePieceUsages()
        {
            return _pieceUsage.Where(x => x.IsActive == true).ToList();
        }

        public void AddPieceUsage(PieceUsage pieceUsage)
        {
            var _item = new PieceUsage
            {
                IsActive = pieceUsage.IsActive,
                UsageName = pieceUsage.UsageName
            };

            _pieceUsage.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdatePieceUsage(int id, PieceUsage pieceUsage)
        {
            var _item = _pieceUsage.Find(id);
            if (_item != null)
            {
                _item.UsageName = pieceUsage.UsageName;
                _item.IsActive = pieceUsage.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeletePieceUsage(int id)
        {
            var _item = _pieceUsage.Find(id);
            if (_item != null)
            {
                _pieceUsage.Remove(_item);
                _dbContext.SaveChanges();
            }
        }

        
    }
}
