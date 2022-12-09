using Models.Model;
using Models.ViewModel;

namespace Core.Repository
{
    public interface ITestRepository
    {
        Task<TestViewModel> GetTestById(int id);
        Task<List<TestViewModel>> GetTests();
        void AddTest(TestViewModel test);
        bool UpdateTest(int id, TestViewModel test);
        void DeleteTest(int id);
    }
}
