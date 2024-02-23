using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
	public class DashboardController : BaseController
	{
		private readonly ILogger<DashboardController> _logger;
		private readonly IOptions<UrlSettings> _appSettings;


		public DashboardController(ILogger<DashboardController> logger, IOptions<UrlSettings> appSettings)
		{
			_logger = logger;
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(IFormCollection collection)
		{
			var searchFilter = new DashboardIndicadoresDto
			{
					FomentoId = collection["ddlFomento"].ToString(),
					Estado = collection["ddlEstado"].ToString(),
					MunicipioId = collection["ddlMunicipio"].ToString(),
					LocalidadeId = collection["ddlLocalidade"].ToString(),
                    DeficienciaId = collection["ddlDeficiencia"].ToString(),
                    Etnia = collection["ddlEtnia"].ToString()
			};

			var indicadores = ApiClientFactory.Instance.GetIndicadoresByFilter(searchFilter);
			var fomentos = new SelectList(ApiClientFactory.Instance.GetFomentoAll(), "Id", "Nome", indicadores.FomentoId);
			var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll(), "Id", "Nome", indicadores.DeficienciaId);
			var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", indicadores.Estado);

            List<SelectListDto> list = new List<SelectListDto>
            {
                new() { IdNome = "PARDO", Nome = "PARDO" },
                new() { IdNome = "BRANCO", Nome = "BRANCO" },
                new() { IdNome = "PRETO", Nome = "PRETO" },
                new() { IdNome = "INDÍGENA", Nome = "INDÍGENA" },
                new() { IdNome = "AMARELO", Nome = "AMARELO" }
            };

            var etnias = new SelectList(list, "IdNome", "Nome", indicadores.Etnia);
            SelectList municipios = null;

            if (!string.IsNullOrEmpty(indicadores.Estado))
            {
                municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(indicadores.Estado), "Id", "Nome", indicadores.MunicipioId);
            }
            SelectList localidades = null;

            if (!string.IsNullOrEmpty(indicadores.LocalidadeId))
            {
                localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeByMunicipio(indicadores.MunicipioId), "Id", "Nome", indicadores.LocalidadeId);
            }
			

			var model = new DashboardModel
			{
				ListFomentos = fomentos,
				ListEstados = estados,
				Indicadores = indicadores,
				ListDeficiencias = deficiencias,
				ListMunicipios = municipios!,
                ListEtnias = etnias,
				ListLocalidades = localidades!

            };
			return View(model);
		}

		public Task<JsonResult> GetMunicipioByUf(string uf)
		{
			try
			{
				if (string.IsNullOrEmpty(uf)) throw new Exception("Estado não informado.");
				var resultLocal = ApiClientFactory.Instance.GetMunicipiosByUf(uf);

				return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Nome")));

			}
			catch (Exception ex)
			{
				return Task.FromResult(Json(ex.Message));
			}
		}
	}
}
