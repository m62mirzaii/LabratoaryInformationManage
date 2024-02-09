using Core.Services;
using Core.Services.System;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;

namespace WebSite.Controllers
{
    public class SystemController : Controller
    {
        public ISystemRepository _systemRepository;

        public SystemController(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetSystems()
        {
            var result = _systemRepository.GetSystems();
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

    }
}