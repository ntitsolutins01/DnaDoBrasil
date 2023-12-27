using Infraero.Relprev.CrossCutting.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Areas.Identity.Models;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class ContratoController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		public ContratoController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetContratoAll();

			return View(new ContratoModel() { Contratos = response });
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
				var command = new ContratoModel.CreateUpdateContratoCommand
				{
					Nome = collection["nome"].ToString().ToUpper(),
					Descricao = collection["descricao"].ToString(),
					DtIni = collection["dtini"].ToString(),
					DtFim = collection["dtfim"].ToString(),
					Anexo = collection["anexo"].ToString()
				};

				await ApiClientFactory.Instance.CreateContrato(command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

		//[ClaimsAuthorize("Usuario", "Alterar")]
		public async Task<ActionResult> Edit(IFormCollection collection)
		{
			var command = new ContratoModel.CreateUpdateContratoCommand
			{
				Id = Convert.ToInt32(collection["editContratoId"]),
				Nome = collection["nome"].ToString().ToUpper(),
				Descricao = collection["descricao"].ToString(),
				DtIni = collection["dtini"].ToString(),
				DtFim = collection["dtfim"].ToString(),
				Status = collection["editStatus"].ToString() == "" ? false : true,
				Anexo = collection["anexo"].ToString()
			};

			await ApiClientFactory.Instance.UpdateContrato(command.Id, command);

			return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteContrato(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public async Task<ContratoDto> GetContratoById(int id)
		{
			var result = ApiClientFactory.Instance.GetContratoById(id);

			return result;
		}
	}
}

