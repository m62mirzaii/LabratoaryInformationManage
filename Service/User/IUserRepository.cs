using Models.Model;
using Models.ViewModel;

namespace Service.User;

public interface IUserRepository
{
    UserViewModel GetUserById(int id);
    List<UserViewModel> GetUsers();
    List<UserViewModel> GetUserForSelect2(string searchTerm);
    void AddUser(Users user);
    bool UpdateUser(UserViewModel user);
    void DeleteUser(int id);
    Users CheckLogin(string userName, string password);

}
