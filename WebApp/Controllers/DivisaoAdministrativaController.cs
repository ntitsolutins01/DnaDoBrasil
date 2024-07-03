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
    public class DivisaoAdministrativaController : BaseController
	{
		private readonly ILogger<DivisaoAdministrativaController> _logger;
		private readonly IOptions<UrlSettings> _appSettings;


		public DivisaoAdministrativaController(ILogger<DivisaoAdministrativaController> logger, IOptions<UrlSettings> appSettings)
		{
			_logger = logger;
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
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
                return Task.FromResult(Json(ex));
            }
        }



        public Task<JsonResult> GetMunicipioByFomento(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Fomento não informado.");

                var fomento = ApiClientFactory.Instance.GetFomentoById(Convert.ToInt32(id));

                var result = new List<string>
                {
                    fomento.MunicipioId.ToString(),
                    fomento.MunicipioEstado.Split("/")[1].Trim()
                };

                return Task.FromResult(Json(result));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex));
            }
        }
    }
}
