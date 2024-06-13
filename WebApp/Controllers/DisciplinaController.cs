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
	public class DisciplinaController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		public DisciplinaController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetDisciplinasAll();

			return View(new DisciplinaModel() { Disciplinas = response });
		}

		//[ClaimsAuthorize("Disciplina", "Incluir")]
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
				var command = new DisciplinaModel.CreateUpdateDisciplinaCommand
				{
					Nome = collection["nome"].ToString(),
				};

				await ApiClientFactory.Instance.CreateDisciplina(command);

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
			var command = new DisciplinaModel.CreateUpdateDisciplinaCommand
			{
				Id = Convert.ToInt32(collection["editDisciplinaId"]),
				Nome = collection["nome"].ToString(),
			};

			await ApiClientFactory.Instance.UpdateDisciplina(command.Id, command);

			return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteDisciplina(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public Task<DisciplinaDto> GetDisciplinaById(int id)
		{
			var result = ApiClientFactory.Instance.GetDisciplinaById(id);

			return Task.FromResult(result);
		}
	}
}
