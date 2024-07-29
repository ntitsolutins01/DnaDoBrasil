using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
        {
            //return Redirect("/Identity/Account/Login");
            //return RedirectToPage("/Identity/Account/Login");
            return View();
        }
		public IActionResult EmpresaParceira()
        {
            return View();
        }
		public IActionResult Index2()
        {
            return Redirect("/Identity/Account/Login");
            //return RedirectToPage("/Identity/Account/Login");
            //return View();
        }
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}