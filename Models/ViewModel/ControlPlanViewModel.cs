using Models.Model;

namespace Models.ViewModel
{
    public partial class ControlPlanViewModel
    {
        public ControlPlanViewModel()
        {
            ControlPlanPieceViewModels = new List<ControlPlanPieceViewModel>();
            ControlPlanProcessViewModels = new List<ControlPlanProcessViewModel>();
        }
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CreateDate { get; set; }
        public string PlanNumber { get; set; }
        public List<ControlPlanPieceViewModel> ControlPlanPieceViewModels { get; set; }
        public List<ControlPlanProcessViewModel> ControlPlanProcessViewModels { get; set; }
    }
}
