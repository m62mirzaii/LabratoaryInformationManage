namespace Models.ViewModel
{
    public class PieceViewModel
    {
 
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? PieceName { get; set; }
        public int PieceUsageId { get; set; }
        public string? PieceUsageName { get; set; }
        public bool IsActive { get; set; }
    }
}
