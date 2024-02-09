using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class TestRequestViewModel
    {
        [Key]
        public int Id { get; set; }
        public int RequestNumber { get; set; }
        public string RequestDate { get; set; }
        public int RequestUnitId { get; set; }
        public string RequestUnitName { get; set; }
        public int RequestUserId { get; set; }
        public string RequestUserName { get; set; }
        public int PieceId { get; set; }
        public string PieceName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
