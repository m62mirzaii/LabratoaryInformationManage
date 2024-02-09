 
using Core.Services.SystemMethodUsers;
using Microsoft.AspNetCore.Mvc;
using Service.Forms;
using Share.Extentions;

namespace WebSite.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAccessService _userAccessService;
        public MenuViewComponent(IHttpContextAccessor httpContextAccessor, IUserAccessService userAccessService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userAccessService = userAccessService;
        }

        //public IViewComponentResult Invoke()
        //{
        //    int userId = _httpContextAccessor.HttpContext.User.GetUserId(); 
        //    var result =   _userAccessService.GetSystemListByUserId(userId);
        //    return View(result);
        //}

        public  IViewComponentResult  Invoke()
        { 
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = _userAccessService.GetSystemListByUserIdForMenu(userId);
            return View(result);
        }
    }   
}
    