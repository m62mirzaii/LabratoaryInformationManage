using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string? TestName { get; set; }
        public int ProcessId { get; set; }
        public long Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
 

    }
}
