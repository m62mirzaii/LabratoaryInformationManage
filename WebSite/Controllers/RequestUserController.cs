using Core.Services.RequestCompanies;
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace WebSite.Controllers
{
    //   [Authorize(systemName = "RequestUser")]

    public class RequestUserController : Controller
    {
        public IRequestUserRepository _RequestUserService;

        public RequestUserController(IRequestUserRepository RequestUserService)
        {
            _RequestUserService = RequestUserService;
        }

        public IActionResult Index()
        {
            var result = _RequestUserService.GetRequestUsers();
            return View(result);
        }

        [HttpPost]
        public IActionResult GetRequestUsers()
        {
            var result = _RequestUserService.GetRequestUsers();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public JsonResult GetRequestUserForSelect2(string searchTerm)
        {
            var result = _RequestUserService.GetRequestUserForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]
        public void AddRequestUser()
        {
            var name = Request.Form["Name"].ToString();
            var requestUser = new RequestUser
            {
                Name = name,
            };
            _RequestUserService.AddRequestUser(requestUser);
        }

        [HttpPost]
        public void UpdateRequestUser()
        {
            var id = Convert.ToInt32(Request.Form["Id"].ToString());
            var name = Request.Form["Name"].ToString();

            var requestUser = new RequestUser
            {
                Id = id,
                Name = name,
            };
            _RequestUserService.UpdateRequestUser(requestUser);
        }

        public IActionResult Delete(int id)
        {
            _RequestUserService.DeleteRequestUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}