using Models.Model;

namespace Core.Services.Measures
{
    public interface IMeasureRepository
    {
        Task<Measure> GetMeasureById(int id); 
        List<Measure> GetMeasures();
        List<Measure> GetMeasureForSelect2(string searchTerm);
        void AddMeasure(Measure pieceUsage);
        bool UpdateMeasure(  Measure pieceUsage);
        void DeleteMeasure(int id);
    }
}
