
using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class TestAccept
    {
        [Key]
        public int Id { get; set; }
        public int ControlPlanId { get; set; }
        public int TestRequestId { get; set; }
        public DateTime CreateDate { get; set; }
        public int ConfirmCode { get; set; } 
        public string ReceptionNumber { get; set; }

        public ControlPlan ControlPlan { get; set; }
        public TestRequest TestRequest { get; set; }
    }
}
