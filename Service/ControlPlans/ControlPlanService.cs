using Share.Generators;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Services.ControlPlans
{
    public class ControlPlanService : IControlPlanRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<ControlPlan> _controlPlan;
        private readonly DbSet<ControlPlanPiece> _controlPlanPiece;
        private readonly DbSet<ControlPlanProcess> _controlPlanProcess;
        private readonly DbSet<ControlPlanProcessTest> _controlPlanProcessTest;
        private readonly DbSet<Company> _company;
        private readonly DbSet<Piece> _piece;
        private readonly DbSet<PieceUsage> _pieceUsage;
        private readonly DbSet<Process> _process;
        private readonly DbSet<ProcessType> _processType;
        private readonly DbSet<Test> _test;
        private readonly DbSet<Measure> _measures;
        private readonly DbSet<Standard> _standards;
        private readonly DbSet<TestCondition> _testConditions;
        private readonly DbSet<TestImportance> _testImportance;
        private readonly DbSet<TestDescription> _testDescription;
        public ControlPlanService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _controlPlan = dbContext.Set<ControlPlan>();
            _controlPlanPiece = dbContext.Set<ControlPlanPiece>();
            _controlPlanProcess = dbContext.Set<ControlPlanProcess>();
            _controlPlanProcessTest = dbContext.Set<ControlPlanProcessTest>();
            _company = dbContext.Set<Company>();
            _piece = dbContext.Set<Piece>();
            _pieceUsage = dbContext.Set<PieceUsage>();
            _process = dbContext.Set<Process>();
            _processType = dbContext.Set<ProcessType>();
            _test = dbContext.Set<Test>();
            _measures = dbContext.Set<Measure>();
            _standards = dbContext.Set<Standard>();
            _testConditions = dbContext.Set<TestCondition>();
            _testImportance = dbContext.Set<TestImportance>();
            _testDescription = dbContext.Set<TestDescription>();
        }

        public ControlPlanViewModel GetControlPlanByIdAsync(int id)
        {
            var controlPlans = _controlPlan.Where(e => e.Id == id).AsQueryable();

            var result = controlPlans.Select(e => new
            {
                e.Id,
                e.PlanNumber,
                e.CompanyId,
                CompanyName = e.Company.Name ?? "",
                CreateDate = DateTimeGenerator.GetShamsiDate(e.CreateDate),
            }).AsEnumerable()
            .Select(e => new ControlPlanViewModel
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                CompanyName = e.CompanyName,
                CreateDate = e.CreateDate,
                PlanNumber = e.PlanNumber
            })
            .FirstOrDefault();

            return result;
        }

        public List<ControlPlanViewModel> GetControlPlans()
        {
            var controlPlans = _controlPlan.AsQueryable();

            var result = controlPlans.Select(e => new
            {
                e.Id,
                e.PlanNumber,
                e.CompanyId,
                CompanyName = e.Company.Name ?? "",
                CreateDate = DateTimeGenerator.GetShamsiDate(e.CreateDate),
            }).AsEnumerable()
            //.Skip(pageNum)
            //.Take(length)
            .Select(e => new ControlPlanViewModel
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                CompanyName = e.CompanyName,
                CreateDate = e.CreateDate,
                PlanNumber = e.PlanNumber
            })
            .ToList();

            return result;
        }

        public List<ControlPlan> GetControlPlanForSelect2(string searchTerm)
        {
            var query = _controlPlan.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.PlanNumber.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public List<ControlPlanPieceViewModel> GetControlPlanPiece(int controlPlanId)
        {
            if (controlPlanId > 0)
            {
                var controlPlanPiece = _controlPlanPiece.Where(e => e.ControlPlanId == controlPlanId).AsQueryable();

                var result = controlPlanPiece.Select(e => new
                {
                    e.Id,
                    e.PieceId,
                    e.ControlPlanId,
                    Code = e.Piece.Code ?? "",
                    PieceName = e.Piece.PieceName ?? "",
                    UsageName = e.Piece.PieceUsage.UsageName ?? "",
                }).AsEnumerable()
                .Select(e => new ControlPlanPieceViewModel
                {
                    Id = e.Id,
                    PieceId = e.PieceId,
                    Code = e.Code,
                    PieceName = e.PieceName,
                    UsageName = e.UsageName
                })
                .ToList();

                return result;
            }

            return new List<ControlPlanPieceViewModel>();
        }
        public List<ControlPlanProcessViewModel> GetControlPlanProcess(int controlPlanId)
        {
            if (controlPlanId > 0)
            {
                var controlPlanProcess = _controlPlanProcess.Where(e => e.ControlPlanId == controlPlanId).AsQueryable();

                var result = controlPlanProcess.Select(e => new
                {
                    e.Id,
                    e.Process.ProcessName,
                    ProcessTypeName = e.Process.ProcessType.Name,
                }).AsEnumerable()
                .Select(e => new ControlPlanProcessViewModel
                {
                    Id = e.Id,
                    ProcessName = e.ProcessName,
                    ProcessTypeName = e.ProcessTypeName,
                })
                .ToList();

                return result;
            }

            return new List<ControlPlanProcessViewModel>();
        }

        public List<ControlPlanProcessTestViewModel> GetControlPlanProcessTest(int controlPlanProcessId)
        {

            if (controlPlanProcessId > 0)
            {
                var controlPlanProcessTest = _controlPlanProcessTest.Where(e => e.ControlPlanProcessId == controlPlanProcessId).AsQueryable();

                var result = controlPlanProcessTest.Select(e => new
                {
                    e.Id,
                    e.ControlPlanProcessId,
                    e.Test.TestName,
                    TestConditionName = e.Test.TestCondition.Name,
                    e.Test.Amount,
                    Minimum=  e.Test.Minimum,
                    Maximum= e.Test.Maximum,
                    StandardName = e.Test.Standard.Name,
                    MeasureName = e.Test.Measure.Name, 
                    TestDescriptionName = e.Test.TestDescription.Name,
                }).AsEnumerable()
                .Select(e => new ControlPlanProcessTestViewModel
                {
                    Id = e.Id,
                    ControlPlanProcessId = e.ControlPlanProcessId,
                    TestName = e.TestName,
                    TestConditionName = e.TestConditionName,
                    Amount = e.Amount,
                    Minimum = e.Minimum,
                    Maximum = e.Maximum,
                    StandardName = e.StandardName,
                    MeasureName = e.MeasureName, 
                    TestDescriptionName = e.TestDescriptionName
                })
                .ToList();

                return result;
            }

            return new List<ControlPlanProcessTestViewModel>();
        }

        public List<ControlPlanProcessTestViewModel> GetControlPlanProcessTestByControlPlanId(int controlPlanId)
        {

            if (controlPlanId > 0)
            {
                var controlPlanProcessTest = _controlPlanProcessTest.Where(e => e.ControlPlanProcess.ControlPlanId == controlPlanId).AsQueryable();

                var result = controlPlanProcessTest.Select(e => new
                {
                    e.Id,
                    e.ControlPlanProcessId,
                    ProcessName = e.ControlPlanProcess.Process.ProcessName ?? "",
                    ProcessTypeName = e.ControlPlanProcess.Process.ProcessType.Name ?? "",
                    e.Test.TestName,
                    TestConditionName = e.Test.TestCondition.Name,
                    e.Test.Amount,
                    Minimum = e.Test.Minimum,
                    Maximum = e.Test.Maximum,
                    StandardName = e.Test.Standard.Name,
                    MeasureName = e.Test.Measure.Name,
                    TestDescriptionName = e.Test.TestDescription.Name,
                }).AsEnumerable()
                .Select(e => new ControlPlanProcessTestViewModel
                {
                    Id = e.Id,
                    ControlPlanProcessId = e.ControlPlanProcessId,
                    ProcessName = e.ProcessName,
                    ProcessTypeName = e.ProcessTypeName,
                    TestName = e.TestName,
                    TestConditionName = e.TestConditionName,
                    Amount = e.Amount,
                    Minimum = e.Minimum,
                    Maximum = e.Maximum,
                    StandardName = e.StandardName,
                    MeasureName = e.MeasureName, 
                    TestDescriptionName = e.TestDescriptionName
                })
                .ToList();

                return result;
            }

            return new List<ControlPlanProcessTestViewModel>();
        }

        public void AddControlPlan(int companyId, string planNumber, string createDate)
        {
            var _item = new ControlPlan
            {
                CompanyId = companyId,
                CreateDate = DateTimeGenerator.ConvertShamsiToMilady(createDate),
                PlanNumber = planNumber,
            };

            _controlPlan.Add(_item);
            _dbContext.SaveChanges();
        }

        public void AddControlPlanPiece(int ControlPlanId, List<int> lstPieceIds)
        {
            foreach (var piece in lstPieceIds)
            {
                var _item = new ControlPlanPiece
                {
                    ControlPlanId = ControlPlanId,
                    PieceId = piece
                };

                _controlPlanPiece.Add(_item);
                _dbContext.SaveChanges();
            }

        }

        public void AddControlPlanProcess(int ControlPlanId, List<int> lstProcess)
        {
            foreach (var process in lstProcess)
            {
                var _item = new ControlPlanProcess
                {
                    ControlPlanId = ControlPlanId,
                    ProcessId = process
                };

                _controlPlanProcess.Add(_item);
                _dbContext.SaveChanges();
            }
        }

        public void AddControlPlanProcessTest(int ControlPlanProcessId, List<int> testIds, decimal min, decimal max, int testImportanceId, int measureId, int standardId, int testDescriptionId)
        {
            foreach (var test in testIds)
            {
                var _item = new ControlPlanProcessTest
                {
                    ControlPlanProcessId = ControlPlanProcessId,
                    TestId = test, 
                };

                _controlPlanProcessTest.Add(_item);
                _dbContext.SaveChanges();
            }
        }


        public bool UpdateControlPlan(int id, int companyId, string planNumber, string createDate)
        {
            var _item = _controlPlan.Find(id);
            if (_item != null)
            {
                _item.CompanyId = companyId;
                _item.PlanNumber = planNumber;
                _item.CreateDate = DateTimeGenerator.ConvertShamsiToMilady(createDate);

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteControlPlan(int id)
        {
            var _item = _controlPlan.Find(id);
            if (_item != null)
            {
                _controlPlan.Remove(_item);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteControlPlanPiece(int id)
        {
            var _item = _controlPlanPiece.Find(id);
            if (_item != null)
            {
                _controlPlanPiece.Remove(_item);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteControlPlanProcess(int id)
        {
            var _item = _controlPlanProcess.Find(id);
            if (_item != null)
            {
                _controlPlanProcess.Remove(_item);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteControlPlanProcessTest(int id)
        {
            var _item = _controlPlanProcessTest.Find(id);
            if (_item != null)
            {
                _controlPlanProcessTest.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
