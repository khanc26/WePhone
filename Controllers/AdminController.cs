using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;

namespace WePhone.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly AppDbContext _context;

        public AdminController(ILogger<AdminController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
			ViewData["CurrentTab"] = "Index";
            return View();
        }

        public IActionResult ProductCRUD()
        {
			List<Product> smartphones = _context.smartphones.ToList();
			return View(smartphones);
        }
        public IActionResult Info() {
            List<User> users = _context.Users.ToList();
            return View(users);
        }
        public IActionResult EditUser(int Id, string Username, string Password, string Full_Name, string Email, string Phone_Number, string Province_City, string District, string Ward, string Specific_Address)
        {
            //List<User> users= new List<User>();
            //var user = users.FirstOrDefault(u=>u.Id == Id);
            
                User user = new User
                {
                    Id = Id,
                    Username = Username,
                    Password = Password,
                    Full_Name = Full_Name,
                    Email = Email,
                    Phone_Number = Phone_Number,
                    Province_City = Province_City,
                    District = District,
                    Ward = Ward,
                    Specific_Address = Specific_Address,
                };
                return View(user);
        }
        public IActionResult UpdateUser(User user) {

            var users = user;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = _context.Users.Find(user.Id);
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Full_Name = user.Full_Name;
                existingUser.Email = user.Email;
                existingUser.Phone_Number = user.Phone_Number;
                existingUser.Province_City = user.Province_City;
                existingUser.District = user.District;
                existingUser.Ward = user.Ward;
                existingUser.Specific_Address = user.Specific_Address;

                try
                {
                    // Save changes back to the database
                    _context.SaveChanges();
                    return Ok("User updated successfully");
                }
                catch (Exception ex)
                {
                    // Handle database update exception
                    // Log the exception or return an appropriate error message
                    return StatusCode(500, "Error updating user");
                }
        }
        public IActionResult DeleteUser(User user)
        {
			var users = user;
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
            
            try
			{
                var userToDelete = _context.Users.Find(user.Id);
                if (userToDelete != null)
                {
                    _context.Users.Remove(userToDelete);
                    _context.SaveChanges();
                    return Ok("User deleted successfully");
                }
                else
                {
                    return NotFound(new { success = false, error = "User not found" });
                }
  
    //            _context.SaveChanges();
				//return RedirectToInfo();
			}
			catch (Exception ex)
			{
                
                return StatusCode(500, new { success = false, error = "Error deleting user" });
            }

		}
		public IActionResult Category()
		{
            //var users = _context.User.ToList();

            //// Log the car features to the console
            //foreach (var user in users)
            //{
            //    _logger.LogInformation($"User - Id: {user.User_Id}, FeatureName: {user.First_Name}");
            //}

            //return Ok(); // Optionally, you can return an IActionResult indicating success
            return View();
        }   


		public IActionResult Profile()
		{
			ViewData["CurrentTab"] = "Profile";
			return View();
		}

		public IActionResult SignUp()
		{
			ViewData["CurrentTab"] = "SignUp";
			return View();
		}

		public IActionResult SignIn()
		{
			ViewData["CurrentTab"] = "SignIn";
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult RedirectToInfo()
        {
            return RedirectToAction("Info", "Admin");
        }
    }
}