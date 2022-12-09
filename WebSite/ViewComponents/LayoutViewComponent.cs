using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebSite.ViewComponents
{
    public class LayoutViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public LayoutViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; 
        }
        public string Invoke()
        {            
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name; 

            return userName ?? "Is Null" ;    
        }
    }   
}
