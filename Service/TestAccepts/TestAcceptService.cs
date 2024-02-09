using Share.Extentions;
using Share.Generators;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using System.Linq;
using Share.Enums;

namespace Core.Services.TestAccepts
{
    public class TestAcceptService : ITestAcceptRepository
    {
        public DataBaseContext _dbContext;
        public readonly DbSet<TestAccept> _TestAccepts;
        public readonly DbSet<TestAcceptDetail> _TestAcceptDetails;
        private readonly DbSet<ControlPlanProcessTest> _controlPlanProcessTest;
        private readonly DbSet<ControlPlanPiece> _controlPlanPieces;
        private readonly DbSet<TestRequest> _testRequest;
        private readonly DbSet<TestRequestDetail> _testRequestDetails;
        public TestAcceptService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _TestAccepts = dbContext.Set<TestAccept>();
            _TestAcceptDetails = dbContext.Set<TestAcceptDetail>();
            _controlPlanProcessTest = dbContext.Set<ControlPlanProcessTest>();
            _controlPlanPieces = dbContext.Set<ControlPlanPiece>();
            _testRequest = dbContext.Set<TestRequest>();
            _testRequestDetails = dbContext.Set<TestRequestDetail>();
        }

        public TestAcceptViewModel GetTestAcceptById(int id)
        {
            var testRequets = _TestAccepts.Where(e => e.Id == id).AsQueryable();

            var result = testRequets.Select(e => new
            {
                e.Id,
                e.CreateDate,
                e.ControlPlanId,
                e.TestRequestId,
                e.TestRequest.RequestNumber,
                e.ControlPlan.PlanNumber,
            }).AsEnumerable()
            .Select(e => new TestAcceptViewModel
            {
                Id = e.Id,
                ControlPlanId = e.ControlPlanId,
                RequestNumber = e.RequestNumber,
                TestRequestId = e.TestRequestId,
                CreateDate = DateTimeGenerator.GetShamsiDate(e.CreateDate),
                PlanNumber = e.PlanNumber,
            }).FirstOrDefault();

            return result;
        }

        public List<TestAcceptViewModel> GetTestAccepts()
        {
            var testRequets = _TestAccepts.AsQueryable().Include(e => e.TestRequest);

            var result = testRequets.Select(e => new
            {
                e.Id,
                e.CreateDate,
                e.ControlPlanId,
                e.TestRequestId,
                e.TestRequest.RequestNumber,
                e.ControlPlan.PlanNumber,
                e.ConfirmCode,
                e.ReceptionNumber
            }).AsEnumerable()
            .OrderByDescending(w => w.Id)
            .Select(e => new TestAcceptViewModel
            {
                Id = e.Id,
                ControlPlanId = e.ControlPlanId,
                TestRequestId = e.TestRequestId,
                RequestNumber = e.RequestNumber,
                ConfirmCode = e.ConfirmCode,
                ConfirmCodeName = ((Share.Enums.ConfirmCode)e.ConfirmCode).GetEnumDisplayName(),
                CreateDate = DateTimeGenerator.GetShamsiDate(e.CreateDate),
                PlanNumber = e.PlanNumber,
                ReceptionNumber = e.ReceptionNumber
            })
            .ToList();

            return result;
        }

        public void AddTestAccept(int controlPlanId, string createDate, int testRequestId, string receptionNumber)
        {
            var item = new TestAccept
            {
                CreateDate = DateTimeGenerator.ConvertShamsiToMilady(createDate),
                ControlPlanId = controlPlanId,
                TestRequestId = testRequestId,
                ConfirmCode = (int)Share.Enums.ConfirmCode.FisrtLevel,
                ReceptionNumber = receptionNumber,
            };

            _TestAccepts.Add(item);
            _dbContext.SaveChanges();
        }

        public bool UpdateTestAccept(int id, int controlPlanId, string createDate, int testRequestId, string receptionNumber)
        {
            var _item = _TestAccepts.Find(id);
            if (_item != null)
            {
                _item.CreateDate = DateTimeGenerator.ConvertShamsiToMilady(createDate);
                _item.ControlPlanId = controlPlanId;
                _item.TestRequestId = testRequestId;
                _item.ReceptionNumber = receptionNumber;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTestAccept(int id)
        {
            var _item = _TestAccepts.Where(e => e.Id == id).FirstOrDefault();
            if (_item != null)
            {
                _TestAccepts.Remove(_item);
                _dbContext.SaveChanges();
            }
        }

        public List<TestAcceptViewModel> GetTestAcceptForKartabl(int controlPlanId)
        {
            var testRequets = _TestAccepts.AsQueryable().Where(e => e.ConfirmCode == (int)ConfirmCode.Confirm || e.ConfirmCode == (int)ConfirmCode.SendToKartabl);

            var result = testRequets.Select(e => new
            {
                e.Id,
                e.CreateDate,
                e.ControlPlanId,
                e.TestRequestId,
                e.TestRequest.RequestNumber,
                e.ControlPlan.PlanNumber,
                e.ConfirmCode,
                e.ReceptionNumber
            }).AsEnumerable()
            .OrderByDescending(e => e.Id)
            .Select(e => new TestAcceptViewModel
            {
                Id = e.Id,
                ControlPlanId = e.ControlPlanId,
                TestRequestId = e.TestRequestId,
                RequestNumber = e.RequestNumber,
                ConfirmCode = e.ConfirmCode,
                ConfirmCodeName = ((Share.Enums.ConfirmCode)e.ConfirmCode).GetEnumDisplayName(),
                CreateDate = DateTimeGenerator.GetShamsiDate(e.CreateDate),
                PlanNumber = e.PlanNumber,
                ReceptionNumber = e.ReceptionNumber
            }).ToList();

            return result;
        }

        public void SendToKartabl(int id)
        {
            var _item = _TestAccepts.Find(id);
            if (_item != null)
            {
                _item.ConfirmCode = (int)Share.Enums.ConfirmCode.SendToKartabl;
                _dbContext.SaveChanges();
            }
        }

        public List<TestAcceptDetailViewModel> GetTestAcceptDetails(int TestAcceptId)
        {
            var TestAcceptDetails = _TestAcceptDetails.Where(e => e.TestAcceptId == TestAcceptId).AsQueryable();

            var result = TestAcceptDetails?.Select(e => new
            {
                e.Id,
                e.Avarage,
                e.FromDate,
                e.EndDate,
                e.TestAccept.ConfirmCode,
                e.Humidity,
                e.Temperature,
                e.IsConfirm,
                e.ControlPlanProcessTestId,
                e.ControlPlanPiece.Piece.PieceName,
                ProcessName = e.ControlPlanProcessTest.ControlPlanProcess.Process.ProcessName ?? "",
                ProcessTypeName = e.ControlPlanProcessTest.ControlPlanProcess.Process.ProcessType.Name ?? "",
                e.ControlPlanProcessTest.Test.TestName,
                TestConditionName = e.ControlPlanProcessTest.Test.TestCondition.Name,
                e.ControlPlanProcessTest.Test.Amount,
                e.ControlPlanProcessTest.Test.Minimum,
                e.ControlPlanProcessTest.Test.Maximum,
                StandardName = e.ControlPlanProcessTest.Test.Standard.Name ?? "",
                MeasureName = e.ControlPlanProcessTest.Test.Measure.Name ?? "",
                TestImportanceName = e.ControlPlanProcessTest.Test.TestImportance.Name ?? "",
                TestDescriptionName = e.ControlPlanProcessTest.Test.TestDescription.Name ?? "",
                LabratoaryToolName = e.ControlPlanProcessTest.Test.LabratoaryTool.ToolName,

            }).AsEnumerable()
            .Select(e => new TestAcceptDetailViewModel
            {
                Id = e.Id,
                Avarage = e.Avarage,
                LabratoaryToolName = e.LabratoaryToolName,
                FromDate = DateTimeGenerator.GetShamsiDate(e.FromDate),
                EndDate = DateTimeGenerator.GetShamsiDate(e.EndDate),
                UserName = "",
                Humidity = e.Humidity,
                Temperature = e.Temperature,
                ControlPlanProcessTestId = e.ControlPlanProcessTestId,
                PieceName = e.PieceName,
                ProcessName = e.ProcessName,
                ProcessTypeName = e.ProcessTypeName,
                TestName = e.TestName,
                TestConditionName = e.TestConditionName,
                Amount = e.Amount,
                Minimum = e.Minimum,
                Maximum = e.Maximum,
                StandardName = e.StandardName,
                MeasureName = e.MeasureName,
                TestImportanceName = e.TestImportanceName,
                TestDescriptionName = e.TestDescriptionName,
                ConfirmCode_TestAccept = e.ConfirmCode,
                IsConfirm = e.IsConfirm
            }).ToList();

            return result;
        }

        public List<TestAcceptDetailViewModel> GetTestAcceptDetailByControlPlanId(int controlPlanId)
        {
            var TestAcceptDetails = _TestAcceptDetails.Where(e => e.ControlPlanProcessTest.ControlPlanProcess.ControlPlanId == controlPlanId).AsQueryable();

            var result = TestAcceptDetails.Select(e => new
            {
                e.Id,
                e.Avarage,
                e.FromDate,
                e.EndDate,
                e.Humidity,
                e.Temperature,
                e.ControlPlanProcessTestId,
                ProcessName = e.ControlPlanProcessTest.ControlPlanProcess.Process.ProcessName ?? "",
                ProcessTypeName = e.ControlPlanProcessTest.ControlPlanProcess.Process.ProcessType.Name ?? "",
                e.ControlPlanProcessTest.Test.TestName,
                TestConditionName = e.ControlPlanProcessTest.Test.TestCondition.Name,
                e.ControlPlanProcessTest.Test.Amount,
                e.ControlPlanProcessTest.Test.Minimum,
                e.ControlPlanProcessTest.Test.Maximum,
                StandardName = e.ControlPlanProcessTest.Test.Standard.Name ?? "",
                MeasureName = e.ControlPlanProcessTest.Test.Measure.Name ?? "",
                TestImportanceName = e.ControlPlanProcessTest.Test.TestImportance.Name ?? "",
                TestDescriptionName = e.ControlPlanProcessTest.Test.TestDescription.Name ?? "",
                LabratoaryToolName = e.ControlPlanProcessTest.Test.LabratoaryTool.ToolName,
            }).AsEnumerable()
            .Select(e => new TestAcceptDetailViewModel
            {
                Id = e.Id,
                Avarage = e.Avarage,
                LabratoaryToolName = e.LabratoaryToolName,
                FromDate = DateTimeGenerator.GetShamsiDate(e.FromDate),
                EndDate = DateTimeGenerator.GetShamsiDate(e.EndDate),
                UserName = "",
                Humidity = e.Humidity,
                Temperature = e.Temperature,
                ControlPlanProcessTestId = e.ControlPlanProcessTestId,
                ProcessName = e.ProcessName,
                ProcessTypeName = e.ProcessTypeName,
                TestName = e.TestName,
                TestConditionName = e.TestConditionName,
                Amount = e.Amount,
                Minimum = e.Minimum,
                Maximum = e.Maximum,
                StandardName = e.StandardName,
                MeasureName = e.MeasureName,
                TestImportanceName = e.TestImportanceName,
                TestDescriptionName = e.TestDescriptionName
            }).ToList();

            return result;
        }

        public void AddTestAcceptDetail(List<TestAcceptDetailViewModel> TestAcceptDetails)
        {
            foreach (var item in TestAcceptDetails)
            {
                var deleteItem = _TestAcceptDetails
                    .Where(x => x.TestAcceptId == item.TestAcceptId && x.ControlPlanProcessTestId == item.ControlPlanProcessTestId && x.ControlPlanPieceId == item.ControlPlanPieceId)
                    .FirstOrDefault();
                if (deleteItem != null)
                {
                    _TestAcceptDetails.Remove(deleteItem);
                    _dbContext.SaveChanges();
                }

                var TestAcceptDetail = new TestAcceptDetail
                {
                    TestAcceptId = item.TestAcceptId,
                    ControlPlanProcessTestId = item.ControlPlanProcessTestId,
                    ControlPlanPieceId = item.ControlPlanPieceId,
                    Avarage = item.Avarage,
                    FromDate = DateTimeGenerator.ConvertShamsiToMilady(item.FromDate),
                    EndDate = DateTimeGenerator.ConvertShamsiToMilady(item.EndDate),
                    Humidity = item.Humidity,
                    Temperature = item.Temperature,
                    IsConfirm = false,
                };

                _TestAcceptDetails.Add(TestAcceptDetail);
                _dbContext.SaveChanges();
            }
        }

        public bool UpdateTestAcceptDetail(int id, decimal avarage, string toolCode, string fromDate, string endDate, int humidity, int temperature)
        {
            var _item = _TestAcceptDetails.Find(id);
            if (_item != null)
            {
                _item.Avarage = avarage;
                _item.FromDate = DateTimeGenerator.ConvertShamsiToMilady(fromDate);
                _item.EndDate = DateTimeGenerator.ConvertShamsiToMilady(endDate);
                _item.Humidity = humidity;
                _item.Temperature = temperature;
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteTestAcceptDetail(int id)
        {
            var _item = _TestAcceptDetails.Where(e => e.Id == id).FirstOrDefault();
            if (_item != null)
            {
                _TestAcceptDetails.Remove(_item);
                _dbContext.SaveChanges();
            }
        }

        public void Confirm_TestAccept(int id, List<TestAcceptDetail> TestAcceptDetails)
        {
            var _item = _TestAccepts.Find(id);
            if (_item != null)
            {
                _item.ConfirmCode = (int)Share.Enums.ConfirmCode.Confirm;

                foreach (var item in TestAcceptDetails)
                {
                    var _detail = _TestAcceptDetails.FirstOrDefault(e => e.Id == item.Id);
                    if (_detail != null)
                    {
                        _detail.IsConfirm = item.IsConfirm;
                        _detail.AnswerText = item.AnswerText;
                        _detail.TestResult = item.TestResult;
                    }
                }
                _dbContext.SaveChanges();
            }
        }

        public void Update_TestAccept_ConfirmCode(int id)
        {
            var _item = _TestAccepts.Find(id);
            if (_item != null)
            {
                _item.ConfirmCode = (int)Share.Enums.ConfirmCode.SendToKartabl;
                _dbContext.SaveChanges();
            }
        }

        public void Return_TestAccept(int id)
        {
            var _item = _TestAccepts.Find(id);
            if (_item != null)
            {
                _item.ConfirmCode = (int)Share.Enums.ConfirmCode.ReturnToUser;
                _dbContext.SaveChanges();
            }
        }

        public List<TestAcceptDetailViewModel> GetTestAcceptDetailsForInsertPopup(int controlPlanId, int testRequestId)
        {
            if (controlPlanId > 0)
            {
                var testRequestDetails = _testRequestDetails.AsQueryable()
                    .Include(e => e.Test).ThenInclude(e => e.TestImportance)
                    .Include(e => e.Test).ThenInclude(e => e.TestCondition)
                    .Include(e => e.Test).ThenInclude(e => e.Measure)
                    .Include(e => e.Test).ThenInclude(e => e.TestDescription)
                    .Include(e => e.Test).ThenInclude(e => e.LabratoaryTool)
                    .Include(e => e.Test).ThenInclude(e => e.Standard)
                    .Where(e => e.TestRequestId == testRequestId).ToList();
                var testIds = testRequestDetails.Select(e => e.TestId).ToList();

                var controlPlanProcessTest = _controlPlanProcessTest.Where(e => e.ControlPlanProcess.ControlPlanId == controlPlanId && testIds.Contains(e.TestId)).AsQueryable();
                var pieces = _controlPlanPieces.Where(e => e.ControlPlanId == controlPlanId).Include(e => e.Piece).ToList();

                var result = new List<TestAcceptDetailViewModel>();

                foreach (var piece in pieces)
                {
                    var processTest = controlPlanProcessTest.Select(e => new
                    {
                        e.Id,
                        e.ControlPlanProcessId,
                        ProcessName = e.ControlPlanProcess.Process.ProcessName ?? "",
                        ProcessTypeName = e.ControlPlanProcess.Process.ProcessType.Name ?? "",
                        e.Test.TestName,
                        TestConditionName = e.Test.TestCondition.Name,
                        e.Test.Amount,
                        e.Test.Minimum,
                        e.Test.Maximum,
                        StandardName = e.Test.Standard.Name,
                        MeasureName = e.Test.Measure.Name,
                        TestImportanceName = e.Test.TestImportance.Name,
                        TestDescriptionName = e.Test.TestDescription.Name,
                        piece.Piece.PieceName,
                        ControlPlanPieceId = piece.Id,
                        LabratoaryToolId = e.Test.LabratoaryTool.Id,
                        LabratoaryToolName = e.Test.LabratoaryTool.ToolName
                    }).AsEnumerable()
                   .Select((e, index) => new TestAcceptDetailViewModel
                   {
                       ControlPlanProcessTestId = e.Id,
                       ProcessName = e.ProcessName,
                       ProcessTypeName = e.ProcessTypeName,
                       ControlPlanPieceId = e.ControlPlanPieceId,
                       PieceName = e.PieceName,
                       TestName = e.TestName,
                       TestConditionName = e.TestConditionName,
                       Amount = e.Amount,
                       Minimum = e.Minimum,
                       Maximum = e.Maximum,
                       StandardName = e.StandardName,
                       MeasureName = e.MeasureName,
                       TestImportanceName = e.TestImportanceName,
                       TestDescriptionName = e.TestDescriptionName,
                       LabratoaryToolId = e.LabratoaryToolId,
                       LabratoaryToolName = e.LabratoaryToolName,
                       IsConflict = false,
                   })
                   .ToList();

                    result.AddRange(processTest);
                };

                testRequestDetails.RemoveAll(t => controlPlanProcessTest.Select(s => s.TestId).ToList().Contains(t.TestId));

                var testRequest = testRequestDetails
                      .Select(e => new
                      {
                          e.Id,
                          e.Test.TestName,
                          TestConditionName = e.Test.TestCondition.Name,
                          e.Test.Amount,
                          e.Test.Minimum,
                          e.Test.Maximum,
                          StandardName = e.Test.Standard.Name,
                          MeasureName = e.Test.Measure.Name,
                          TestImportanceName = e.Test.TestImportance.Name,
                          TestDescriptionName = e.Test.TestDescription.Name,
                          PieceName = "",
                          ControlPlanPieceId = 0,
                          LabratoaryToolId = e.Test.LabratoaryTool.Id,
                          LabratoaryToolName = e.Test.LabratoaryTool.ToolName
                      }).AsEnumerable()
                .Select((e, index) => new TestAcceptDetailViewModel
                {
                    ControlPlanProcessTestId = 0,
                    ProcessName = "",
                    ProcessTypeName = "",
                    ControlPlanPieceId = e.ControlPlanPieceId,
                    PieceName = e.PieceName,
                    TestName = e.TestName,
                    TestConditionName = e.TestConditionName,
                    Amount = e.Amount,
                    Minimum = e.Minimum,
                    Maximum = e.Maximum,
                    StandardName = e.StandardName,
                    MeasureName = e.MeasureName,
                    TestImportanceName = e.TestImportanceName,
                    TestDescriptionName = e.TestDescriptionName,
                    LabratoaryToolId = e.LabratoaryToolId,
                    LabratoaryToolName = e.LabratoaryToolName,
                    IsConflict = true
                }).ToList();
                result.AddRange(testRequest);

                return result;
            }

            return new List<TestAcceptDetailViewModel>();
        }

        public bool CheckReceptionNumber(string receptionNumber)
        {
            var count = _TestAccepts.AsQueryable().Count(e => e.ReceptionNumber == receptionNumber);
            if (count > 0)
                return false;

            return true;
        }
    }
}
