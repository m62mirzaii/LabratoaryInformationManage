using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class ControlPlanProcess
    {
        [Key]
        public int Id { get; set; }
        public int ControlPlanId { get; set; }
        public int ProcessId { get; set; }

        public Process Process { get; set; }
        public List<ControlPlanProcessTest> ControlPlanProcessTests { get; set; }
    }
}
