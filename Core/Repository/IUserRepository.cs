using Models.Model;
using Models.ViewModel;

namespace Core.Repository
{
    public interface IUserRepository
    {
        UserViewModel GetUserById(int id);
        Task<List<UserViewModel>> GetUsers();
        void AddUser(Users user);
        bool UpdateUser(int id, UserViewModel user);
        void DeleteUser(int id);
        Users CheckLogin(string userName, string password);

    }
}
