using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Authorization;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Identity;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers;

public class AulaController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    /// <param name="host">informações da aplicação em execução</param>
    public AulaController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de Aula
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Aula, Identity.Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetAulasAll();

        return View(new AulaModel() { Aulas = response });
    }

    /// <summary>
    /// Tela para inclusão de Aula
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Aula, Identity.Claim.Incluir)]
    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");



            return View(new AulaModel()
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

    /// <summary>
    /// Ação de inclusão do Aula
    /// </summary>
    /// <param name="collection">coleção de dados para inclusao de Aula</param>
    /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Aula, Identity.Claim.Incluir)]
    [HttpPost]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
            var command = new AulaModel.CreateUpdateAulaCommand
            {
               
            };

            await ApiClientFactory.Instance.CreateAula(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Tela Alteração de Aula
    /// </summary>
    /// <param name="id">Id de alteração do Aula</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    /// <exception cref="ArgumentNullException">Mensagem de erro ao alterar o Aula</exception>
    [ClaimsAuthorize(ClaimType.Aula, Claim.Alterar)]
    public ActionResult Edit(int id, int? crud, int? notify, string message = null)
    {
        try
        {

            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var Aula = ApiClientFactory.Instance.GetAulaById(id);
            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", Aula.Aluno.MunicipioEstado);
            var municipio = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", Aula.Aluno.MunicipioId);
            var localidade = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", Aula.Aluno.LocalidadeId);
            var disciplina = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", Aula.Aluno.DeficienciaId);


            return View(new AulaModel()
            {
                
             
            });
        }
        catch (Exception ex)
        {
            return RedirectToAction(nameof(Index),
                new
                {
                    notify = (int)EnumNotify.Error,
                    message = $"Erro ao alterar usuário. Favor entrar em contato com o administrador do sistema. {ex.Message}"
                });
        }
    }

    /// <summary>
    /// Ação de alteração do Aula
    /// </summary>
    /// <param name="id">identificador do Aula</param>
    /// <param name="collection">coleção de dados para alteração de Aula</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Aula, Identity.Claim.Alterar)]
    public async Task<ActionResult> Edit(IFormCollection collection)
    {
        try
        {
            var command = new AulaModel.CreateUpdateAulaCommand
            {
                Id = Convert.ToInt32(collection["editAulaId"]),
               
                Status = collection["editStatus"].ToString() == "" ? false : true
            };

            await ApiClientFactory.Instance.UpdateAula(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de exclusão do Aula
    /// </summary>
    /// <param name="id">identificador do Aula</param>
    /// <param name="collection">coleção de dados para exclusão de Aula</param>
    /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Aula, Identity.Claim.Excluir)]
    public ActionResult Delete(int id)
    {
        try
        {
            ApiClientFactory.Instance.DeleteAula(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }
    #endregion

    #region Get Methods

    public Task<AulaDto> GetAulaById(int id)
    {
        var result = ApiClientFactory.Instance.GetAulaById(id);

        return Task.FromResult(result);
    }
    #endregion
}