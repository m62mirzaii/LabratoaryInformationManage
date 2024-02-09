using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Core.Services.SystemMethods
{
    public class SystemMethodService : ISystemMethodRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<SystemMethod> _systemMethodsServices;

        public SystemMethodService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _systemMethodsServices = dbContext.Set<SystemMethod>();
        }

        public async Task<List<SystemMethodViewModel>> GetBySystemId(int systemId)
        {
            var query = _systemMethodsServices.Where(e => e.SystemsId == systemId).AsQueryable();

            var result = query.Select(e => new
            {
                e.Id,
                e.SystemsId,
                e.MethodName,
                SystemName = e.System.Name,
            })
            .AsEnumerable()
            .Select(e => new SystemMethodViewModel
            {
                Id = e.Id,
                SystemId = e.SystemsId,
                SystemName = e.SystemName,
                MethodName = e.MethodName,
            }).ToList();

            return result;
        }
    }
}
