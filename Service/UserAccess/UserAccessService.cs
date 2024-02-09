using DataAccessLayer.Context;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Service.Forms
{
    public class UserAccessService : IUserAccessService
    {

        public DataBaseContext _dbContext;
        private readonly DbSet<UserAccess> _userAccess;
        private readonly DbSet<Systems> _system;
        private readonly DbSet<Users> _users;


        public UserAccessService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _system = dbContext.Set<Systems>();
            _userAccess = dbContext.Set<UserAccess>();
            _users = dbContext.Set<Users>();
        }

        public List<UserAccessViewModel> GetByUserId(int userId)
        {
            if (userId > 0)
            {
                var userAccess = _userAccess
                    .Include(e => e.Systems)
                    .Where(e => e.UserId == userId);

                var result = userAccess
                    .Select(e => new UserAccessViewModel
                    {
                        Id = e.Id,
                        UserId = e.UserId,
                        GroupName = e.Systems.GroupName,
                        NameFa = e.Systems.NameFa,
                        Name = e.Systems.Name,
                        SystemId = e.SystemsId,
                    }).ToList();

                return result;
            }
            return new List<UserAccessViewModel>();
        }

        public List<UserAccessViewModel> GetSystems()
        {
            var result = _system.Select(e => new
            {
                e.Id,
                e.GroupName,
                e.NameFa,
                e.Name,
                SystemId = e.Id
            })
            .AsEnumerable()
            .Select(e => new UserAccessViewModel
            {
                Id = e.Id,
                GroupName = e.GroupName,
                NameFa = e.NameFa,
                Name = e.Name,
                SystemId = e.SystemId,

            }).ToList();

            return result;
        }

        public List<UserAccessViewModel> GetSystemListByUserIdForMenu(int userId)
        {
            if (userId == 0)
                userId = 1;

            var user = _users.Where(e => e.Id == userId).FirstOrDefault();
            var isAdmin = user.IsAdmin;
            if (isAdmin == false)
            {
                var systems = _userAccess
                               .Include(e => e.Systems)
                               .Where(e => e.SystemsId != null && e.UserId == userId);

                var result = systems.Select(e => new UserAccessViewModel
                {
                    Id = e.Id,
                    GroupName = e.Systems.GroupName,
                    Name = e.Systems.Name,
                    NameFa = e.Systems.NameFa,
                }).ToList();

                return result;
            }
            else
            {
                var systems = _system.Where(e => e.IsActive == true);
                var result = systems
                    .Select(e => new UserAccessViewModel
                    {
                        Id = e.Id,
                        GroupName = e.GroupName,
                        Name = e.Name,
                        NameFa = e.NameFa,
                    }).ToList();
                return result;
            }
        }

        public void Add(int userId, List<int> systemIds)
        {
            foreach (var systemId in systemIds)
            {
                var isExist = _userAccess.Where(x => x.UserId == userId && x.SystemsId == systemId).ToList().Count();
                if (isExist == 0)
                {
                    var userAccess = new UserAccess
                    {
                        SystemsId = systemId,
                        UserId = userId,
                    };
                    _userAccess.Add(userAccess);
                }
                _dbContext.SaveChanges();
            }
        }

        public void DeleteByUserId(int userId)
        {

        }

        public void DeleteById(int id)
        {
            var _item = _userAccess.Find(id);
            if (_item != null)
            {
                _userAccess.Remove(_item);
                _dbContext.SaveChanges();
            }  
        }

    }
}
