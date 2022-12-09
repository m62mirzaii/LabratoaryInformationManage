using Models.Model;
using Models.ViewModel;

namespace Core.Repository
{
    public interface IProcessRepository
    {
        Task<Process> GetProcessById(int id);
        Task<List<Process>> GetProcess();
        List<Process> GetAllProcess();
        List<ProcessViewModel> GetProcessViewModel();
        void AddProcess(Process process);
        bool UpdateProcess(int id, Process process);
        void DeleteProcess(int id);
    }
}
