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
                var command = new FomentoModel.CreateUpdateFomentoCommand
                {
                    Fomento = collection["fomento"].ToString(),
                    Localidade = collection["localidades"].ToString(),
                    Estado = collection["estado"].ToString(),
                    Cidade = Convert.ToInt32(collection["cidade"]),
                    
                await ApiClientFactory.Instance.CreateFomento(command);

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
        var command = new FomentoModel.CreateUpdateFomentoCommand
        {
            Id = Convert.ToInt32(id),
            Estado = collection["Estado"].ToString(),
            Cidade = collection["Cidade"].ToString(),
            Localidade = Convert.ToInt32(collection["Localidade"]),
           
        };

        //await ApiClientFactory.Instance.UpdateFomento(command);

        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
    }
}
