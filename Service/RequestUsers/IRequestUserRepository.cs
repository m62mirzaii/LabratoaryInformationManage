using Models.Model;

namespace Core.Services.RequestCompanies
{
    public interface IRequestUserRepository
    {
        Task<RequestUser> GetRequestUserById(int id);
        List<RequestUser> GetRequestUserForSelect2(string searchTerm);
        List<RequestUser> GetRequestUsers();
        void AddRequestUser(RequestUser pieceUsage);
        bool UpdateRequestUser(  RequestUser pieceUsage);
        void DeleteRequestUser(int id);
    }
}
