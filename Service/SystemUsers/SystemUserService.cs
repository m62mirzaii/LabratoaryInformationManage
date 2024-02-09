using Share.Generators;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using System.Diagnostics;

namespace Core.Services.SystemUsers
{
    public class SystemUserService : ISystemUserRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<SystemUser> _systemUser;

        public SystemUserService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _systemUser = dbContext.Set<SystemUser>();
        }

        public List<SystemUserViewModel> GetUsers()
        {
            var systemUser = _systemUser.AsQueryable();

            var result = systemUser.Select(e => new
            {
                userId = e.User.Id,
                FullNameUser = e.User.FirstName + ' ' + e.User.LastName ?? "",
            })
            .AsEnumerable()
            .Distinct()
            .Select(e => new SystemUserViewModel
            {
                UserId = e.userId,
                FullNameUser = e.FullNameUser,
            })

            .ToList();

            return result;
        }

        public List<SystemUserViewModel> GetByUserId(int userId)
        {
            if (userId > 0)
            {
                var query = _systemUser.Where(e => e.UserId == userId).AsQueryable();

                var result = query.Select(e => new
                {
                    e.Id,
                    e.UserId,
                    SystemNameFa = e.Systems.NameFa,
                    e.User.IsAdmin
                })
                .AsEnumerable()
                .Select(e => new SystemUserViewModel
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    SystemNameFa = e.SystemNameFa,
                    IsAdmin = e.IsAdmin
                }).ToList();

                return result;
            }

            return new List<SystemUserViewModel>();
        }

        public void AddSystemUser(int userId, List<int> systemIds)
        {
            foreach (var systemId in systemIds)
            {
                var count = _systemUser.Where(x => x.UserId == userId && x.SystemsId == systemId).Count();
                if (count == 0)
                {
                    var systemUser = new SystemUser
                    {
                        SystemsId = systemId,
                        UserId = userId,
                    };
                    _systemUser.Add(systemUser);
                }
                _dbContext.SaveChanges();
            }
        }

        public void UpdateSystemUser(int userId, List<int> systemIds)
        {
            foreach (var systemId in systemIds)
            {
                var count = _systemUser.Where(x => x.UserId == userId && x.SystemsId == systemId).Count();
                if (count == 0)
                {
                    var systemUser = new SystemUser
                    {
                        SystemsId = systemId,
                        UserId = userId,
                    };
                    _systemUser.Add(systemUser);
                }
                _dbContext.SaveChanges();
            }
        }


        public void DeleteByUserId(int userId)
        {
            var userSystems = _systemUser.Where(e => e.User.Id == userId).ToList();
            foreach (var item in userSystems)
            {
                _systemUser.Remove(item);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var _item = _systemUser.Where(e => e.Id == id).FirstOrDefault();
            if (_item != null)
            {
                _systemUser.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
