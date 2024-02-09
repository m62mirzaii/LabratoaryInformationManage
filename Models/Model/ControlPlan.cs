using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class ControlPlan
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string PlanNumber { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Company Company { get; set; }
    }
}
