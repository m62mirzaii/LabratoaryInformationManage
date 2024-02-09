using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.LabratoryTools
{
    public class LabratoaryToolService : ILabratoaryToolRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<LabratoaryTool> _labratoaryTool;

        public LabratoaryToolService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _labratoaryTool = dbContext.Set<LabratoaryTool>();
        }

        public async Task<LabratoaryTool> GetLabratoaryToolById(int id)
        {
            return await _labratoaryTool.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public List<LabratoaryTool> GetLabratoaryTools()
        {
            return _labratoaryTool.OrderByDescending(e => e.Id).ToList();
        }

        public List<LabratoaryTool> GetLabratoaryToolForSelect2(string searchTerm)
        {
            var query = _labratoaryTool.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.ToolName.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddLabratoaryTool(LabratoaryTool LabratoaryTool)
        {
            var _item = new LabratoaryTool
            {
                IsActive = LabratoaryTool.IsActive,
                ToolName = LabratoaryTool.ToolName
            };

            _labratoaryTool.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateLabratoaryTool(  LabratoaryTool LabratoaryTool)
        {
            var _item = _labratoaryTool.Find(LabratoaryTool.Id);
            if (_item != null)
            {
                _item.ToolName = LabratoaryTool.ToolName;
                _item.IsActive = LabratoaryTool.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteLabratoaryTool(int id)
        {
            var _item = _labratoaryTool.Find(id);
            if (_item != null)
            {
                _labratoaryTool.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
