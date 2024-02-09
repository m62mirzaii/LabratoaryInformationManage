using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class Piece 
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }  
        public string? PieceName { get; set; }
        public int? PieceUsageId { get; set; }
        public bool IsActive { get; set; }

        public PieceUsage? PieceUsage { get; set; }
    }
}
