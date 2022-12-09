using Core.Repository;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services
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

        public async Task<List<LabratoaryTool>> GetLabratoaryTools()
        {
            return await _labratoaryTool.ToListAsync();
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

        public bool UpdateLabratoaryTool(int id, LabratoaryTool LabratoaryTool)
        {
            var _item = _labratoaryTool.Find(id);
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
