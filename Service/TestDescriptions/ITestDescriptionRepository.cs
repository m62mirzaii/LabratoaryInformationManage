using Models.Model;

namespace Core.Services.TestDescriptions
{
    public interface ITestDescriptionRepository
    {
        Task<TestDescription> GetTestDescriptionById(int id); 
        List<TestDescription> GetTestDescriptions();
        List<TestDescription> GetTestDescriptionForSelect2(string searchTerm);
        void AddTestDescription(TestDescription testDescription);
        bool UpdateTestDescription( TestDescription testDescription);
        void DeleteTestDescription(int id);
    }
}
