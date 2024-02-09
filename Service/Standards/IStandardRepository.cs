using Models.Model;

namespace Core.Services.Standards
{
    public interface IStandardRepository
    {
        Task<Standard> GetStandardById(int id);
        Task<List<Standard>> GetStandards_Async();
        List<Standard> GetStandards();
        List<Standard> GetStandardForSelect2(string searchTerm);
        void AddStandard(Standard standard);
        bool UpdateStandard(Standard standard);
        void DeleteStandard(int id);
    }
}
