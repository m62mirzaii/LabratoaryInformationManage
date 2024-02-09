using Models.Model;
using Models.ViewModel;

namespace Core.Services.SystemMethodUsers
{
    public interface ISystemMethodUserRepository
    {
        List<SystemMethodUserViewModel> GetByUserId(int userId);
        void AddSystemMethodUser(SystemMethodUserViewModel systemMethodUser);

    }
}
