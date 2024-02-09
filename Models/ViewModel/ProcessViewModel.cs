namespace Models.ViewModel
{
    public class ProcessViewModel
    { 
        public int Id { get; set; }
        public string? ProcessName { get; set; }
        public int ProcessTypeId { get; set; }
        public string ProcessTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
