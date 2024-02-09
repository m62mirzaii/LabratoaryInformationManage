using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class TestRequest
    {
        [Key]
        public int Id { get; set; }      
        public int RequestNumber { get; set; }
        public DateTime RequestDate { get; set; }        
        public int RequestUnitId { get; set; }
        public int RequestUserId { get; set; }
        public int PieceId { get; set; }
        public int CompanyId { get; set; } 

      
        public Company Company { get; set; }
        public RequestUnit RequestUnit { get; set; }
        public RequestUser RequestUser { get; set; }
        public Piece Piece { get; set; }
    }
}
