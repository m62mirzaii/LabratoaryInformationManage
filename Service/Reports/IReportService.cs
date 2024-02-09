using Models.ViewModel;

namespace Service.Reports
{
    public interface IReportService
    {
        List<ConfirmCodeForReportViewModel> GetAssessmentintResult(); 
        List<TestAcceptViewModel> GetReceptionNumber();
    }
}
