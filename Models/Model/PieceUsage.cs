using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class PieceUsage
    {
        [Key]
        public int Id { get; set; }
        public string? UsageName { get; set; }
        public bool IsActive { get; set; }

    }
}
