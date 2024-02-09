using Share.Generators;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;

namespace Core.Services.TestRequests
{
    public class TestRequestService : ITestRequestRepository
    {
        public DataBaseContext _dbContext;
        public readonly DbSet<TestRequest> _tesRequests;
        public readonly DbSet<TestRequestDetail> _testRequestDetails;
        private readonly DbSet<ControlPlanProcessTest> _controlPlanProcessTest;
        private readonly DbSet<ControlPlanPiece> _controlPlanPieces;
        public TestRequestService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _tesRequests = dbContext.Set<TestRequest>();
            _testRequestDetails = dbContext.Set<TestRequestDetail>();
            _controlPlanProcessTest = dbContext.Set<ControlPlanProcessTest>();
            _controlPlanPieces = dbContext.Set<ControlPlanPiece>(); ;
        }

        public TestRequestViewModel GetTesRequestById(int id)
        {
            var testRequets = _tesRequests.Where(e => e.Id == id).AsQueryable();

            var result = testRequets.Select(e => new
            {
                e.Id,
                e.RequestNumber,
                e.RequestDate,

                RequestUnitName = e.RequestUnit.Name,
                UserName = e.RequestUser.Name  ,
                e.Piece.PieceName,
                CompanyName = e.Company.Name,
            }).AsEnumerable()
            .Select(e => new TestRequestViewModel
            {
                Id = e.Id,
                RequestNumber = e.RequestNumber,
                RequestDate = DateTimeGenerator.GetShamsiDate(e.RequestDate),
                RequestUnitName = e.RequestUnitName,
                CompanyName = e.CompanyName,
                RequestUserName = e.UserName,
                PieceName = e.PieceName,
            }).FirstOrDefault();

            return result;
        }

        public List<TestRequestViewModel> GetTesRequests()
        {
            var testRequets = _tesRequests.AsQueryable();

            var result = testRequets.Select(e => new
            {
                e.Id,
                e.RequestNumber,
                e.RequestDate,
                e.CompanyId,
                e.RequestUserId,
                e.PieceId,
                e.RequestUnitId,
                RequestUnitName = e.RequestUnit.Name,
                RequestUserName = e.RequestUser.Name ,
                e.Piece.PieceName,
                CompanyName = e.Company.Name,
            }).AsEnumerable()
            .OrderByDescending(e => e.Id)
            .Select(e => new TestRequestViewModel
            {
                Id = e.Id,
                RequestNumber = e.RequestNumber,
                RequestDate = DateTimeGenerator.GetShamsiDate(e.RequestDate),
                RequestUnitName = e.RequestUnitName,
                CompanyName = e.CompanyName,
                RequestUserName = e.RequestUserName,
                PieceName = e.PieceName,
                RequestUserId = e.RequestUserId,
                PieceId = e.PieceId,
                CompanyId = e.CompanyId,
                RequestUnitId = e.RequestUnitId
            }).ToList();

            return result;
        }

        public List<TestRequest> GetTestRequestForSelect2(string searchTerm)
        { 
            var query = _tesRequests.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.RequestNumber.ToString().Contains(searchTerm));

            var result = query.ToList();
            return result;

        }

        public void AddTestRequest(int requestNumber, string requestDate, int requestUnitId, int requestUserId, int pieceId, int companyId)
        {
            //int requestNumber = _tesRequests.Max(x => (int?)x.RequestNumber) ?? 0;
            var item = new TestRequest
            {
                RequestDate = DateTimeGenerator.ConvertShamsiToMilady(requestDate),
                RequestNumber = requestNumber,
                RequestUnitId = requestUnitId,
                RequestUserId = requestUserId,
                PieceId = pieceId,
                CompanyId = companyId
            };

            _tesRequests.Add(item);
            _dbContext.SaveChanges();
        }

        public bool UpdateTestRequest(int id, int requestNumber, string requestDate, int requestUnitId, int requestUserId, int pieceId, int companyId)
        {
            var _item = _tesRequests.Find(id);
            if (_item != null)
            {
                _item.RequestNumber = requestNumber;
                _item.RequestUnitId = requestUnitId;
                _item.RequestUserId = requestUserId;
                _item.PieceId = pieceId;
                _item.CompanyId = companyId;
                _item.RequestDate = DateTimeGenerator.ConvertShamsiToMilady(requestDate);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTestRequest(int id)
        {
            var _item = _tesRequests.Where(e => e.Id == id).FirstOrDefault();
            if (_item != null)
            {
                _tesRequests.Remove(_item);
                _dbContext.SaveChanges();
            }
        }

        public List<TestRequestDetailViewModel> GetTesRequestDetails(int testRequestId)
        {
            var testRequestDetails = _testRequestDetails.Where(e => e.TestRequestId == testRequestId).AsQueryable();

            var result = testRequestDetails?.Select(e => new
            {
                e.Id,
                e.TestRequestId,
                TestName = e.Test.TestName,
                TestConditionName = e.Test.TestCondition.Name
            }).AsEnumerable()
            .Select(e => new TestRequestDetailViewModel
            {
                Id = e.Id,
                TestRequestId = e.TestRequestId,
                TestName = e.TestName,
                TestConditionName = e.TestConditionName,
            }).ToList();

            return result;
        }

        public void AddTestRequestDetail(int testRequestId, List<int> testIds)
        {
            foreach (var item in testIds)
            {

                var TestRequestDetail = new TestRequestDetail
                {
                    TestRequestId = testRequestId,
                    TestId = item,
                };

                _testRequestDetails.Add(TestRequestDetail);
                _dbContext.SaveChanges();
            }
        }

        public bool UpdateTestRequestDetail(int id, string testName)
        {
            var _item = _testRequestDetails.Find(id);
            if (_item != null)
            {
                //_item.TestId = testName;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTestRequestDetail(int id)
        {
            var _item = _testRequestDetails.Where(e => e.Id == id).FirstOrDefault();
            if (_item != null)
            {
                _testRequestDetails.Remove(_item);
                _dbContext.SaveChanges();
            }
        }
    }
}
