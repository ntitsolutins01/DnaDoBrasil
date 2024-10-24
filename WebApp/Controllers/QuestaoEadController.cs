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
public class QuestaoEadController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    /// <param name="host">informações da aplicação em execução</param>
    public QuestaoEadController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de QuestaoEad
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    //[ClaimsAuthorize(ClaimType.QuestaoEad, Identity.Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetQuestaoEadAll();

        return View(new QuestaoEadModel() { QuestoesEad = response });
    }

    /// <summary>
    /// Tela para inclusão de Modulo Ead
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.QuestaoEad, Identity.Claim.Incluir)]
    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var modulosEad = new SelectList(ApiClientFactory.Instance.GetModulosEadAll(), "Id", "Nome");
            var tipoCurso = new SelectList(ApiClientFactory.Instance.GetTipoCursosAll(), "Id", "Nome");
            //var curso = new SelectList(ApiClientFactory.Instance.GetCursosAll(), "Id", "Nome");


            return View(new QuestaoEadModel());
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

        }
    }

    ///// <summary>
    ///// Ação de inclusão do QuestaoEad
    ///// </summary>
    ///// <param name="collection">coleção de dados para inclusao de QuestaoEad</param>
    ///// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    //[ClaimsAuthorize(ClaimType.QuestaoEad, Identity.Claim.Incluir)]
    //[HttpPost]
    //public async Task<ActionResult> Create(IFormCollection collection)
    //{
    //    try
    //    {
    //        var command = new QuestaoEadModel.CreateUpdateQuestaoEadCommand
    //        {
    //            CursoId = Convert.ToInt32(collection["ddlCurso"].ToString()),
    //            Titulo = collection["nome"].ToString(),
    //            Descricao = collection["descricao"].ToString(),
    //            CargaHoraria = Convert.ToInt32(collection["cargaHoraria"].ToString())
    //        };

    //        //foreach (var file in collection.Files)
    //        //{
    //        //    if (file.Length <= 0) continue;

    //        //    command.Imagem = Path.GetFileName(collection.Files[0].FileName);

    //        //    using (var ms = new MemoryStream())
    //        //    {
    //        //        file.CopyToAsync(ms);
    //        //        var byteIMage = ms.ToArray();
    //        //        command.ByteImage = byteIMage;
    //        //    }
    //        //}

    //        await ApiClientFactory.Instance.CreateQuestaoEad(command);

    //        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
    //    }
    //    catch (Exception e)
    //    {
    //        return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
    //    }
    //}


    ///// <summary>
    ///// Ação de alteração do QuestaoEad
    ///// </summary>
    ///// <param name="id">identificador do QuestaoEad</param>
    ///// <param name="collection">coleção de dados para alteração de QuestaoEad</param>
    ///// <returns>retorna mensagem de alteração através do parametro crud</returns>
    //[ClaimsAuthorize(ClaimType.QuestaoEad, Identity.Claim.Alterar)]
    //public async Task<ActionResult> Edit(IFormCollection collection)
    //{
    //    try
    //    {
    //        var command = new QuestaoEadModel.CreateUpdateQuestaoEadCommand
    //        {
    //            Id = Convert.ToInt32(collection["editQuestaoEadId"]),
    //            Titulo = collection["nome"].ToString(),
    //            Descricao = collection["descricao"].ToString(),
    //            CargaHoraria = Convert.ToInt32(collection["cargaHoraria"].ToString()),
    //            Status = collection["editStatus"].ToString() == "" ? false : true
    //        };

    //        //foreach (var file in collection.Files)
    //        //{
    //        //    if (file.Length <= 0) continue;

    //        //    command.Imagem = Path.GetFileName(collection.Files[0].FileName);

    //        //    using (var ms = new MemoryStream())
    //        //    {
    //        //        file.CopyToAsync(ms);
    //        //        var byteIMage = ms.ToArray();
    //        //        command.ByteImage = byteIMage;
    //        //    }
    //        //}

    //        await ApiClientFactory.Instance.UpdateQuestaoEad(command.Id, command);

    //        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
    //    }
    //    catch (Exception e)
    //    {
    //        return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
    //    }
    //}

    /// <summary>
    /// Ação de exclusão do QuestaoEad
    /// </summary>
    /// <param name="id">identificador do QuestaoEad</param>
    /// <param name="collection">coleção de dados para exclusão de QuestaoEad</param>
    /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.QuestaoEad, Identity.Claim.Excluir)]
    public ActionResult Delete(int id)
    {
        try
        {
            ApiClientFactory.Instance.DeleteQuestaoEad(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Este módulo não pode ser excluído pois possui aulas vinculadas a ele." });
        }
    }

    public Task<JsonResult> GetCursosAllByTipoCursoId(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Tipo de Curso não informado.");
            var resultLocal = ApiClientFactory.Instance.GetCursosAllByTipoCursoId(Convert.ToInt32(id));

            return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Titulo")));

        }
        catch (Exception ex)
        {
            return Task.FromResult(Json(ex.Message));
        }
    }

    public Task<JsonResult> GetModulosEadAllByCursoId(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Curso não informado.");
            var resultLocal = ApiClientFactory.Instance.GetModulosEadAllByCursoId(Convert.ToInt32(id));

            return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Titulo")));

        }
        catch (Exception ex)
        {
            return Task.FromResult(Json(ex.Message));
        }
    }
    #endregion

    #region Get Methods

    public Task<QuestaoEadDto> GetQuestaoEadById(int id)
    {
        var result = ApiClientFactory.Instance.GetQuestaoEadById(id);

        return Task.FromResult(result);
    }
    #endregion
}