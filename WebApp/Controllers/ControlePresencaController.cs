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
	public class ControlePresencaController : BaseController
	{
		private readonly IOptions<UrlSettings> _appSettings;

		public ControlePresencaController(IOptions<UrlSettings> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

        public ActionResult Index(int? crud, int? notify, IFormCollection collection, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var searchFilter = new ControlesPresencasFilterDto()
                {
                    FomentoId = collection["ddlFomento"].ToString(),
                    Estado = collection["ddlEstado"].ToString(),
                    MunicipioId = collection["ddlMunicipio"].ToString(),
                    LocalidadeId = collection["ddlLocalidade"].ToString(),
                    DeficienciaId = collection["ddlDeficiencia"].ToString(),
                    Etnia = collection["ddlEtnia"].ToString()
                };

                var result = ApiClientFactory.Instance.GetControlesPresencasByFilter(searchFilter);

                var fomentos = new SelectList(ApiClientFactory.Instance.GetFomentoAll(), "Id", "Nome", searchFilter.FomentoId);
                var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll(), "Id", "Nome", searchFilter.DeficienciaId);
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", searchFilter.Estado);

                List<SelectListDto> list = new List<SelectListDto>
                {
                    new() { IdNome = "PARDO", Nome = "PARDO" },
                    new() { IdNome = "BRANCO", Nome = "BRANCO" },
                    new() { IdNome = "PRETO", Nome = "PRETO" },
                    new() { IdNome = "INDÍGENA", Nome = "INDÍGENA" },
                    new() { IdNome = "AMARELO", Nome = "AMARELO" }
                };

                var etnias = new SelectList(list, "IdNome", "Nome", searchFilter.Etnia);
                SelectList municipios = null;

                if (!string.IsNullOrEmpty(searchFilter.Estado))
                {
                    municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(searchFilter.Estado), "Id", "Nome", searchFilter.MunicipioId);
                }
                SelectList localidades = null;

                if (!string.IsNullOrEmpty(searchFilter.LocalidadeId))
                {
                    localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeByMunicipio(searchFilter.MunicipioId), "Id", "Nome", searchFilter.LocalidadeId);
                }

                var model = new ControlePresencaModel()
                {
                    ListFomentos = fomentos,
                    ListEstados = estados,
                    ListDeficiencias = deficiencias,
                    ListMunicipios = municipios!,
                    ListEtnias = etnias,
                    ListLocalidades = localidades!,
                    ControlesPresencas = result.ControlesPresencas

                };
                return View(model);

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

		//[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
		public ActionResult Create(int? crud, int? notify, string message = null)
		{
			try
			{
				SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");


			return View(new ControlePresencaModel()
			{
				ListEstados = estados
			});

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
				var command = new ControlePresencaModel.CreateUpdateControlePresencaCommand
				{
					MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()).ToString(),
					LocalidadeId = collection["ddlLocalidade"] == "" ? null : Convert.ToInt32(collection["ddlLocalidade"].ToString()),
					Controle = collection["controle"].ToString(),
					Justificativa = collection["justificativa"].ToString(),
					AlunoId = collection["ddlAluno"] == "" ? null : Convert.ToInt32(collection["ddlAluno"].ToString()).ToString(),
				};

				await ApiClientFactory.Instance.CreateControlePresenca(command);

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
				var command = new ControlePresencaModel.CreateUpdateControlePresencaCommand
				{
					Id = Convert.ToInt32(collection["editControlePresencaId"]),
					Controle = collection["controle"].ToString(),
					Justificativa = collection["justificativa"].ToString()
				};

				await ApiClientFactory.Instance.UpdateControlePresenca(command.Id, command);

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
