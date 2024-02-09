
using Models.ViewModel;

namespace Service.Forms
{
    public interface IUserAccessService
    {
         List<UserAccessViewModel>  GetByUserId(int userId);
        List<UserAccessViewModel> GetSystems();
         List<UserAccessViewModel> GetSystemListByUserIdForMenu(int userId); 
        void Add(int userId, List<int> systemIds);  
        void DeleteByUserId(int userId);
        void DeleteById(int id);
    }
}
