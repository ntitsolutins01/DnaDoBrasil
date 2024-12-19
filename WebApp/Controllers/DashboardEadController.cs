using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Factory;
using WebApp.Identity;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    [Authorize(Policy = ModuloAccess.DashboardEad)]
    public class DashboardEadController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;


        public DashboardEadController(ILogger<DashboardEadController> logger, IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
            
        }
        public async Task<IActionResult> Index(IFormCollection collection)
        {
            //var usu = User?.Identity.Name;
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            //var email = User.FindFirstValue(ClaimTypes.Email);
            //var token = HttpContext.Request.Cookies["token"];
            //ApplicationSettings.Token = HttpContext.Request.Cookies["token"];

            var searchFilter = new DashboardEadDto
            {
                FomentoId = collection["ddlFomento"].ToString(),
                Estado = collection["ddlEstado"].ToString(),
                MunicipioId = collection["ddlMunicipio"].ToString(),
                LocalidadeId = collection["ddlLocalidade"].ToString(),
                DeficienciaId = collection["ddlDeficiencia"].ToString(),
                Etnia = collection["ddlEtnia"].ToString()
            };

            var dashboardEad = new DashboardEadDto();

            var fomentos = new SelectList(ApiClientFactory.Instance.GetFomentoAll(), "Id", "Nome", dashboardEad.FomentoId);
            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", dashboardEad.Estado);
            var tipoCurso = new SelectList(ApiClientFactory.Instance.GetTipoCursosAll(), "Id", "Nome");

            SelectList municipios = null;

            if (!string.IsNullOrEmpty(dashboardEad.Estado))
            {
                municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(dashboardEad.Estado), "Id", "Nome", dashboardEad.MunicipioId);
            }
            SelectList localidades = null;

            if (!string.IsNullOrEmpty(dashboardEad.LocalidadeId))
            {
                localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeByMunicipio(dashboardEad.MunicipioId), "Id", "Nome", dashboardEad.LocalidadeId);
            }

            var model = new DashboardEadModel
            {
                ListFomentos = fomentos,
                ListEstados = estados,
                Dashboard = dashboardEad,
                ListMunicipios = municipios!,
                ListTipoCursos = tipoCurso,
                ListLocalidades = localidades!
            };



            return View(model);
        }
    }
}
