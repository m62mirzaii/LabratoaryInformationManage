using Models.Model;
using Models.ViewModel;

namespace Core.Services.TestAccepts
{
    public interface ITestAcceptRepository
    {
        TestAcceptViewModel GetTestAcceptById(int id);
        List<TestAcceptViewModel> GetTestAccepts();
        List<TestAcceptViewModel> GetTestAcceptForKartabl(int controlPlanId);

        bool CheckReceptionNumber(string receptionNumber);

        void AddTestAccept(int controlPlanId, string createDate, int testRequestId, string receptionNumber);
        bool UpdateTestAccept(int id, int controlPlanId, string createDate, int testRequestId, string receptionNumber);
        void DeleteTestAccept(int id);
        void SendToKartabl(int id);
        void Confirm_TestAccept(int id, List<TestAcceptDetail> TestAcceptDetails);
        void Update_TestAccept_ConfirmCode(int id);
        void Return_TestAccept(int id);
        List<TestAcceptDetailViewModel> GetTestAcceptDetailsForInsertPopup(int controlPlanId, int testRequestId);
        List<TestAcceptDetailViewModel> GetTestAcceptDetails(int TestAcceptId);
        List<TestAcceptDetailViewModel> GetTestAcceptDetailByControlPlanId(int controlPlanId);
        void AddTestAcceptDetail(List<TestAcceptDetailViewModel> TestAcceptDetails);
        bool UpdateTestAcceptDetail(int id, decimal avarage, string toolCode, string fromDate, string endDate, int humidity, int temperature);
        void DeleteTestAcceptDetail(int id);
    }
}
