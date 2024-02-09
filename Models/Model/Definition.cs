using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class Definition
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? EName { get; set; }
        public string? PName { get; set; }
        public bool IsActive { get; set; }
        public string? Path { get; set; }
        public string? Description { get; set; } 
    }
}
