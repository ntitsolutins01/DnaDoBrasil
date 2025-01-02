using Microsoft.AspNetCore.Authorization;
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

[Authorize(Policy = ModuloAccess.ConfiguracaoSistemaEad)]
public class TipoMaterialController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    /// <param name="host">informações da aplicação em execução</param>
    public TipoMaterialController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de TipoMaterial
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.TipoMaterial, Identity.Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetTiposMateriaisAll();

        return View(new TipoMaterialModel() { TiposMateriais = response });
    }

    /// <summary>
    /// Tela para inclusão de Modulo Ead
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.TipoMaterial, Identity.Claim.Incluir)]
    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var gruposMateriais = new SelectList(ApiClientFactory.Instance.GetGruposMateriaisAll(), "Id", "Nome");

            return View(new TipoMaterialModel()
            {
                ListGruposMateriais = gruposMateriais
            });
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

        }
    }

    /// <summary>
    /// Ação de inclusão do TipoMaterial
    /// </summary>
    /// <param name="collection">coleção de dados para inclusao de TipoMaterial</param>
    /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.TipoMaterial, Identity.Claim.Incluir)]
    [HttpPost]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
            var command = new TipoMaterialModel.CreateUpdateTipoMaterialCommand
            {
                GrupoMaterialId = Convert.ToInt32(collection["ddlGrupoMaterial"].ToString()),
                Nome = collection["nome"].ToString()
            };

            //foreach (var file in collection.Files)
            //{
            //    if (file.Length <= 0) continue;

            //    command.Imagem = Path.GetFileName(collection.Files[0].FileName);

            //    using (var ms = new MemoryStream())
            //    {
            //        file.CopyToAsync(ms);
            //        var byteIMage = ms.ToArray();
            //        command.ByteImage = byteIMage;
            //    }
            //}

            await ApiClientFactory.Instance.CreateTipoMaterial(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }


    /// <summary>
    /// Ação de alteração do TipoMaterial
    /// </summary>
    /// <param name="id">identificador do TipoMaterial</param>
    /// <param name="collection">coleção de dados para alteração de TipoMaterial</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.TipoMaterial, Identity.Claim.Alterar)]
    public async Task<ActionResult> Edit(IFormCollection collection)
    {
        try
        {
            var command = new TipoMaterialModel.CreateUpdateTipoMaterialCommand
            {
                Id = Convert.ToInt32(collection["editTipoMaterialId"]),
                Nome = collection["nome"].ToString(),
            };

            await ApiClientFactory.Instance.UpdateTipoMaterial(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de exclusão do TipoMaterial
    /// </summary>
    /// <param name="id">identificador do TipoMaterial</param>
    /// <param name="collection">coleção de dados para exclusão de TipoMaterial</param>
    /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.TipoMaterial, Identity.Claim.Excluir)]
    public ActionResult Delete(int id)
    {
        try
        {
            ApiClientFactory.Instance.DeleteTipoMaterial(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Este grupo de material não pode ser excluído pois possui aulas vinculadas a ele." });
        }
    }

    public Task<JsonResult> GetTiposMateriaisAllByGrupoMaterialId(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Tipo de Material não informado.");
            var resultLocal = ApiClientFactory.Instance.GetTiposMateriaisAllByGrupoMaterialId(Convert.ToInt32(id));

            return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Nome")));

        }
        catch (Exception ex)
        {
            return Task.FromResult(Json(ex.Message));
        }
    }
    #endregion

    #region Get Methods

    public Task<TipoMaterialDto> GetTipoMaterialById(int id)
    {
        var result = ApiClientFactory.Instance.GetTipoMaterialById(id);

        return Task.FromResult(result);
    }
    #endregion
}