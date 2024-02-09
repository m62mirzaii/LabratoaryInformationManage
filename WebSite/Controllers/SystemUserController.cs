using Core.Services;
using Core.Services.SystemUsers;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;
using WebSite.Filters;

namespace WebSite.Controllers
{
    //[Authorize(systemName = "SystemUser")]
    public class SystemUserController : Controller
    {
        public ISystemUserRepository _systemUserRepository;

        public SystemUserController(ISystemUserRepository systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUsers()
        {
            var result = _systemUserRepository.GetUsers();
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult GetByUserId(int userId)
        {
            var result = _systemUserRepository.GetByUserId(userId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            Users users = new Users();
            return PartialView("_InsertPartial", users);
        }

        public IActionResult AddSystemUser(int userId, List<int> systemIds)
        {
            _systemUserRepository.AddSystemUser(userId, systemIds);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Show_UpdatePartial()
        {
            Systems systems = new Systems();
            return PartialView("_UpdatePartial", systems);
        }

        public IActionResult UpdateSystemUser(int userId, List<int> systemIds)
        {
            _systemUserRepository.UpdateSystemUser(userId, systemIds);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteByUserId(int userId)
        {
            _systemUserRepository.DeleteByUserId(userId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteById(int id)
        {
            _systemUserRepository.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }



    }
}