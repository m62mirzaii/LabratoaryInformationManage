using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class TestRequestDetail
    {
        [Key]
        public int Id { get; set; }
        public int TestRequestId { get; set; } 
        public int TestId { get; set; } 

        public TestRequest TestRequest { get; set; }
        public Test Test { get; set; }
    }
}
