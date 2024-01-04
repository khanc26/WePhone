using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using WePhone.Models;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WePhone.Controllers
{
    public class AccessController : Controller
    {
        private readonly LoginContext _dbContext; 

        public AccessController(LoginContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ULogin modelLogin)
        {
            var user = _dbContext.ULogins.FirstOrDefault(u => u.Username == modelLogin.Username && u.Password == modelLogin.Password);

            if (user != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = false,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "User not found";
            return View();
        }

        [AllowAnonymous, HttpGet("Forget-Password")]
        public IActionResult ForgetPass()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("Forget-Password")]
        public IActionResult ForgetPass(ForgetPass ForgetPassModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View(ForgetPassModel);
        }
    }
}