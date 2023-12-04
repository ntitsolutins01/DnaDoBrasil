using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class FomentoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public FomentoController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetAmbienteAll();

            return View(new AmbienteModel() { Ambientes = response });
        }
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeAll(), "Id", "Nome");
            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");
            var model = new FomentoModel
            {
                ListLocalidades = localidades,
                ListEstados = estados

            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new SerieModel.CreateUpdateSerieCommand
                {

                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
                    IdadeInicial = Convert.ToInt32(collection["idadeInicial"]),
                    IdadeFinal = Convert.ToInt32(collection["idadeFinal"]),
                    ScoreTotal = Convert.ToInt32(collection["scoreTotal"].ToString())
                };

                await ApiClientFactory.Instance.CreateSerie(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
    public async Task<ActionResult> Edit(string id, IFormCollection collection)
    {
        var command = new SerieModel.CreateUpdateSerieCommand
        {
            Id = Convert.ToInt32(id),
            Nome = collection["nome"].ToString(),
            Descricao = collection["descricao"].ToString(),
            IdadeInicial = Convert.ToInt32(collection["idadeInicial"]),
            IdadeFinal = Convert.ToInt32(collection["idadeFinal"]),
            ScoreTotal = Convert.ToInt32(collection["scoreTotal"].ToString())
        };

        //await ApiClientFactory.Instance.UpdateSerie(command);

        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
    }
}
