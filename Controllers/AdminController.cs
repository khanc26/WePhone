using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
    }
}