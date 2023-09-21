using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class AlunoController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
