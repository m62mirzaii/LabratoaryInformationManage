using Models.Model;
using Models.ViewModel;

namespace Core.Services.Pieces
{
    public interface IPieceRepository
    {
        Task<Piece> GetPieceById(int id);
        List<Piece> GetPieceForSelect2(string searchTerm);
        List<PieceViewModel> GetPieces();
        void AddPiece(PieceViewModel piece);
        bool UpdatePiece(PieceViewModel piece);
        void DeletePiece(int id);
    }
}
