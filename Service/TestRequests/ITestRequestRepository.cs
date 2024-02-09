using Models.Model;
using Models.ViewModel;

namespace Core.Services.TestRequests
{
    public interface ITestRequestRepository
    {
        TestRequestViewModel GetTesRequestById(int id);
        List<TestRequestViewModel> GetTesRequests();
        List<TestRequest> GetTestRequestForSelect2(string searchTerm);
        void AddTestRequest(int requestNumber, string requestDate, int requestUnitId, int requestUserId, int pieceId, int companyId);
        bool UpdateTestRequest(int id, int requestNumber, string requestDate, int requestUnitId, int requestUserId, int pieceId, int companyId);
        void DeleteTestRequest(int id);
        List<TestRequestDetailViewModel> GetTesRequestDetails(int testRequestId);
        void AddTestRequestDetail(int testRequestId, List<int> testIds);
        bool UpdateTestRequestDetail(int id, string testName);
        void DeleteTestRequestDetail(int id);
    }
}
