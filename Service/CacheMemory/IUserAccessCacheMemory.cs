using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CacheMemory
{
    public interface IUserAccessCacheMemory
    {
        public void SaveUserAccess(int userId);
        public bool CheckUserAccess(int userId, string systemName);
    }
}
