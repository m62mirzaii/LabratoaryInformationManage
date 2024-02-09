using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.TestDescriptions
{
    public class TestDescriptionService : ITestDescriptionRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<TestDescription> _testDescription;

        public TestDescriptionService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _testDescription = dbContext.Set<TestDescription>();
        }

        public async Task<TestDescription> GetTestDescriptionById(int id)
        {
            return await _testDescription.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        } 

        public List<TestDescription> GetTestDescriptions()
        {
            return _testDescription.OrderByDescending(e => e.Id).ToList();
        }

        public List<TestDescription> GetTestDescriptionForSelect2(string searchTerm)
        {
            var query = _testDescription.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddTestDescription(TestDescription testDescription)
        {
            var _item = new TestDescription
            {
                IsActive = testDescription.IsActive,
                Name = testDescription.Name
            };

            _testDescription.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateTestDescription(  TestDescription testDescription)
        {
            var _item = _testDescription.Find(testDescription.Id);
            if (_item != null)
            {
                _item.Name = testDescription.Name;
                _item.IsActive = testDescription.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTestDescription(int id)
        {
            var _item = _testDescription.Find(id);
            if (_item != null)
            {
                _testDescription.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
