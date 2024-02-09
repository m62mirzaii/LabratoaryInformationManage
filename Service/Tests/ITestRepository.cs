using Models.Model;
using Models.ViewModel;

namespace Core.Services.Tests
{
    public interface ITestRepository
    { 
        List<TestViewModel> GetTests();
        List<Test> GetTestForSelect2(string searchTerm);
        void AddTest(TestViewModel test);
        bool UpdateTest( TestViewModel test);
        void DeleteTest(int id);
    }
}
