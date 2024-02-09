using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.ProcessTypes
{
    public class ProcessTypeService : IProcessTypeRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<ProcessType> _processType;

        public ProcessTypeService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _processType = dbContext.Set<ProcessType>();
        }

        public async Task<ProcessType> GetProcessTypeById(int id)
        {
            return await _processType.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public List<ProcessType> GetProcessTypes()
        {
            return _processType
            .OrderByDescending(e => e.Id)
            .ToList();
        }

        public List<ProcessType> GetProcessTypeForSelect2(string searchTerm)
        {
            var query = _processType.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddProcessType(ProcessType processType)
        {
            var _item = new ProcessType
            {
                IsActive = processType.IsActive,
                Name = processType.Name
            };

            _processType.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateProcessType(ProcessType processType)
        {
            var _item = _processType.Find(processType.Id);
            if (_item != null)
            {
                _item.Name = processType.Name;
                _item.IsActive = processType.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteProcessType(int id)
        {
            var _item = _processType.Find(id);
            if (_item != null)
            {
                _processType.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
