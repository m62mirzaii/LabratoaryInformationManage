using Models.Model;

namespace Core.Services.ProcessTypes
{
    public interface IProcessTypeRepository
    {
        Task<ProcessType> GetProcessTypeById(int id);
        List<ProcessType> GetProcessTypes();
        List<ProcessType> GetProcessTypeForSelect2(string searchTerm);
        void AddProcessType(ProcessType processType);
        bool UpdateProcessType(ProcessType processType);
        void DeleteProcessType(int id);
    }
}
