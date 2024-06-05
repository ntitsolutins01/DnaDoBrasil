using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers;

public class CursoController : BaseController
{
    private readonly IOptions<UrlSettings> _appSettings;

    public CursoController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }

    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        //var response = ApiClientFactory.Instance.GetCursosAll();

        return View();//new CursoModel() { Cursos = response }
    }

    ////[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
    //public ActionResult Create(int? crud, int? notify, string message = null)
    //{
    //    try
    //    {
    //        SetNotifyMessage(notify, message);
    //        SetCrudMessage(crud);
    //        var metricas = new SelectList(ApiClientFactory.Instance.GetCursosAll(), "Id", "Nome");

    //        return View(new CursoModel() { ListCursos = metricas });
    //    }
    //    catch (Exception e)
    //    {
    //        Console.Write(e.StackTrace);
    //        return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

    //    }
    //}

    ////[ClaimsAuthorize("Usuario", "Incluir")]
    //[HttpPost]
    //public async Task<ActionResult> Create(IFormCollection collection)
    //{
    //    try
    //    {
    //        var command = new CursoModel.CreateUpdateCursoCommand
    //        {
    //            Classificacao = collection["classificacao"].ToString(),
    //            Idade = Convert.ToInt32(collection["idade"].ToString()),
    //            ValorInicial = Convert.ToDecimal(collection["valorInicial"].ToString()),
    //            ValorFinal = Convert.ToDecimal(collection["valorFinal"].ToString()),
    //            Sexo = collection["ddlSexo"].ToString()
    //        };

    //        await ApiClientFactory.Instance.CreateCurso(command);

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
    //        var command = new CursoModel.CreateUpdateCursoCommand
    //        {
    //            Id = Convert.ToInt32(collection["editCursoId"]),
    //            Classificacao = collection["classificacao"].ToString(),
    //            Idade = Convert.ToInt32(collection["idade"].ToString()),
    //            ValorInicial = Convert.ToDecimal(collection["valorInicial"].ToString()),
    //            ValorFinal = Convert.ToDecimal(collection["valorFinal"].ToString()),
    //            Status = collection["editStatus"].ToString() == "" ? false : true
    //        };

    //        await ApiClientFactory.Instance.UpdateCurso(command.Id, command);

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
    //        ApiClientFactory.Instance.DeleteCurso(id);
    //        return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
    //    }
    //    catch
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //}

    //public Task<CursoDto> GetCursoById(int id)
    //{
    //    var result = ApiClientFactory.Instance.GetCursoById(id);

    //    return Task.FromResult(result);
    //}
}