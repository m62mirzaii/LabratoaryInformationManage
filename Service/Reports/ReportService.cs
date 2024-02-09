using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using Service.Reports;
using Share.Enums;
using Share.Extentions; 

namespace Core.Services.Standards
{
    public class ReportdService : IReportService
    {
        public DataBaseContext _dbContext;
        public readonly DbSet<TestAccept> _TestAccepts;
        public readonly DbSet<TestAcceptDetail> _TestAcceptDetails; 

        public ReportdService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _TestAccepts = dbContext.Set<TestAccept>();
            _TestAcceptDetails = dbContext.Set<TestAcceptDetail>(); 
        }
          
        public  List<ConfirmCodeForReportViewModel> GetAssessmentintResult()
        { 
            var confirmCodeForReports = ((ConfirmCodeForReport[])Enum.GetValues(typeof(ConfirmCodeForReport))).Select(e => e).ToList();

            var lst = new List<ConfirmCodeForReportViewModel>();
            foreach (var item in confirmCodeForReports)
            {
                lst.Add(new ConfirmCodeForReportViewModel
                {
                    Id = (int)item,
                    Name = item.GetEnumDisplayName()
                });
            }

            return  lst; 
        }  

        public List<TestAcceptViewModel> GetReceptionNumber()
        {
            var testRequets = _TestAccepts.AsQueryable();

            var result = testRequets.Select(e => new
            {
                 e.Id, 
                e.ReceptionNumber
            }).AsEnumerable() 
             
            .Select(e => new TestAcceptViewModel
            {
                Id = e.Id, 
                ReceptionNumber = e.ReceptionNumber
            })
            .ToList();

            return result;
        }

    }
}
