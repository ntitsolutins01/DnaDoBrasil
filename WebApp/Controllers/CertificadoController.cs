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
using Claim = System.Security.Claims.Claim;

namespace WebApp.Controllers;

public class CertificadoController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    /// <param name="host">informações da aplicação em execução</param>
    public CertificadoController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de Certificado
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Certificado, Identity.Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        //var response = ApiClientFactory.Instance.GetCertificadosAll();

        return View();//new CertificadoModel() { Certificados = response }
    }

    /// <summary>
    /// Tela para inclusão de aluno
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Certificado, Identity.Claim.Incluir)]

    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
           SetCrudMessage(crud);
            //var metricas = new SelectList(ApiClientFactory.Instance.GetCertificadosAll(), "Id", "Nome");

            return View(); //(new CertificadoModel() { ListCertificados = metricas });
        }
        catch (Exception e)
        {
         Console.Write(e.StackTrace);
           return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

        }
    }
    #endregion

    ////[ClaimsAuthorize("Usuario", "Incluir")]
    //[HttpPost]
    //public async Task<ActionResult> Create(IFormCollection collection)
    //{
    //    try
    //    {
    //        var command = new CertificadoModel.CreateUpdateCertificadoCommand
    //        {
    //            Classificacao = collection["classificacao"].ToString(),
    //            Idade = Convert.ToInt32(collection["idade"].ToString()),
    //            ValorInicial = Convert.ToDecimal(collection["valorInicial"].ToString()),
    //            ValorFinal = Convert.ToDecimal(collection["valorFinal"].ToString()),
    //            Sexo = collection["ddlSexo"].ToString()
    //        };

    //        await ApiClientFactory.Instance.CreateCertificado(command);

    //        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
    //    }
    //    catch (Exception e)
    //    {
    //        return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
    //    }
    //}

    ////[ClaimsAuthorize("Usuario", "Alterar")]
    //public async Task<ActionResult> Edit(IFormCollection collection)
    //{
    //    try
    //    {
    //        var command = new CertificadoModel.CreateUpdateCertificadoCommand
    //        {
    //            Id = Convert.ToInt32(collection["editCertificadoId"]),
    //            Classificacao = collection["classificacao"].ToString(),
    //            Idade = Convert.ToInt32(collection["idade"].ToString()),
    //            ValorInicial = Convert.ToDecimal(collection["valorInicial"].ToString()),
    //            ValorFinal = Convert.ToDecimal(collection["valorFinal"].ToString()),
    //            Status = collection["editStatus"].ToString() == "" ? false : true
    //        };

    //        await ApiClientFactory.Instance.UpdateCertificado(command.Id, command);

    //        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
    //    }
    //    catch (Exception e)
    //    {
    //        return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
    //    }
    //}

    ////[ClaimsAuthorize("Usuario", "Excluir")]
    //public ActionResult Delete(int id)
    //{
    //    try
    //    {
    //        ApiClientFactory.Instance.DeleteCertificado(id);
    //        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
    //    }
    //    catch
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //}

    //public Task<CertificadoDto> GetCertificadoById(int id)
    //{
    //    var result = ApiClientFactory.Instance.GetCertificadoById(id);

    //    return Task.FromResult(result);
    //}
}