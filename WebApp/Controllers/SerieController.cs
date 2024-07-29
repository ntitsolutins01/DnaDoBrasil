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
	public class SerieController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		public SerieController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetSerieAll();

			return View(new SerieModel() { Series = response });
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
				var command = new SerieModel.CreateUpdateSerieCommand
				{
					Nome = collection["nome"].ToString(),
					Descricao = collection["descricao"].ToString(),
					IdadeInicial = Convert.ToInt32(collection["idadeIni"].ToString()),
					IdadeFinal = Convert.ToInt32(collection["idadeFim"].ToString()),
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

		//[ClaimsAuthorize("Usuario", "Alterar")]
		public async Task<ActionResult> Edit(IFormCollection collection)
		{
			var command = new SerieModel.CreateUpdateSerieCommand
			{
				Id = Convert.ToInt32(collection["editSerieId"]),
				Nome = collection["nome"].ToString(),
				Status = collection["editStatus"].ToString() == "" ? false : true,
				Descricao = collection["descricao"].ToString(),
				IdadeInicial = Convert.ToInt32(collection["idadeIni"].ToString()),
				IdadeFinal = Convert.ToInt32(collection["idadeFim"].ToString()),
				ScoreTotal = Convert.ToInt32(collection["scoreTotal"].ToString())
			};

			await ApiClientFactory.Instance.UpdateSerie(command.Id, command);

			return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteSerie(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public Task<SerieDto> GetSerieById(int id)
		{
			var result = ApiClientFactory.Instance.GetSerieById(id);

			return Task.FromResult(result);
		}
	}
}
