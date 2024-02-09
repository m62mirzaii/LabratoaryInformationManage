using Models.Model;
using System.Collections.Generic;

namespace Models.ViewModel
{
    public class ControlPlanProcessViewModel
    {
        public ControlPlanProcessViewModel()
        {
            ControlPlanProcessTestViewModels = new List<ControlPlanProcessTestViewModel>();
        }
        public int Id { get; set; }
        public int ControlPlanId { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string ProcessTypeId { get; set; }
        public string ProcessTypeName { get; set; }
        public List<ControlPlanProcessTestViewModel> ControlPlanProcessTestViewModels { get; set; }

    }
}
