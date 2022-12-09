using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel 
{
    public class TestViewModel
    { 
        public int Id { get; set; }
        public int ProcessId { get; set; }
        
        public string? TestName { get; set; } 
        public string? ProcessName { get; set; }
        public long Amount { get; set; }
        public string? FromDate { get; set; }
        public string? EndDate { get; set; }
 

    }
}
