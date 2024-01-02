using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sign_Up.Controllers
{
    public class SignUpController : Controller
    {
        private readonly LoginContext _dbContext;

        public SignUpController(LoginContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]


        public IActionResult SignUp(ULogin modelSignUp)
        {
            var user = _dbContext.ULogins.FirstOrDefault(u => u.Username == modelSignUp.Username || u.Email == modelSignUp.Email);
            bool usernameExists = user != null && user.Username == modelSignUp.Username;
            bool emailExists = user != null && user.Email == modelSignUp.Email;// xem user tồn tại hay email

            if (user == null)
            {

                ULogin newUser = new ULogin
                {
                    Username = modelSignUp.Username,
                    Password = modelSignUp.Password,
                    FullName = modelSignUp.FullName,
                    Email = modelSignUp.Email,
                    PhoneNumber = modelSignUp.PhoneNumber,
                    ProvinceCity = modelSignUp.ProvinceCity,
                    District = modelSignUp.District,
                    Ward = modelSignUp.Ward,
                    SpecificAddress = modelSignUp.SpecificAddress
                };

                _dbContext.ULogins.Add(newUser);
                _dbContext.SaveChanges();
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelSignUp.Username),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = false,
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                return RedirectToAction("Index", "Home");
            }
            else if (usernameExists)
            {
                ViewData["ValidateMessage"] = "Username already exists";
            }
            else if (emailExists)
            {
                ViewData["ValidateMessage"] = "Email already exists";
            }

            return View();
        }


    }
}
