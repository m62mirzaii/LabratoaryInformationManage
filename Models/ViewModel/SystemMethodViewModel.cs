using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class SystemMethodViewModel
    { 
        public int Id { get; set; }
        public int SystemId { get; set; }
        public string? MethodName { get; set; }
        public string? SystemName { get; set; }
    }
}
