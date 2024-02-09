using Models.Model;

namespace Core.Services.TestConditions
{
    public interface ITestConditionRepository
    {
        Task<TestCondition> GetTestConditionById(int id);
        List<TestCondition> GetTestConditionForSelect2(string searchTerm);
        List<TestCondition> GetTestConditions();
        void AddTestCondition(TestCondition testCondition);
        bool UpdateTestCondition(  TestCondition testCondition);
        void DeleteTestCondition(int id);
    }
}
