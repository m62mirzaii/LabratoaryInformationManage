using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.RequestUnits
{
    public class RequestUnitService : IRequestUnitRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<RequestUnit> _requestUnit;

        public RequestUnitService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _requestUnit = dbContext.Set<RequestUnit>();
        }

        public async Task<RequestUnit> GetRequestUnitById(int id)
        {
            return await _requestUnit.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public List<RequestUnit>  GetRequestUnitForSelect2(string searchTerm)
        {
            var query = _requestUnit.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public List<RequestUnit> GetRequestUnits()
        {
            return _requestUnit
            .OrderByDescending(e => e.Id)
            .ToList();
        }

        public void AddRequestUnit(RequestUnit requestUnit)
        {
            var _item = new RequestUnit
            {
                Name = requestUnit.Name
            };

            _requestUnit.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateRequestUnit(  RequestUnit requestUnit)
        {
            var _item = _requestUnit.Find(requestUnit.Id);
            if (_item != null)
            {
                _item.Name = requestUnit.Name;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteRequestUnit(int id)
        {
            var _item = _requestUnit.Find(id);
            if (_item != null)
            {
                _requestUnit.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
