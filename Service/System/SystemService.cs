using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.System
{
    public class SystemService : ISystemRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Systems> _system;

        public SystemService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _system = dbContext.Set<Systems>();
        }

        public List<Systems> GetSystems()
        {
            return _system.ToList();
        }
    }
}
