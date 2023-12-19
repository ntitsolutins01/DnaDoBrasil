using Infraero.Relprev.CrossCutting.Enumerators;
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
	public class QuestionarioController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		public QuestionarioController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetQuestionarioAll();
			var model = new QuestionarioModel()
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
			var model = new QuestionarioModel()
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
				var command = new QuestionarioModel.CreateUpdateQuestionarioCommand
				{
					Pergunta = collection["pergunta"].ToString(),
					TipoLaudoId = Convert.ToInt32(collection["ddlTipoLaudo"].ToString()),

				};

				await ApiClientFactory.Instance.CreateQuestionario(command);

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
			try
			{
				var command = new QuestionarioModel.CreateUpdateQuestionarioCommand
				{
					Id = Convert.ToInt32(collection["editQuestionarioId"]),
					Pergunta = collection["pergunta"].ToString(),
				};

				await ApiClientFactory.Instance.UpdateQuestionario(command.Id, command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
			}
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });
			}
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteQuestionario(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });
			}
		}

		public async Task<QuestionarioDto> GetQuestionarioById(int id)
		{
			var result = ApiClientFactory.Instance.GetQuestionarioById(id);

			return result;
		}
		public async Task<JsonResult> GetQuestionariosByTipoLaudo(string id)
		{
			try
			{
				if (string.IsNullOrEmpty(id)) throw new Exception("Tipo de Laudo não informado.");
				var resultLocal = ApiClientFactory.Instance.GetQuestionarioByTipoLaudo(Convert.ToInt32(id));

				return Json(new SelectList(resultLocal, "Id", "Pergunta"));

			}
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
				return Json(e.Message);
			}
		}
	}
}
