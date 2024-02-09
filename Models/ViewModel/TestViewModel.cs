using Models.Model;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel 
{
    public class TestViewModel
    { 
        public int Id { get; set; }
        public int TestConditionId { get; set; }
        public int TestImportanceId  { get; set; }
        public int LabratoaryToolId { get; set; }   
        public string? TestName { get; set; } 
        public string? TestConditionName { get; set; }
        public string TestImportanceName { get; set; }
        public string LabratoaryToolName { get; set; }
        public long Amount { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public int StandardId { get; set; }
        public string StandardName { get; set; }
        public int MeasureId { get; set; }
        public string MeasureName { get; set; }
        public int? TestDescriptionId { get; set; }
        public string TestDescriptionName { get; set; }
        public string? FromDate { get; set; }
        public string? EndDate { get; set; }
 

    }
}
