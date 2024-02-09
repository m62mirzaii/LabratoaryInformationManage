using Models.Model;
using Models.ViewModel;

namespace Core.Services.ControlPlans
{
    public interface IControlPlanRepository
    {
        ControlPlanViewModel GetControlPlanByIdAsync(int id);
        List<ControlPlanViewModel> GetControlPlans();
        List<ControlPlan> GetControlPlanForSelect2(string searchTerm);
        List<ControlPlanPieceViewModel> GetControlPlanPiece(int controlPlanId);
        List<ControlPlanProcessViewModel> GetControlPlanProcess(int controlPlanId);
        List<ControlPlanProcessTestViewModel> GetControlPlanProcessTest(int ControlPlanProcessId);
        List<ControlPlanProcessTestViewModel> GetControlPlanProcessTestByControlPlanId(int ControlPlanId);

        void AddControlPlan(int companyId, string planNumber, string createDate);
        void AddControlPlanPiece(int ControlPlanId, List<int> lstPieceIds);
        void AddControlPlanProcess(int ControlPlanId, List<int> lstProcessIds);
        void AddControlPlanProcessTest(int ControlPlanProcessId, List<int> testIds, decimal min, decimal max, int testImportanceId, int measureId, int standardId, int testDescriptionId);
        bool UpdateControlPlan(int id, int companyId, string planNumber, string createDate);
        void DeleteControlPlan(int id);
        void DeleteControlPlanPiece(int id);
        void DeleteControlPlanProcess(int id);
        void DeleteControlPlanProcessTest(int id);

    }
}
