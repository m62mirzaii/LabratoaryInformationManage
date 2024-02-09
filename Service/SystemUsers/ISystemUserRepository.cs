using Models.ViewModel;
using Models.Model;

namespace Core.Services.SystemUsers
{
    public interface ISystemUserRepository
    {
        List<SystemUserViewModel> GetUsers();
        List<SystemUserViewModel> GetByUserId(int userId);
        void AddSystemUser(int userId, List<int> systemIds);
        void UpdateSystemUser(int userId, List<int> systemIds);
        void DeleteByUserId(int userId);
        void DeleteById(int id);
    }
}
