using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Identity;
using WebApp.Models;
using WebApp.Utility;
using Claim = WebApp.Identity.Claim;
using WebApp.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Policy = ModuloAccess.ControlePresenca)]
    public class ControlePresencaController : BaseController
	{
        #region Constructor
        /// <summary>
        /// Construtor da página
        /// </summary>
        /// <param name="appSettings">configurações de url da api</param>
        public ControlePresencaController(IOptions<UrlSettings> appSettings)
		{
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
		}

        #endregion


        #region Crud Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crud"></param>
        /// <param name="notify"></param>
        /// <param name="collection"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Consultar)]
        public async Task<ActionResult> Index(int? crud, int? notify, IFormCollection collection, string message = null)
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

                var result = await ApiClientFactory.Instance.GetControlesPresencasByFilter(searchFilter);

                var fomentos = new SelectList(ApiClientFactory.Instance.GetFomentoAll(), "Id", "Nome", searchFilter.FomentoId);
                var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll().Where(x => x.Status), "Id", "Nome", searchFilter.DeficienciaId);
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", searchFilter.Estado);

                List<SelectListDto> list = new List<SelectListDto>
                {
                    new() { IdNome = "PARDO", Nome = "PARDO" },
                    new() { IdNome = "BRANCO", Nome = "BRANCO" },
                    new() { IdNome = "PRETO", Nome = "PRETO" },
                    new() { IdNome = "INDIGENA", Nome = "INDIGENA" },
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


        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Incluir)]
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

        [HttpPost]
        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Incluir)]
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

                var possuiPrecensa = ApiClientFactory.Instance.GetControlePresencaByAlunoId(Convert.ToInt32(command.AlunoId)).Where(x=>x.Data == DateTime.Now.ToString("dd/MM/yyyy"));

                if (possuiPrecensa.Any())
                {
                    return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Warning, message = "Já existe presença cadastrada para este aluno no dia de hoje." });
                }
                await ApiClientFactory.Instance.CreateControlePresenca(command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
			}
		}

        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Alterar)]
        public async Task<ActionResult> Edit(IFormCollection collection)
		{
			try
			{
				var command = new ControlePresencaModel.CreateUpdateControlePresencaCommand
				{
					Id = Convert.ToInt32(collection["editControlePresencaId"]),
					Controle = collection["controle"].ToString(),
					Justificativa = collection["convidado"].ToString()
				};

				await ApiClientFactory.Instance.UpdateControlePresenca(command.Id, command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
			}
		}

        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Excluir)]
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

        #endregion

        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Consultar)]
        public Task<ControlePresencaDto> GetControlePresencaById(int id)
		{
			var result = ApiClientFactory.Instance.GetControlePresencaById(id);

			return Task.FromResult(result);
		}
	}
}
