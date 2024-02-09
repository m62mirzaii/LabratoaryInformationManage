using Core.Services.System;
using Microsoft.AspNetCore.Mvc;
using Service.Forms;
using Service.User;

namespace WebSite.Controllers
{
    
    public class UserAccessController : Controller
    {
        ISystemRepository _systemService;
        IUserAccessService _userAccessService;
        IUserRepository _userService;

        public UserAccessController(IUserAccessService userAccessService, IUserRepository userService, ISystemRepository systemService)
        {
            _userAccessService = userAccessService;
            _userService = userService;
            _systemService = systemService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetByUserId(int userId)
        {
            var result = _userAccessService.GetByUserId(userId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult GetSystems_ForInsert()
        {
            var result = _userAccessService.GetSystems();
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult AddUserAccess(int userId, List<int> systemIds )
        {
            _userAccessService.Add(userId, systemIds);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteById(int id)
        {
            _userAccessService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
