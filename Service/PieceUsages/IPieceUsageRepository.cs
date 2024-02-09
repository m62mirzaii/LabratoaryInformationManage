using Models.Model;

namespace Core.Services.PieceUsages
{
    public interface IPieceUsageRepository
    {
        Task<PieceUsage> GetPieceUsageById(int id);
        List<PieceUsage> GetPieceUsages();
        List<PieceUsage> PieceUsageForSelect2(string searchTerm);
        void AddPieceUsage(PieceUsage pieceUsage);
        bool UpdatePieceUsage( PieceUsage pieceUsage);
        void DeletePieceUsage(int id);
    }
}
