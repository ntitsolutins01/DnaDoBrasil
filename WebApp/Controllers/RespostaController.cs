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
	public class RespostaController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		public RespostaController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetRespostaAll();

			var model = new RespostaModel()
			{
                Respostas = response
			};

			return View(model);
		}

		//[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
		public ActionResult Create(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);

			var tiposlaudos = new SelectList(ApiClientFactory.Instance.GetTiposLaudoAll(), "Id", "Nome");
			var model = new RespostaModel()
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
				var command = new RespostaModel.CreateUpdateRespostaCommand()
				{
                    RespostaQuestionario = collection["resposta"].ToString(),
                    QuestionarioId = Convert.ToInt32(collection["ddlQuestionario"].ToString()),
					
				};

				await ApiClientFactory.Instance.CreateResposta(command);

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
			var command = new RespostaModel.CreateUpdateRespostaCommand
			{
				Id = Convert.ToInt32(collection["editQuestionarioId"]),
                RespostaQuestionario = collection["resposta"].ToString(),
            };

			await ApiClientFactory.Instance.UpdateResposta(command.Id, command);

			return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteResposta(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public async Task<RespostaDto> GetRespostaById(int id)
		{
			var result = ApiClientFactory.Instance.GetRespostaById(id);

			return result;
		}

        public async Task<JsonResult> GetQuestionarioByTipoLaudo(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Tipo de Laudo não informado.");
                var resultLocal = ApiClientFactory.Instance.GetQuestionarioByTipoLaudo(Convert.ToInt32(id));

                return Json(new SelectList(resultLocal, "Id", "Pergunto"));

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
