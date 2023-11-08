using Microsoft.AspNetCore.Mvc;
using WebApp.Factory;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class AlunoController : BaseController
    {
        public ActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            //var deficiencias = ApiClientFactory.Instance.GetDeficienciasAll();

            //return View(new AlunoModel { Deficiencias = deficiencias });
            return View();
        }
        public IActionResult Create()
		{
			return View();
		}
		public IActionResult Laudo()
		{
			return View();
		}
		public IActionResult TalentoEsportivo()
		{
			return View();
		}
		public IActionResult Vocacional()
		{
			return View();
		}
		public IActionResult QualidadeVida()
		{
			return View();
		}
		public IActionResult Saude()
		{
			return View();
		}
		public IActionResult ConsumoAlimentar()
		{
			return View();
		}
		public IActionResult SaudeBucal()
		{
			return View();
		}
	}
}
