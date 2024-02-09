using Models.Model;

namespace Core.Services.RequestUnits
{
    public interface IRequestUnitRepository
    {
        Task<RequestUnit> GetRequestUnitById(int id); 
        List<RequestUnit> GetRequestUnits();
        List<RequestUnit> GetRequestUnitForSelect2(string searchTerm);
        void AddRequestUnit(RequestUnit pieceUsage);
        bool UpdateRequestUnit(  RequestUnit pieceUsage);
        void DeleteRequestUnit(int id);
    }
}
