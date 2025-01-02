using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApp.Authorization;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Identity;
using WebApp.Models;
using WebApp.Utility;
using Claim = WebApp.Identity.Claim;

namespace WebApp.Controllers;

/// <summary>
/// Controller de Atividade
/// </summary>
[Authorize(Policy = ModuloAccess.Atividade)]
public class AtividadeController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    public AtividadeController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de Atividade
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Atividade, Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetAtividadesAll();

        return View(new AtividadeModel() { Atividades = response });
    }

    /// <summary>
    /// Tela para inclusão de Atividade
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Atividade, Claim.Incluir)]
    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

            var linhasAcoes = new SelectList(ApiClientFactory.Instance.GetLinhasAcoesAll(), "Id", "Nome");

            var categorias = new SelectList(ApiClientFactory.Instance.GetCategoriasAll(), "Id", "Nome");

            var model = new AtividadeModel()
            {
                ListEstados = estados,
                ListLinhasAcoes = linhasAcoes,
                ListCategorias = categorias
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
    /// Ação de inclusão do Atividade
    /// </summary>
    /// <param name="collection">coleção de dados para inclusao de Atividade</param>
    /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Atividade, Claim.Incluir)]
    [HttpPost]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
            var command = new AtividadeModel.CreateUpdateAtividadeCommand
            {
                EstruturaId = 0,
                LinhaAcaoId = 0,
                CategoriaId = 0,
                ModalidadeId = 0,
                ProfissionalId = 0,
                LocalidadeId = 0
            };

            await ApiClientFactory.Instance.CreateAtividade(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de alteração do Atividade
    /// </summary>
    /// <param name="id">identificador do Atividade</param>
    /// <param name="collection">coleção de dados para alteração de Atividade</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Atividade, Claim.Alterar)]
    public async Task<ActionResult> Edit(IFormCollection collection)
    {
        try
        {
            var command = new AtividadeModel.CreateUpdateAtividadeCommand
            {
                Id = Convert.ToInt32(collection["editAtividadeId"]),
                //Codigo = collection["codigo"]
                //    .ToString(),
                //Nome = collection["nome"]
                //    .ToString(),
                //IdadeFinal = Convert.ToInt32(collection["idadeFinal"]),
                //IdadeInicial = Convert.ToInt32(collection["idadeInicial"]),
                //Descricao = collection["descricao"]
                //    .ToString(),
                Status = collection["editStatus"]
                             .ToString() ==
                         ""
                    ? false
                    : true,
                EstruturaId = 0,
                LinhaAcaoId = 0,
                CategoriaId = 0,
                ModalidadeId = 0,
                ProfissionalId = 0,
                LocalidadeId = 0,
            };

            await ApiClientFactory.Instance.UpdateAtividade(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de exclusão do Atividade
    /// </summary>
    /// <param name="id">identificador do Atividade</param>
    /// <param name="collection">coleção de dados para exclusão de Atividade</param>
    /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Atividade, Claim.Excluir)]
    public ActionResult Delete(int id)
    {
        try
        {
            //if (ApiClientFactory.Instance.GetAtividadeById(id).Any())
            //{
            //	return RedirectToAction(nameof(Index), new { AtividadeId = id, notify = (int)EnumNotify.Error, message = "O Atividade não pode ser excluído pois existem presenças registradas para o mesmo." });
            //}
            ApiClientFactory.Instance.DeleteAtividade(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }
    #endregion

    #region Get Methods

    public Task<AtividadeDto> GetAtividadeById(int id)
    {
        var result = ApiClientFactory.Instance.GetAtividadeById(id);

        return Task.FromResult(result);
    }
    #endregion
}
