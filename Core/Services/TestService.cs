using Core.Generators;
using Core.Repository;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Services
{
    public class TestService : ITestRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Test> _test;
        private readonly DbSet<Process> _process;

        public TestService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _test = dbContext.Set<Test>();
            _process = dbContext.Set<Process>();
        }

        public async Task<TestViewModel> GetTestById(int id)
        {
            var test = await _test.Where(x=>x.Id == id).FirstOrDefaultAsync();

            var result = new TestViewModel
            {
                Id = test.Id,
                TestName = test.TestName,
                ProcessName = _process.Where(e => e.Id == test.ProcessId)?.FirstOrDefault()?.ProcessName,
                Amount = test.Amount,
                FromDate = DateTimeGenerator.GetShamsiDate(test.FromDate),
                EndDate = DateTimeGenerator.GetShamsiDate(test.EndDate),
            };

            return result;
        }

        public async Task<List<TestViewModel>> GetTests()
        {
            var tests = await _test.ToListAsync();
            var lst = tests.Select(x => new TestViewModel
            {
                Id = x.Id,
                TestName = x.TestName,
                ProcessName = _process.Where(e => e.Id == x.ProcessId)?.FirstOrDefault()?.ProcessName,
                Amount = x.Amount,
                FromDate = DateTimeGenerator.GetShamsiDate(x.FromDate),
                EndDate = DateTimeGenerator.GetShamsiDate(x.EndDate),
            }).ToList();

            return lst;
        }

        public void AddTest(TestViewModel test)
        {
            var _item = new Test
            {
                TestName = test.TestName,
                ProcessId = test.ProcessId,
                Amount = test.Amount,
                FromDate = DateTimeGenerator.ConvertShamsiToMilady(test.FromDate),
                EndDate = DateTimeGenerator.ConvertShamsiToMilady(test.EndDate),
            };

            _test.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateTest(int id, TestViewModel test)
        {
            var _item = _test.Find(id);
            if (_item != null)
            {
                _item.TestName = test.TestName;
                _item.ProcessId = test.ProcessId;
                _item.Amount = test.Amount;
                _item.FromDate = DateTimeGenerator.ConvertShamsiToMilady(test.FromDate);
                _item.EndDate = DateTimeGenerator.ConvertShamsiToMilady(test.EndDate);

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTest(int id)
        {
            var _item = _test.Find(id);
            if (_item != null)
            {
                _test.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
