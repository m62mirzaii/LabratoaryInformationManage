using Core.Repository;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Core.Services
{
    public class ProcessService : IProcessRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Process> _process;
        private readonly DbSet<Definition> _definition;
        public ProcessService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _process = dbContext.Set<Process>();
            _definition = dbContext.Set<Definition>();
        }

        public async Task<Process> GetProcessById(int id)
        {
            return await _process.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public async Task<List<Process>> GetProcess()
        {
            return await _process.ToListAsync();
        }

        public List<Process> GetAllProcess()
        {
            return _process.ToList();
        }

        public List<ProcessViewModel> GetProcessViewModel()
        {
            var process = _process.ToList();
            var results = process.Select(x => new ProcessViewModel
            {
                Id = x.Id,
                ProcessName = x.ProcessName,
                IsActive = x.IsActive,
                DefinitionId = x.DefinitionId,
                DefinitionName = _definition.Where(e => e.Id == x.DefinitionId).FirstOrDefault().Name,
            }).ToList();

            return results;
        }
        public void AddProcess(Process process)
        {
            var _item = new Process
            {
                ProcessName = process.ProcessName,
                IsActive = process.IsActive,
                DefinitionId = process.DefinitionId
            };

            _process.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateProcess(int id, Process process)
        {
            var _item = _process.Find(id);
            if (_item != null)
            {
                _item.ProcessName = process.ProcessName;
                _item.IsActive = process.IsActive;
                _item.DefinitionId = process.DefinitionId;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteProcess(int id)
        {
            var _item = _process.Find(id);
            if (_item != null)
            {
                _process.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
