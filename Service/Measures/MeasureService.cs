using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.Measures
{
    public class MeasureService : IMeasureRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Measure> _measure;

        public MeasureService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _measure = dbContext.Set<Measure>();
        }

        public async Task<Measure> GetMeasureById(int id)
        {
            return await _measure.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        } 

        public List<Measure> GetMeasures()
        {
            return _measure.OrderByDescending(e => e.Id).ToList();
        }

        public List<Measure> GetMeasureForSelect2(string searchTerm)
        {
            var query = _measure.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddMeasure(Measure measure)
        {
            var _item = new Measure
            {
                IsActive = measure.IsActive,
                Name = measure.Name
            };

            _measure.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateMeasure(  Measure measure)
        {
            var _item = _measure.Find(measure.Id);
            if (_item != null)
            {
                _item.Name = measure.Name;
                _item.IsActive = measure.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteMeasure(int id)
        {
            var _item = _measure.Find(id);
            if (_item != null)
            {
                _measure.Remove(_item);
                _dbContext.SaveChanges();
            }
        } 
    }
}
