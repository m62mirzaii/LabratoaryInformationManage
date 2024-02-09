using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.Standards
{
    public class StandardService : IStandardRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Standard> _standard;

        public StandardService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _standard = dbContext.Set<Standard>();
        }

        public async Task<Standard> GetStandardById(int id)
        {
            return await _standard.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public async Task<List<Standard>> GetStandards_Async()
        {
            return await _standard.ToListAsync();
        }

        public List<Standard> GetStandards()
        {
            return _standard
            .OrderByDescending(e => e.Id)
            .ToList();
        }

        public List<Standard> GetStandardForSelect2(string searchTerm)
        {
            var query = _standard.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddStandard(Standard standard)
        {
            var _item = new Standard
            {
                IsActive = standard.IsActive,
                Name = standard.Name
            };

            _standard.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateStandard(  Standard standard)
        {
            var _item = _standard.Find(standard.Id);
            if (_item != null)
            {
                _item.Name = standard.Name;
                _item.IsActive = standard.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteStandard(int id)
        {
            var _item = _standard.Find(id);
            if (_item != null)
            {
                _standard.Remove(_item);
                _dbContext.SaveChanges();
            }
        } 
    }
}
