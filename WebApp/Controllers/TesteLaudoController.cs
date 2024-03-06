using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class TesteLaudoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public TesteLaudoController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetTesteLaudoAll();

            return View(new TesteLaudoModel() { TesteLaudos = response });
        }

        //[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new TesteLaudoModel.CreateUpdateTesteLaudoCommand
                {
                    Classificacao = collection["classificacao"].ToString(),
                    PontoInicial = Convert.ToInt32(collection["pontoInicial"].ToString()),
                    PontoFinal = Convert.ToInt32(collection["pontoFinal"].ToString()),
                    Aviso = Convert.ToInt32(collection["aviso"].ToString()),
                    Texto = Convert.ToInt32(collection["texto"].ToString()),
                };

                await ApiClientFactory.Instance.CreateTesteLaudo(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(IFormCollection collection)
        {
            var command = new TesteLaudoModel.CreateUpdateTesteLaudoCommand
            {
                Id = Convert.ToInt32(collection["editTesteLaudoId"]),
                Classificacao = collection["classificacao"].ToString(),
                PontoInicial = Convert.ToInt32(collection["pontoInicial"].ToString()),
                PontoFinal = Convert.ToInt32(collection["pontoFinal"].ToString()),
                Aviso = Convert.ToInt32(collection["aviso"].ToString()),
                Texto = Convert.ToInt32(collection["texto"].ToString()),
			};

            await ApiClientFactory.Instance.UpdateTesteLaudo(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteTesteLaudo(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public Task<TesteLaudoDto> GetTesteLaudoById(int id)
        {
            var result = ApiClientFactory.Instance.GetTesteLaudoById(id);

            return Task.FromResult(result);
        }
    }
}
