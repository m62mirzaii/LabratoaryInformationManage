using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Core.Services.SystemMethodUsers
{
    public class SystemMethodUserService : ISystemMethodUserRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<SystemMethodUser> _systemMethodUser;

        public SystemMethodUserService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _systemMethodUser = dbContext.Set<SystemMethodUser>();
        }

        public List<SystemMethodUserViewModel> GetByUserId(int userId)
        {
            var query = _systemMethodUser.Where(e => e.UserId == userId).AsQueryable();

            var result = query.Select(e => new
            {
                e.Id,
                e.UserId,
                e.SystemMethodId,
                e.SystemMethod.MethodName,
                SystemId = e.SystemMethod.SystemsId,
                SystemName = e.SystemMethod.System.Name,
            })
            .AsEnumerable()
            .Select(e => new SystemMethodUserViewModel
            {
                Id = e.Id,
                UserId = e.UserId,
                SystemMethodId = e.SystemMethodId,
                MethodName = e.MethodName,
                SystemId = e.SystemId,
                SystemName = e.SystemName,
            }).ToList();

            return result;
        }

        public void AddSystemMethodUser(SystemMethodUserViewModel systemMethodUser)
        {
            var item = new SystemMethodUser
            {
                SystemMethodId = systemMethodUser.SystemMethodId,
                UserId = systemMethodUser.UserId
            };

            _systemMethodUser.Add(item);
            _dbContext.SaveChanges();
        }

    }
}
