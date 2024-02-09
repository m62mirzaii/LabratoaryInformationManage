using Share.Generators;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Core.Services.TestLabratoryTools
{
    public class TestLabratoryToolService : ITestLabratoaryToolRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<TestLabratoaryTool> _TestLabratoryTool;

        public TestLabratoryToolService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _TestLabratoryTool = dbContext.Set<TestLabratoaryTool>();
        }

        public TestLabratoaryToolViewModel GetById(int id)
        {
            var query = _TestLabratoryTool.Where(e => e.Id == id).AsQueryable();

            var result = query.Select(e => new
            {
                e.Id,
                e.TestId,
                e.Test.TestName,
                e.LabratoaryToolId,
                LabratoaryToolName = e.LabratoaryTool.ToolName,
            }).AsEnumerable()
        .Select(e => new TestLabratoaryToolViewModel
        {
            Id = e.Id,
            TestName = e.TestName,
            LabratoaryToolId = e.LabratoaryToolId,
            LabratoaryToolName = e.LabratoaryToolName
        }).FirstOrDefault();



            return result;
        }

        public List<TestLabratoaryToolViewModel> Get()
        {
            var query = _TestLabratoryTool.AsQueryable();

            var result = query.Select(e => new
            {
                e.Id,
                e.TestId,
                e.Test.TestName,
                e.LabratoaryToolId,
                LabratoaryToolName = e.LabratoaryTool.ToolName,
            }).AsEnumerable()
            .OrderByDescending(e => e.Id)
            .Select(e => new TestLabratoaryToolViewModel
            {
                Id = e.Id,
                TestId = e.TestId,
                TestName = e.TestName,
                LabratoaryToolId = e.LabratoaryToolId,
                LabratoaryToolName = e.LabratoaryToolName
            })
            .ToList();

            return result; 
        }

        public void Add(TestLabratoaryToolViewModel testLabratoaryTool)
        {
            var _item = new TestLabratoaryTool
            {
                TestId = testLabratoaryTool.TestId,
                LabratoaryToolId = testLabratoaryTool.LabratoaryToolId
            };

            _TestLabratoryTool.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool Update(TestLabratoaryToolViewModel testLabratoaryTool)
        {
            var _item = _TestLabratoryTool.Find(testLabratoaryTool.Id);
            if (_item != null)
            {
                _item.TestId = testLabratoaryTool.TestId;
                _item.LabratoaryToolId = testLabratoaryTool.LabratoaryToolId;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void Delete(int id)
        {
            var _item = _TestLabratoryTool.Find(id);
            if (_item != null)
            {
                _TestLabratoryTool.Remove(_item);
                _dbContext.SaveChanges();
            }
        }
    }
}
