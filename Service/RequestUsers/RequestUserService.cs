using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.RequestCompanies
{
    public class RequestUserService : IRequestUserRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<RequestUser> _requestUser;

        public RequestUserService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _requestUser = dbContext.Set<RequestUser>();
        }

        public async Task<RequestUser> GetRequestUserById(int id)
        {
            return await _requestUser.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public  List<RequestUser> GetRequestUserForSelect2(string searchTerm)
        {
            var query = _requestUser.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public List<RequestUser> GetRequestUsers()
        {
            return _requestUser
            .OrderByDescending(e => e.Id)
            .ToList();
        }

        public void AddRequestUser(RequestUser requestUser)
        {
            var _item = new RequestUser
            {
                Name = requestUser.Name
            };

            _requestUser.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateRequestUser(RequestUser requestUser)
        {
            var _item = _requestUser.Find(requestUser.Id);
            if (_item != null)
            {
                _item.Name = requestUser.Name;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteRequestUser(int id)
        {
            var _item = _requestUser.Find(id);
            if (_item != null)
            {
                _requestUser.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
