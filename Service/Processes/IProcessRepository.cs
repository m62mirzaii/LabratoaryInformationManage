using Models.Model;
using Models.ViewModel;

namespace Core.Services.Processes
{
    public interface IProcessRepository
    {
        Task<Process> GetProcessById(int id);
        List<ProcessViewModel> GetProcess();
        void AddProcess(ProcessViewModel process);
        bool UpdateProcess(ProcessViewModel process);
        void DeleteProcess(int id);
    }
}
