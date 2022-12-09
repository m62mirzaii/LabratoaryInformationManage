using Models.Model;

namespace Core.Repository
{
    public interface ILabratoaryToolRepository
    {
        Task<LabratoaryTool> GetLabratoaryToolById(int id);
        Task<List<LabratoaryTool>> GetLabratoaryTools();
        void AddLabratoaryTool(LabratoaryTool LabratoaryTool);
        bool UpdateLabratoaryTool(int id, LabratoaryTool LabratoaryTool);
        void DeleteLabratoaryTool(int id);
    }
}
