using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class TestAcceptDetail
    {
        [Key]
        public int Id { get; set; }
        public int TestAcceptId { get; set; }
        public int ControlPlanPieceId { get; set; }
        public int ControlPlanProcessTestId { get; set; }  
        public decimal Avarage { get; set; } 
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Humidity { get; set; }
        public int Temperature { get; set; }
        public bool IsConfirm { get; set; }
        public string? AnswerText { get; set; }
        public string? TestResult { get; set; } 

        public TestAccept TestAccept { get; set; }
        public ControlPlanProcessTest ControlPlanProcessTest { get; set; }
        public ControlPlanPiece ControlPlanPiece { get; set; }
    }
}
