using Models.Model;

namespace Core.Repository
{
    public interface IPieceUsageRepository
    {
        Task<PieceUsage> GetPieceUsageById(int id);
        Task<List<PieceUsage>> GetPieceUsages();
         List<PieceUsage>  GetActivePieceUsages();
        void AddPieceUsage(PieceUsage pieceUsage);
        bool UpdatePieceUsage(int id, PieceUsage pieceUsage);
        void DeletePieceUsage(int id);
    }
}
