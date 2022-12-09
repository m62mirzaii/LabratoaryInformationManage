namespace Models.ViewModel
{
    public class ProcessViewModel
    { 
        public int Id { get; set; }
        public string? ProcessName { get; set; }
        public int DefinitionId { get; set; }
        public string DefinitionName { get; set; }
        public bool IsActive { get; set; }
    }
}
