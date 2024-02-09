
using Core.CacheMemory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebSite.Filters
{
    public class AuthorizeAttribute : Attribute, IActionFilter
    {

        public string systemName;
        public string methodName;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var userAccessCacheMemory = context.HttpContext.RequestServices.GetService<IUserAccessCacheMemory>();
                var httpContextAccessor = context.HttpContext.RequestServices.GetService<IHttpContextAccessor>();
                int userId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var isAuthorize = userAccessCacheMemory.CheckUserAccess(userId, systemName);

                if (!isAuthorize)
                {
                    context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Home" }, { "action", "NotAllowed" } });
                }
            }
            catch (Exception)
            {

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
