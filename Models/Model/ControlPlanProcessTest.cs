using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class ControlPlanProcessTest
    {
        [Key]
        public int Id { get; set; }
        public int ControlPlanProcessId { get; set; }
        public int TestId { get; set; } 

        public ControlPlanProcess ControlPlanProcess { get; set; }
        public Test Test { get; set; }  
    }
}
