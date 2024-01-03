using Microsoft.AspNetCore.Mvc;

namespace WePhone.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("UserId", "11");

            List<Smartphone> smartphones = await _context.Smartphones.ToListAsync();

            return View(smartphones);
        }

        public IActionResult Cart()
        {
            List<Cart> carts = _context.Carts.Where(c => c.User_Id == 11).ToList();
            _logger.LogInformation("This is cart");
            return View(carts);
        }

        public IActionResult PhoneDetail(int id, string? name, string? brand, int ram, int rom, decimal price, decimal discount, string? color, string? picture)
        {
            var phone = new Smartphone
            {
                Id = id,
                Name = name,
                Brand = brand,
                Ram = ram,
                Rom = rom,
                Price = price,
                Discount = discount,
                Color = color,
                Picture = picture
            };

            return View(phone);
        }
    }
}
