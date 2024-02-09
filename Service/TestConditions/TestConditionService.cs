using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.TestConditions
{
    public class TestConditionService : ITestConditionRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<TestCondition> _testCondition;

        public TestConditionService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _testCondition = dbContext.Set<TestCondition>();
        }

        public async Task<TestCondition> GetTestConditionById(int id)
        {
            return await _testCondition.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public  List<TestCondition>  GetTestConditionForSelect2(string searchTerm)
        {
            var query = _testCondition.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public List<TestCondition> GetTestConditions()
        {
            return _testCondition
            .OrderByDescending(e => e.Id)
            .ToList();
        }

        public void AddTestCondition(TestCondition testCondition)
        {
            var _item = new TestCondition
            {
                Name = testCondition.Name
            };

            _testCondition.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateTestCondition(  TestCondition testCondition)
        {
            var _item = _testCondition.Find(testCondition.Id);
            if (_item != null)
            {
                _item.Name = testCondition.Name;
                _item.IsActive = testCondition.IsActive;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTestCondition(int id)
        {
            var _item = _testCondition.Find(id);
            if (_item != null)
            {
                _testCondition.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
