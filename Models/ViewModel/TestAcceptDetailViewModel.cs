
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class TestAcceptDetailViewModel
    {
        public int Id { get; set; }
        public int TestAcceptId { get; set; }
        public int ControlPlanPieceId { get; set; } 
        public int ControlPlanProcessTestId { get; set; } 
        public decimal Avarage { get; set; }  
        public string FromDate { get; set; }
        public string EndDate { get; set; }
        public string UserName { get; set; }
        public int Humidity { get; set; }
        public int Temperature { get; set; }
        public string PieceName { get; set; }
        public string ProcessName { get; set; }
        public string ProcessTypeName { get; set; } 
        public string? TestName { get; set; }
        public string? TestConditionName { get; set; }
        public long Amount { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public string StandardName { get; set; }
        public string MeasureName { get; set; }
        public string TestImportanceName { get; set; }
        public string TestDescriptionName { get; set; }
        public int ConfirmCode_TestAccept { get; set; }
        public bool IsConfirm{ get; set; }
        public string AnswerText { get; set; }
        public string TestResult { get; set; }
        public int LabratoaryToolId { get; set; }
        public string LabratoaryToolName { get; set; }
        public int? TestRequestDetailId { get; set; }
        public bool IsConflict { get; set; } = false;
        public int RowNumber { get; set; }
    }
}
