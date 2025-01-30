using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    /// <summary>
    /// Controle de Presença
    /// </summary>
    [Authorize(Policy = ModuloAccess.ControlePresenca)]
    public class ControlePresencaController : BaseController
	{
        #region Constructor

        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Construtor da página
        /// </summary>
        /// <param name="appSettings">configurações de url da api</param>
        /// <param name="userManager">gerenciador de identidade de usuários</param>
        public ControlePresencaController(IOptions<UrlSettings> appSettings, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        #endregion


        #region Main Methods

        /// <summary>
        /// Listagem de Controle de Presença 
        /// </summary>
        /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
        /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
        /// <param name="collection">parametro que indica o tipo de notificação realizada</param>
        /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
        /// <returns>returs true false</returns>
        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Consultar)]
        public async Task<ActionResult> Index(int? crud, int? notify, IFormCollection collection, string message = null)
        {
            try
            {
                var usuario = User.Identity.Name;

                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var usu = ApiClientFactory.Instance.GetUsuarioByEmail(usuario);


                var fomentos = new SelectList(ApiClientFactory.Instance.GetFomentoAll(), "Id", "Nome");
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", usu.Uf);

                SelectList municipios = null;

                if (!string.IsNullOrEmpty(usu.Uf))
                {
                    municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(usu.Uf), "Id", "Nome", usu.MunicipioId);
                }

                SelectList localidades = null;

                if (usu.MunicipioId != null)
                {
                    var resultLocalidades = ApiClientFactory.Instance.GetLocalidadeByMunicipio(usu.MunicipioId.ToString());

                    localidades = new SelectList(resultLocalidades, "Id", "Nome", usu.LocalidadeId);
                }

                SelectList alunos = null;

                if (usu.LocalidadeId != null)
                {
                    var resultAlunos = ApiClientFactory.Instance.GetAlunosByLocalidade(Convert.ToInt32(usu.LocalidadeId));

                    alunos =  new SelectList(resultAlunos, "Id", "Nome");
                }

                var searchFilter = new ControlesPresencasFilterDto()
                {
                    UsuarioEmail = usuario,
                    FomentoId = collection["ddlFomento"].ToString(),
                    Estado = collection["ddlEstado"].ToString(),
                    MunicipioId = collection["ddlMunicipio"].ToString(),
                    LocalidadeId = collection["ddlLocalidade"].ToString() == "" ? usu.LocalidadeId : collection["ddlLocalidade"].ToString(),

                    PageNumber = 1,
#if DEBUG
                    PageSize = 10
#else
                    PageSize = 1000
#endif
                };

                var response = await ApiClientFactory.Instance.GetControlesPresencasByFilter(searchFilter);

                var model = new ControlePresencaModel()
                {
                    ListFomentos = fomentos,
                    ListEstados = estados,
                    ListMunicipios = municipios!,
                    ListLocalidades = localidades!,
                    ListAlunos = alunos,
                    ControlesPresencas = response.ControlesPresencas

                };
                return View(model);

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        /// <summary>
        /// Tela para Inclusão de Controle de Presença
        /// </summary>
        /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
        /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
        /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
        /// <returns>returns true love </returns>
        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Incluir)]
        public ActionResult Create(int? crud, int? notify, string message = null)
		{
			try
			{
				SetNotifyMessage(notify, message);
			    SetCrudMessage(crud);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.Identity == null) return Redirect("/Identity/Account/Login");
                var usuario = User.Identity.Name;

                if (usuario == null) return Redirect("/Identity/Account/Login");
                var usu = ApiClientFactory.Instance.GetUsuarioByEmail(usuario);

                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", usu.Uf);

                SelectList municipios = null;

                if (!string.IsNullOrEmpty(usu.Uf))
                {
                    municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(usu.Uf), "Id", "Nome", usu.MunicipioId);
                }

                SelectList localidades = null;

                if (usu.MunicipioId != null)
                {
                    var resultLocalidades = ApiClientFactory.Instance.GetLocalidadeByMunicipio(usu.MunicipioId.ToString());

                    localidades = new SelectList(resultLocalidades, "Id", "Nome", usu.LocalidadeId);
                }

                SelectList alunos = null;

                if (usu.LocalidadeId == null)
                    return View(new ControlePresencaModel()
                    {
                        ListEstados = estados,
                        ListMunicipios = municipios!,
                        ListLocalidades = localidades!,
                        ListAlunos = alunos,
                    });
                var resultAlunos = ApiClientFactory.Instance.GetAlunosByLocalidade(Convert.ToInt32(usu.LocalidadeId));

                alunos = new SelectList(resultAlunos, "Id", "Nome");

                return View(new ControlePresencaModel()
                {
                    ListEstados = estados,
                    ListMunicipios = municipios!,
                    ListLocalidades = localidades!,
                    ListAlunos = alunos,
                });

            }
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

			}
		}

        /// <summary>
        /// Ação de Inclusão de Controle de Presença
        /// </summary>
        /// <param name="collection">coleção de dados para Inclusao de Controle de Presença</param>
        /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
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

                var possuiPrecensa = ApiClientFactory.Instance.GetControlePresencaByAlunoId(Convert.ToInt32(command.AlunoId))
                    .Where(x=>x.Data == DateTime.Now.ToString("dd/MM/yyyy") && x.EventoId == null);

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

        /// <summary>
        /// Ação de Alteração de Controle de Presença 
        /// </summary>
        /// <param name="collection">coleção de dados para alteração de Controle de Presença</param>
        /// <returns>retorna mensagem de alteração através do parametro crud</returns>
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

        /// <summary>
        /// Ação de Exclusão de Controle de Presença 
        /// </summary>
        /// <param name="id">identificador do Controle de Categoria</param>
        /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
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

        #region Get Methods

        /// <summary>
        /// Busca Controle de Presença  por Id
        /// </summary>
        /// <param name="id">Identificador de Controle de Presença</param>
        /// <returns>Retorna a Categoria</returns>
        [ClaimsAuthorize(ClaimType.ControlePresenca, Claim.Consultar)]
        public Task<ControlePresencaDto> GetControlePresencaById(int id)
        {
            var result = ApiClientFactory.Instance.GetControlePresencaById(id);

            return Task.FromResult(result);
        }

        #endregion

    }
}
