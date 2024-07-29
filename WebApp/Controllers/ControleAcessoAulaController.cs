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

public class ControleAcessoAulaController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    /// <param name="host">informações da aplicação em execução</param>
    public ControleAcessoAulaController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de ControleAcessoAula
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.ControleAcessoAula, Identity.Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetControlesAcessosAulasAll();

        return View(new ControleAcessoAulaModel() { ControlesAcessosAulas = response });
    }

    /// <summary>
    /// Tela para inclusão de ControleAcessoAula
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.ControleAcessoAula, Identity.Claim.Incluir)]
    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);



            return View(new ControleAcessoAulaModel()
            {
            });
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

        }
    }

    /// <summary>
    /// Ação de inclusão do ControleAcessoAula
    /// </summary>
    /// <param name="collection">coleção de dados para inclusao de ControleAcessoAula</param>
    /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.ControleAcessoAula, Identity.Claim.Incluir)]
    [HttpPost]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
            var command = new ControleAcessoAulaModel.CreateUpdateControleAcessoAulaCommand
            {
                AulaId = 0,
                TempoPermanecia = null,
                LiberacaoAula = null,
                DataLiberacao = null,
                DataEncerramento = null
            };

            await ApiClientFactory.Instance.CreateControleAcessoAula(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Tela Alteração de ControleAcessoAula
    /// </summary>
    /// <param name="id">Id de alteração do ControleAcessoAula</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    /// <exception cref="ArgumentNullException">Mensagem de erro ao alterar o ControleAcessoAula</exception>
    [ClaimsAuthorize(ClaimType.ControleAcessoAula, Claim.Alterar)]
    public ActionResult Edit(int id, int? crud, int? notify, string message = null)
    {
        try
        {

            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var ControleAcessoAula = ApiClientFactory.Instance.GetControleAcessoAulaById(id);


            return View(new ControleAcessoAulaModel()
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
    /// Ação de alteração do ControleAcessoAula
    /// </summary>
    /// <param name="id">identificador do ControleAcessoAula</param>
    /// <param name="collection">coleção de dados para alteração de ControleAcessoAula</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.ControleAcessoAula, Identity.Claim.Alterar)]
    public async Task<ActionResult> Edit(IFormCollection collection)
    {
        try
        {
            var command = new ControleAcessoAulaModel.CreateUpdateControleAcessoAulaCommand
            {
                Id = Convert.ToInt32(collection["editControleAcessoAulaId"]),

                Status = collection["editStatus"]
                             .ToString() ==
                         ""
                    ? false
                    : true,
                AulaId = 0,
                TempoPermanecia = null,
                LiberacaoAula = null,
                DataLiberacao = null,
                DataEncerramento = null
            };

            await ApiClientFactory.Instance.UpdateControleAcessoAula(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de exclusão do ControleAcessoAula
    /// </summary>
    /// <param name="id">identificador do ControleAcessoAula</param>
    /// <param name="collection">coleção de dados para exclusão de ControleAcessoAula</param>
    /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.ControleAcessoAula, Identity.Claim.Excluir)]
    public ActionResult Delete(int id)
    {
        try
        {
            ApiClientFactory.Instance.DeleteControleAcessoAula(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }
    #endregion

    #region Get Methods

    public Task<ControleAcessoAulaDto> GetControleAcessoAulaById(int id)
    {
        var result = ApiClientFactory.Instance.GetControleAcessoAulaById(id);

        return Task.FromResult(result);
    }
    #endregion
}