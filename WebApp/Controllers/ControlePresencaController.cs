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
    public class ControlePresencaController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public ControlePresencaController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetControlePresencaAll();

            return View(new ControlePresencaModel() { ControlePresencas = response });
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
                var command = new ControlePresencaModel.CreateUpdateControlePresencaCommand
                {
					MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()).ToString(),
					LocalidadeId = collection["ddlLocalidade"] == "" ? null : Convert.ToInt32(collection["ddlLocalidade"].ToString()),
					Controle = Convert.ToInt32(collection["controle"].ToString()),
					Justificativa = Convert.ToInt32(collection["justificativa"].ToString()),
                    AlunoId = 0
                };

                await ApiClientFactory.Instance.CreateControlePresenca(command);

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
            var command = new ControlePresencaModel.CreateUpdateControlePresencaCommand
            {
                Id = Convert.ToInt32(collection["editControlePresencaId"]),
				MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()).ToString(),
				LocalidadeId = collection["ddlLocalidade"] == "" ? null : Convert.ToInt32(collection["ddlLocalidade"].ToString()),
				Controle = Convert.ToInt32(collection["controle"].ToString()),
				Justificativa = Convert.ToInt32(collection["justificativa"].ToString()),
				AlunoId = 0
			};

            await ApiClientFactory.Instance.UpdateControlePresenca(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteControlePresenca(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public Task<ControlePresencaDto> GetControlePresencaById(int id)
        {
            var result = ApiClientFactory.Instance.GetControlePresencaById(id);

            return Task.FromResult(result);
        }
    }
}
