using Core.Repository;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using System.Net;

namespace Core.Services
{
    public class UserService : IUserRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Users> _user;
        private readonly DbSet<UserViewModel> _userViewModel;

        public UserService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _user = dbContext.Set<Users>();
        }

        public Users CheckLogin(string userName, string password)
        {
            return _user.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
        }

        public UserViewModel GetUserById(int id)
        {
            var user = _user.Where(x => x.Id == id).FirstOrDefault();

            var result = new UserViewModel
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Address = user.Address,
                UserName = user.UserName,
                Password = user.Password,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
            };

            return result;
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            var lstUser = await _user.ToListAsync();
            var result = lstUser.Select(x => new UserViewModel
            {
                Id = x.Id,
                FullName = $"{x.FirstName} {x.LastName}",
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                Address = x.Address,
                UserName = x.UserName,
                Password = x.Password,
                IsActive = x.IsActive,
                IsAdmin = x.IsAdmin,
            }).ToList();

            return result;
        }

        public void AddUser(Users user)
        {
            var _item = new Users
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Address = user.Address,
                UserName = user.UserName,
                Password = user.Password,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
            };

            _user.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateUser(int id, UserViewModel user)
        {
            var _item = _user.Find(id);
            if (_item != null)
            {
                _item.FirstName = user.FirstName;
                _item.LastName = user.LastName;
                _item.Phone = user.Phone;
                _item.Address = user.Address;
                _item.UserName = user.UserName;
                _item.Password = user.Password;
                _item.IsActive = user.IsActive;
                _item.IsAdmin = user.IsAdmin;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteUser(int id)
        {
            var _item = _user.Find(id);
            if (_item != null)
            {
                _user.Remove(_item);
                _dbContext.SaveChanges();
            }
        }
    }
}
