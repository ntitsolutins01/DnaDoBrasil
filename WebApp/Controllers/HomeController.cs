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

		private readonly ILogger<HomeController> _logger;
		private readonly IOptions<SettingsModel> _appSettings;


		public HomeController(ILogger<HomeController> logger, IOptions<SettingsModel> appSettings)
		{
			_logger = logger;
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index()
		{
			var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeAll(), "Id", "Nome");
			//var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Id", "Nome"); 
			//var model = new DashboardModel
			//{
			//	ListLocalidades = localidades,
			//	ListEstados = estados 
				
			//}; //TOdo: Fabio por favor resolver esse erro 
			return View(model);
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