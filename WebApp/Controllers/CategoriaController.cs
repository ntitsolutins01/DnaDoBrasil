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
/// Controller de Evento
/// </summary>
[Authorize(Policy = ModuloAccess.ConfiguracaoSistema)]
public class CategoriaController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    public CategoriaController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de Categoria
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Categoria, Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetCategoriasAll();

        return View(new CategoriaModel() { Categorias = response });
    }

    /// <summary>
    /// Tela para inclusão de Categoria
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Categoria, Claim.Incluir)]
    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            return View(new CategoriaModel());
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });
        }
    }

    /// <summary>
    /// Ação de inclusão do Categoria
    /// </summary>
    /// <param name="collection">coleção de dados para inclusao de Categoria</param>
    /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Categoria, Claim.Incluir)]
    [HttpPost]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
            var command = new CategoriaModel.CreateUpdateCategoriaCommand
            {
                Codigo = collection["codigo"].ToString(),
                Nome = collection["nome"].ToString(),
                IdadeFinal = Convert.ToInt32(collection["idadeFinal"]),
                IdadeInicial = Convert.ToInt32(collection["idadeInicial"]),
                Descricao = collection["descricao"].ToString(),
            };

            await ApiClientFactory.Instance.CreateCategoria(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de alteração do Categoria
    /// </summary>
    /// <param name="id">identificador do Categoria</param>
    /// <param name="collection">coleção de dados para alteração de Categoria</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Categoria, Claim.Alterar)]
    public async Task<ActionResult> Edit(IFormCollection collection)
    {
        try
        {
            var command = new CategoriaModel.CreateUpdateCategoriaCommand
            {
                Id = Convert.ToInt32(collection["editCategoriaId"]),
				Codigo = collection["codigo"].ToString(),
				Nome = collection["nome"].ToString(),
				IdadeFinal = Convert.ToInt32(collection["idadeFinal"]),
				IdadeInicial = Convert.ToInt32(collection["idadeInicial"]),
				Descricao = collection["descricao"].ToString(),
				Status = collection["editStatus"].ToString() == "" ? false : true,
            };

            await ApiClientFactory.Instance.UpdateCategoria(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de exclusão do Categoria
    /// </summary>
    /// <param name="id">identificador do Categoria</param>
    /// <param name="collection">coleção de dados para exclusão de Categoria</param>
    /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Categoria, Claim.Excluir)]
    public ActionResult Delete(int id)
    {
        try
        {
            //if (ApiClientFactory.Instance.GetCategoriaById(id).Any())
            //{
            //	return RedirectToAction(nameof(Index), new { CategoriaId = id, notify = (int)EnumNotify.Error, message = "O Categoria não pode ser excluído pois existem presenças registradas para o mesmo." });
            //}
            ApiClientFactory.Instance.DeleteCategoria(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }
    #endregion

    #region Get Methods

    public Task<CategoriaDto> GetCategoriaById(int id)
    {
        var result = ApiClientFactory.Instance.GetCategoriaById(id);

        return Task.FromResult(result);
    }
    #endregion
}
