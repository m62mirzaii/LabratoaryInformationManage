using Models.Model;

namespace Core.Services.System
{
    public interface ISystemRepository
    {
        //Task<System> GetById(int id);
        List<Systems> GetSystems();
    }
}
