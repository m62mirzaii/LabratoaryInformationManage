using Core.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Models.ViewModel; 

namespace WebSite.Controllers
{ 
    public class HomeController : Controller
    {
        IUserRepository _userRepository;
        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        { 
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult LoginValidation(UserViewModel userViewModel)
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

        public IActionResult Logout()
        {            
            return RedirectToAction("Login", "Home");
        }

    }
}