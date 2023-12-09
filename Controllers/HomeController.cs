using Microsoft.AspNetCore.Mvc;

namespace WePhone.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
