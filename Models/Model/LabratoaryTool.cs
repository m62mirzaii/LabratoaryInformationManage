using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class LabratoaryTool
    {
        [Key]
        public int Id { get; set; }
        public string? ToolName { get; set; }
        public bool IsActive { get; set; }
    }
}
