using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class SystemMethodUserViewModel
    { 
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int SystemId { get; set; }
        public string? SystemName { get; set; }
        public int SystemMethodId { get; set; }
        public string? MethodName { get; set; }
    }
}
