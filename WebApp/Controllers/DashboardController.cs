using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Dto;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
	public class DashboardController : BaseController
	{
		private readonly ILogger<DashboardController> _logger;
		private readonly IOptions<SettingsModel> _appSettings;


		public DashboardController(ILogger<DashboardController> logger, IOptions<SettingsModel> appSettings)
		{
			_logger = logger;
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index()
		{
			var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeAll(), "Id", "Nome");
			var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");
			var model = new DashboardModel
			{
				ListLocalidades = localidades,
				ListEstados = estados

			};
			return View(model);
		}
		[HttpGet("GetMunicipioByUf/{uf}")]
		public JsonResult GetMunicipioByUf([FromQuery]string uf)
		{
			var resultLocal = ApiClientFactory.Instance.GetMunicipiosByUf(uf);


			return Json(new SelectList(resultLocal, "Id", "Nome"));
		}
	}
}
