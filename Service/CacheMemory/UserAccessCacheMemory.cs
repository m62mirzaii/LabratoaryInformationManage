using Core.Services.SystemUsers;
using Microsoft.Extensions.Caching.Memory;
using Models.ViewModel;
using Service.User;

namespace Core.CacheMemory
{
    public class UserAccessCacheMemory : IUserAccessCacheMemory
    {
        private const string keyName = "userAccess_";
        private IMemoryCache _memoryCache;
        private readonly ISystemUserRepository systemUserrepository;
        private readonly IUserRepository userRepository;
        public UserAccessCacheMemory(IMemoryCache memoryCache, ISystemUserRepository systemUserrepository, IUserRepository userRepository)
        {
            _memoryCache = memoryCache;
            this.systemUserrepository = systemUserrepository;
            this.userRepository = userRepository;
        }

        public void SaveUserAccess(int userId)
        {
            //var result = new List<SystemMethodUserViewModel>();
            //string key = keyName + userId.ToString();

            //if (!_memoryCache.TryGetValue(key, out result))
            //{ 
            //    result = systemUserrepository.GetByUserId(userId);
            //    _memoryCache.Set(key, result);
            //} 
        }

        public bool CheckUserAccess(int userId, string systemName)
        {
            string key = keyName + userId.ToString();

            if (!_memoryCache.TryGetValue(key, out UserViewModel users))
                _memoryCache.Set(key, userRepository.GetUserById(userId));
            if (_memoryCache.TryGetValue(key,out UserViewModel user))
            {
                if (user.IsAdmin)
                    return true;
            }

            if (!_memoryCache.TryGetValue(key, out List<SystemUserViewModel> lst))
                _memoryCache.Set(key, systemUserrepository.GetByUserId(userId));

            var result = _memoryCache.TryGetValue(key, out List<SystemUserViewModel> userAccess);
            if (result)
            {
                var access = userAccess.Where(s => s.SystemName == systemName || s.IsAdmin == true).ToList();

                if (access.Any())
                    return true;
            }

            return false;
        }
    }
}
