using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.TestImportances
{
    public class TestImportanceService : ITestImportanceRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<TestImportance> _testImportance;

        public TestImportanceService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _testImportance = dbContext.Set<TestImportance>();
        }

        public async Task<TestImportance> GetTestImportanceById(int id)
        {
            return await _testImportance.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public async Task<List<TestImportance>> GetTestImportances_Async()
        {
            return await _testImportance.ToListAsync();
        }

        public List<TestImportance> GetTestImportances()
        {
            return _testImportance
            .OrderByDescending(e => e.Id)
            .ToList();
        }

        public List<TestImportance> GetTestImportancForSelect2(string searchTerm)
        {
            var query = _testImportance.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddTestImportance(TestImportance testImportance)
        {
            var _item = new TestImportance
            {
                IsActive = testImportance.IsActive,
                Name = testImportance.Name
            };

            _testImportance.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateTestImportance(TestImportance testImportance)
        {
            var _item = _testImportance.Find(testImportance.Id);
            if (_item != null)
            {
                _item.Name = testImportance.Name;
                _item.IsActive = testImportance.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTestImportance(int id)
        {
            var _item = _testImportance.Find(id);
            if (_item != null)
            {
                _testImportance.Remove(_item);
                _dbContext.SaveChanges();
            }
        }
    }
}
