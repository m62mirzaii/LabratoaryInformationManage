using Models.Model;

namespace Models.ViewModel
{
    public class ControlPlanPieceViewModel
    { 
        public int Id { get; set; }
        public int PieceId { get; set; }
        public int ControlPlanId { get; set; }
        public string Code { get; set; }
        public string PieceName { get; set; }
        public int PieceUsageId { get; set; }
        public string UsageName { get; set; } 
    }
}
