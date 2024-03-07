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
	public class TextoLaudoController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		public TextoLaudoController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetTextosLaudosAll();

			return View(new TextoLaudoModel() { TextosLaudos = response });
		}

		//[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
		public ActionResult Create(int? crud, int? notify, string message = null)
		{
			try
			{
				SetNotifyMessage(notify, message);
				SetCrudMessage(crud);

				return View();
			}
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

			}
		}

		//[ClaimsAuthorize("Usuario", "Incluir")]
		[HttpPost]
		public async Task<ActionResult> Create(IFormCollection collection)
		{
			try
			{
				var command = new TextoLaudoModel.CreateUpdateTextoLaudoCommand
				{
					Classificacao = collection["classificacao"].ToString(),
					PontoInicial = Convert.ToInt32(collection["pontoInicial"].ToString()),
					PontoFinal = Convert.ToInt32(collection["pontoFinal"].ToString()),
					Aviso = Convert.ToInt32(collection["aviso"].ToString()),
					Texto = Convert.ToInt32(collection["texto"].ToString()),
				};

				await ApiClientFactory.Instance.CreateTextoLaudo(command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
			}
		}

		//[ClaimsAuthorize("Usuario", "Alterar")]
		public async Task<ActionResult> Edit(IFormCollection collection)
		{
			try
			{
				var command = new TextoLaudoModel.CreateUpdateTextoLaudoCommand
				{
					Id = Convert.ToInt32(collection["editTextoLaudoId"]),
					Classificacao = collection["classificacao"].ToString(),
					PontoInicial = Convert.ToInt32(collection["pontoInicial"].ToString()),
					PontoFinal = Convert.ToInt32(collection["pontoFinal"].ToString()),
					Aviso = Convert.ToInt32(collection["aviso"].ToString()),
					Texto = Convert.ToInt32(collection["texto"].ToString()),
				};

				await ApiClientFactory.Instance.UpdateTextoLaudo(command.Id, command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
			}
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteTextoLaudo(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public Task<TextoLaudoDto> GetTextoLaudoById(int id)
		{
			var result = ApiClientFactory.Instance.GetTextoLaudoById(id);

			return Task.FromResult(result);
		}
	}
}
