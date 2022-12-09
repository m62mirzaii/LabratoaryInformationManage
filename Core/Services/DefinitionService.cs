using Core.Repository; 
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services
{
    public class DefinitionService : IDefinitionRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Definition> _definition;

        public DefinitionService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _definition = dbContext.Set<Definition>();
        }

        public async Task<Definition> GetDefinitionById(int id)
        {
            return await _definition.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public async Task<List<Definition>> GetDefinitions()
        {
            return await _definition.ToListAsync();
        }

        public List<Definition> GetAllDefinitions()
        {
            return _definition.ToList();
        }

        public void AddDefinition(Definition definition)
        {
            var _item = new Definition
            {
                ProcessId = definition.Id 
            };

            _definition.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateDefinition(int id, Definition definition)
        {
            //var _item = _definition.Find(id);
            //if (_item != null)
            //{
            //    _item.UsageName = Definition.UsageName;
            //    _item.IsActive = Definition.IsActive;

            //    _dbContext.SaveChanges();
            //    return true;
            //}
            //else
                return false;
        }

        public void DeleteDefinition(int id)
        {
            var _item = _definition.Find(id);
            if (_item != null)
            {
                _definition.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
