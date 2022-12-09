using Models.Model;
using Models.ViewModel;

namespace Core.Repository
{
    public interface IPieceRepository
    {
        Task<Piece> GetPieceById(int id);
        Task<List<PieceViewModel>> GetPieces();
        void AddPiece(Piece piece);
        bool UpdatePiece(int id, Piece piece);
        void DeletePiece(int id);
    }
}
