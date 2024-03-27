using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BemVindo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
