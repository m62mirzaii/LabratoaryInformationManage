using Models.Model;
using Models.ViewModel;

namespace Core.Services.SystemMethods
{
    public interface ISystemMethodRepository
    {
        Task<List<SystemMethodViewModel>> GetBySystemId(int systemId);
        //Task<List<SystemMethod>> GetSystemMethods(); 
    }
}
