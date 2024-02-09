using Models.Model;
using Models.ViewModel;

namespace Core.Services.TestLabratoryTools
{
    public interface ITestLabratoaryToolRepository
    {
        TestLabratoaryToolViewModel GetById(int id);
        List<TestLabratoaryToolViewModel> Get();
        void Add(TestLabratoaryToolViewModel testLabratoaryTool);
        bool Update(  TestLabratoaryToolViewModel testLabratoaryTool);
        void Delete(int id);
    }
}
