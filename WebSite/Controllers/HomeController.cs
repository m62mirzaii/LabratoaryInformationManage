using Core.CacheMemory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Models.ViewModel;
using Service.User;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository _userRepository; 

        public HomeController(IUserRepository userRepository )
        {
            _userRepository = userRepository; 
        }
        public ActionResult Index()
        { 
            //  return View();
             return RedirectToAction("Login", "Home");
        }

        public ActionResult Main()
        { 
            return View();
        }

        public ActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult LoginValidation(UserViewModel userViewModel)
        {
            var userName = userViewModel.UserName;
            var password = userViewModel.Password;
            ViewBag.IsValid = false;

            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            { 
                var user = _userRepository.CheckLogin(userName, password);
                if (user != null)
                {
                    ViewBag.IsValid = true; 

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    HttpContext.SignInAsync(principal, properties); 

                    return RedirectToAction("Main", "Home");
                }
            } 
         
            return View("Login", userViewModel);
        }

        public ActionResult Logout()
        {            
            return RedirectToAction("Login", "Home");
        } 
    }
}