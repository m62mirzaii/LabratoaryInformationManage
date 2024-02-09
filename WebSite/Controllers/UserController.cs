using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;
using Service.User;

namespace WebSite.Controllers
{
    //[Authorize(systemName = "User")]
    public class UserController : Controller
    {
        public IUserRepository _userService;

        public UserController(IUserRepository userService)
        {
            _userService = userService;
        }
          
        public  IActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult GetUsers()
        {
            var result = _userService.GetUsers(); 
            var jsonData = new { data = result };

            return Ok(jsonData);
        }  

        [HttpPost]
        public JsonResult GetUserForSelect2(string searchTerm)
        {
            var result = _userService.GetUserForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]  
        public void AddUser()
        {
            var firstName = Request.Form["FirstName"].ToString();
            var lastName = Request.Form["LastName"].ToString();
            var address = Request.Form["Address"].ToString();
            var phone = Request.Form["Phone"].ToString();
            var userName = Request.Form["UserName"].ToString();
            var password = Request.Form["Password"].ToString();
            var bankAccountNo = Request.Form["BankAccountNo"].ToString();
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());
            var isAdmin = Convert.ToBoolean(Request.Form["IsAdmin"].ToString());

            var user = new Users 
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Phone = phone,
                UserName = userName,
                Password = password,
                BankAccountNo = bankAccountNo,
                IsAdmin = isAdmin,
                IsActive = IsActive,
            };
            _userService.AddUser(user); 
        }

        [HttpPost] 
        public void UpdateUser( )
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var firstName = Request.Form["FirstName"].ToString();
            var lastName = Request.Form["LastName"].ToString();
            var address = Request.Form["Address"].ToString();
            var phone = Request.Form["Phone"].ToString();
            var userName = Request.Form["UserName"].ToString();
            var password = Request.Form["Password"].ToString();
            var bankAccountNo = Request.Form["BankAccountNo"].ToString(); 
            var IsActive = Convert.ToBoolean(Request.Form["IsActive"].ToString());
            var isAdmin = Convert.ToBoolean(Request.Form["IsAdmin"].ToString());

            var user = new UserViewModel
            {
                Id= id,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Phone = phone,
                UserName = userName,
                Password = password,
                BankAccountNo = bankAccountNo,
                IsAdmin = isAdmin,
                IsActive = IsActive,
            };
            _userService.UpdateUser(  user); 
        }

        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}