

using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class TestAcceptViewModel
    {
        [Key]
        public int Id { get; set; }
        public int TestRequestId { get; set; } 
        public int RequestNumber { get; set; }
        public int ControlPlanId { get; set; }
        public string PlanNumber { get; set; } 
        public string ReceptionNumber { get; set; }
        public string CreateDate { get; set; }
        public int ConfirmCode { get; set; }
        public string ConfirmCodeName { get; set; }
    }
}
