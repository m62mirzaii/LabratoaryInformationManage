using Models.Model;

namespace Core.Repository
{
    public interface IDefinitionRepository
    {
        Task<Definition> GetDefinitionById(int id);
        Task<List<Definition>> GetDefinitions();
        List<Definition> GetAllDefinitions();
        void AddDefinition(Definition definition);
        bool UpdateDefinition(int id, Definition definition);
        void DeleteDefinition(int id);
    }
}
