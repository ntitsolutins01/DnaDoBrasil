using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class CadeiaProdutivaController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Create()
		{
			return View();
		}
	}
}
