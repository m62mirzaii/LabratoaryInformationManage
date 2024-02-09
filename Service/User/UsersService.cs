using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using Models.ViewModel;
using Service.User;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services.User
{
    public class UserService : IUserRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Users> _user;


        public UserService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _user = dbContext.Set<Users>();
        }

        public Users CheckLogin(string userName, string userPassword)
        {
            var encryptPassword = EncryptPassword(userPassword);

            return _user.Where(x => x.UserName == userName && x.Password == encryptPassword).FirstOrDefault();
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
                BankAccountNo = user.BankAccountNo
            };

            return result;
        }

        public List<UserViewModel> GetUsers()
        {
            var users = _user.AsQueryable();
            var result = users.Select(e => new
            {
                e.Id,
                FullName = $"{e.FirstName} {e.LastName}",
                e.FirstName,
                e.LastName,
                e.Phone,
                e.Address,
                e.UserName,
                e.Password,
                e.IsActive,
                e.IsAdmin,
                e.BankAccountNo
            })
            .AsEnumerable()
            .OrderByDescending(e => e.Id)
            .Select(e => new UserViewModel
            {
                Id = e.Id,
                FullName = e.FullName,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Phone = e.Phone,
                Address = e.Address,
                UserName = e.UserName,
                Password = e.Password,
                IsActive = e.IsActive,
                IsAdmin = e.IsAdmin,
                BankAccountNo = e.BankAccountNo
            }).ToList();

            return result;
        }

        public List<UserViewModel> GetUserForSelect2(string searchTerm)
        {
            var query = _user.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.FirstName.Contains(searchTerm) || e.LastName.Contains(searchTerm));

            var result = query.Select(e => new UserViewModel
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}",  
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
                Password = EncryptPassword(user.Password),
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                BankAccountNo = user.BankAccountNo
            };

            _user.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateUser( UserViewModel user)
        {
            var _item = _user.Find(user.Id);
            if (_item != null)
            {
                _item.FirstName = user.FirstName;
                _item.LastName = user.LastName;
                _item.Phone = user.Phone;
                _item.Address = user.Address;
                _item.UserName = user.UserName;
                _item.Password = EncryptPassword(user.Password);
                _item.IsActive = user.IsActive;
                _item.IsAdmin = user.IsAdmin;
                _item.BankAccountNo = user.BankAccountNo;

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

        public string EncryptPassword(string password)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(password);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string DecryptPassword(string key, string cipherText)
        {
            byte[] iv = new byte[16];

            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
