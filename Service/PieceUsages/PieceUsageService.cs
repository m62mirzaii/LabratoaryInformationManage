using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.PieceUsages
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

        public List<PieceUsage> GetPieceUsages()
        {
            var result = _pieceUsage.OrderByDescending(e => e.Id).ToList();
            return result;
        }

        public List<PieceUsage> PieceUsageForSelect2(string searchTerm) 
        {
            var query = _pieceUsage.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.UsageName.Contains(searchTerm));

            var result = query.ToList();
            return result;
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

        public bool UpdatePieceUsage(  PieceUsage pieceUsage)
        {
            var _item = _pieceUsage.Find(pieceUsage.Id);
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
