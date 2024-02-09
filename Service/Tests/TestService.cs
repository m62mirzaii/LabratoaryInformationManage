using Share.Generators;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Services.Tests
{
    public class TestService : ITestRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Test> _test;
        private readonly DbSet<TestCondition> _testCondition;

        public TestService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _test = dbContext.Set<Test>();
            _testCondition = dbContext.Set<TestCondition>();
        } 

        public List<TestViewModel> GetTests()
        {
            var tests = _test.AsQueryable();
            var result = tests.Select(x => new
            {
                x.Id,
                x.TestName,
                x.TestConditionId,
                x.TestImportanceId,
                x.LabratoaryToolId,
                TestConditionName = x.TestCondition.Name,
                TestImportanceName = x.TestImportance.Name,
                LabratoaryToolName = x.LabratoaryTool.ToolName,
                x.Amount,
                StandardId = x.StandardId,
                StandardName = x.Standard.Name,
                MeasureId = x.MeasureId,
                MeasureName = x.Measure.Name,
                Minimum = x.Minimum,
                Maximum = x.Maximum,
                TestDescriptionId = x.TestDescriptionId,
                TestDescriptionName = x.TestDescription.Name,
                FromDate = DateTimeGenerator.GetShamsiDate(x.FromDate),
                EndDate = DateTimeGenerator.GetShamsiDate(x.EndDate),
            })
            .AsEnumerable()
            .OrderByDescending(e => e.Id)
            .Select(e => new TestViewModel
            {
                Id = e.Id,
                TestName = e.TestName,
                TestConditionId = e.TestConditionId,
                TestConditionName = e.TestConditionName,
                TestImportanceId = e.TestImportanceId,
                LabratoaryToolId = e.LabratoaryToolId,
                TestImportanceName = e.TestImportanceName,
                LabratoaryToolName = e.LabratoaryToolName,
                Amount = e.Amount,
                StandardId = e.StandardId,
                StandardName = e.StandardName,
                MeasureId = e.MeasureId,
                MeasureName = e.MeasureName,
                Minimum = e.Minimum,
                Maximum = e.Maximum,
                TestDescriptionId = e.TestDescriptionId,
                TestDescriptionName = e.TestDescriptionName,
                FromDate = e.FromDate,
                EndDate = e.EndDate,
            }).ToList();

            return result;
        }

        public List<Test> GetTestForSelect2(string searchTerm)
        {
            var query = _test.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.TestName.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddTest(TestViewModel test)
        {
            var _item = new Test
            {
                TestName = test.TestName,
                TestConditionId = test.TestConditionId,
                TestImportanceId = test.TestImportanceId,
                LabratoaryToolId = test.LabratoaryToolId,
                Amount = test.Amount,
                StandardId = test.StandardId, 
                MeasureId = test.MeasureId,
                Minimum = test.Minimum,
                Maximum = test.Maximum,
                TestDescriptionId = test.TestDescriptionId,
                FromDate = DateTimeGenerator.ConvertShamsiToMilady(test.FromDate),
                EndDate = DateTimeGenerator.ConvertShamsiToMilady(test.EndDate),
            };

            _test.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateTest(TestViewModel test)
        {
            var _item = _test.Find(test.Id);
            if (_item != null)
            {
                _item.TestName = test.TestName;
                _item.TestConditionId = test.TestConditionId;
                _item.TestImportanceId = test.TestImportanceId;
                _item.LabratoaryToolId = test.LabratoaryToolId;
                _item.Amount = test.Amount;
                _item.StandardId = test.StandardId;
                _item.MeasureId = test.MeasureId;
                _item.StandardId = test.StandardId;
                _item.Minimum = test.Minimum;
                _item.Maximum = test.Maximum;
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
