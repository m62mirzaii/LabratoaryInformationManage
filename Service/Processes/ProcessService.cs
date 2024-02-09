using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel; 

namespace Core.Services.Processes
{
    public class ProcessService : IProcessRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Process> _process;
        private readonly DbSet<ProcessType> _processType;
        public ProcessService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _process = dbContext.Set<Process>();
            _processType = dbContext.Set<ProcessType>();
        }

        public async Task<Process> GetProcessById(int id)
        {
            return await _process.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public List<ProcessViewModel> GetProcess()
        {
            var process = _process.AsQueryable();

            var result = process.Select(e => new
            {
                e.Id,
                e.ProcessName,
                e.IsActive,
                e.ProcessTypeId,
                ProcessTypeName = e.ProcessType.Name ?? "",
            }).AsEnumerable()
            .OrderByDescending(e => e.Id)
            .Select(e => new ProcessViewModel
            {
                Id = e.Id,
                ProcessName = e.ProcessName,
                IsActive = e.IsActive,
                ProcessTypeId = e.ProcessTypeId,
                ProcessTypeName = e.ProcessTypeName,
            })
            .ToList();

            return result;
        }
        public void AddProcess(ProcessViewModel process)
        {
            var _item = new Process
            {
                ProcessName = process.ProcessName,
                IsActive = process.IsActive,
                ProcessTypeId = process.ProcessTypeId
            };

            _process.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateProcess(ProcessViewModel process)
        {
            var _item = _process.Find(process.Id);
            if (_item != null)
            {
                _item.ProcessName = process.ProcessName;
                _item.IsActive = process.IsActive;
                _item.ProcessTypeId = process.ProcessTypeId;

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
