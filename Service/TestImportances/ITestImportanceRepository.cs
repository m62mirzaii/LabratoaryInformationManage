using Models.Model;

namespace Core.Services.TestImportances
{
    public interface ITestImportanceRepository
    {
        Task<TestImportance> GetTestImportanceById(int id);
        Task<List<TestImportance>> GetTestImportances_Async();
        List<TestImportance> GetTestImportances();
        List<TestImportance> GetTestImportancForSelect2(string searchTerm);
        void AddTestImportance(TestImportance testImportance);
        bool UpdateTestImportance(TestImportance testImportance);
        void DeleteTestImportance(int id);
    }
}
