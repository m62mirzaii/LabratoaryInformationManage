using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;

namespace WebSite.Controllers
{
    public class UserController : Controller
    {
        public IUserRepository _userService;

        public UserController(IUserRepository userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result =  await _userService.GetUsers();
            return View(result);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            Users users = new Users();
            return PartialView("_InsertPartial", users);
        }

        public IActionResult AddUser(Users users)
        {
            _userService.AddUser(users);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult  Show_UpdatePartial([FromBody] UserViewModel lab)
        {
            int id = lab.Id;
            UserViewModel users =   _userService.GetUserById(id);
            return PartialView("_UpdatePartial", users);
        }

        public IActionResult UpdateUser(UserViewModel users)
        {
            int id = users.Id;
            bool result = _userService.UpdateUser(id, users);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}