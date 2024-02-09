
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class TestRequestDetailViewModel
    {
        [Key]
        public int Id { get; set; }
        public int TestRequestId { get; set; }
        public string TestId { get; set; }
        public string TestName { get; set; }
        public string TestConditionName { get; set; }
    }
}
