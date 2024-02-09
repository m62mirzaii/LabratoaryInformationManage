using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class TestLabratoaryToolViewModel
    {
        [Key]
        public int Id { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int LabratoaryToolId { get; set; }
        public string LabratoaryToolName { get; set; }
    }
}
