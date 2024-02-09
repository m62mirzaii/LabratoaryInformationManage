using Models.Model;

namespace Core.Services.LabratoryTools
{
    public interface ILabratoaryToolRepository
    {
        Task<LabratoaryTool> GetLabratoaryToolById(int id);
        List<LabratoaryTool> GetLabratoaryTools();
        List<LabratoaryTool> GetLabratoaryToolForSelect2(string searchTerm);
        void AddLabratoaryTool(LabratoaryTool LabratoaryTool);
        bool UpdateLabratoaryTool(  LabratoaryTool LabratoaryTool);
        void DeleteLabratoaryTool(int id);
    }
}
