using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
	public classRespostaController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		publicRespostaController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetQuestionarioAll();
			var model = newRespostaModel()
			{
				Questionarios = response
			};

			return View(model);
		}

		//[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
		public ActionResult Create(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);

			var tiposlaudos = new SelectList(ApiClientFactory.Instance.GetTiposLaudoAll(), "Id", "Nome");
			var model = newRespostaModel()
			{
				ListTiposLaudos = tiposlaudos
			};
			return View(model);
		}

		//[ClaimsAuthorize("Usuario", "Incluir")]
		[HttpPost]
		public async Task<ActionResult> Create(IFormCollection collection)
		{
			try
			{
				var command = newRespostaModel.CreateUpdateQuestionarioCommand
				{
                    Pergunta = collection["pergunta"].ToString(),
                    TiposLaudo = Convert.ToInt32(collection["tiposlaudo"].ToString()),
					
				};

				await ApiClientFactory.Instance.CreateQuestionario(command);

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
			var command = newRespostaModel.CreateUpdateQuestionarioCommand
			{
				Id = Convert.ToInt32(collection["editQuestionarioId"]),
                Pergunta = collection["pergunta"].ToString(),
            };

			await ApiClientFactory.Instance.UpdateQuestionario(command.Id, command);

			return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteQuestionario(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public async Task<QuestionarioDto> GetQuestionarioById(int id)
		{
			var result = ApiClientFactory.Instance.GetQuestionarioById(id);

			return result;
		}
	}
}
