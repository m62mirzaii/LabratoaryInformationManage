using Models.Model;

namespace Models.ViewModel
{
    public class ControlPlanProcessTestViewModel
    { 
        public int Id { get; set; }
        public int ControlPlanProcessId { get; set; }
        //public int ControlPlanPieceId { get; set; }
        //public string PieceName { get; set; }
        public string ProcessName { get; set; }
        public string ProcessTypeName { get; set; }
        public string? TestName { get; set; }
        public string? TestConditionName { get; set; }
        public long Amount { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public string StandardName { get; set; }
        public string MeasureName { get; set; } 
        public string TestDescriptionName { get; set; }
    }
}
