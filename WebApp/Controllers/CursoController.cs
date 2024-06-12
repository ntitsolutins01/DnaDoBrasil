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

public class CursoController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    /// <param name="host">informações da aplicação em execução</param>
    public CursoController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods
    /// <summary>
    /// Listagem de Curso
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Curso, Identity.Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetCursosAll();

        return View(new CursoModel() { Cursos = response });
    }

    /// <summary>
    /// Tela para inclusão de aluno
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Curso, Identity.Claim.Incluir)]

    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var Curso = new SelectList(ApiClientFactory.Instance.GetCursosAll(), "Id", "Nome");
            var tiposcursos = new SelectList(ApiClientFactory.Instance.GetTipoCursosAll(), "Id", "Nome");
            var coordenadores = new SelectList(ApiClientFactory.Instance.GetCoordenadoresAll(), "Id", "Nome");


            return View(new CursoModel()
            {
                Listtiposcursos = tiposcursos,
                ListCoordenadores = coordenadores
            }); 
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

        }
    }
    #endregion

    #region Get Methods

    //[ClaimsAuthorize("Usuario", "Incluir")]
    [HttpPost]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
	        string filePath = null;

			var command = new CursoModel.CreateUpdateCursoCommand
            {
				Nome = collection["nome"].ToString(),
				NomeFoto = filePath,
                TipoCursoId = Convert.ToInt32(collection["ddlTipoCurso"].ToString()),
                CoordenadorId = Convert.ToInt32(collection["ddlCoordenador"].ToString()),
            };

            await ApiClientFactory.Instance.CreateCurso(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    //[ClaimsAuthorize("Usuario", "Alterar")]
    public async Task<ActionResult> Edit(IFormCollection collection)
    {
        try
        {
	        string filePath = null;

			var command = new CursoModel.CreateUpdateCursoCommand
            {
                Id = Convert.ToInt32(collection["editCursoId"]),
				Nome = collection["nome"].ToString(),
				NomeFoto = filePath,
                TipoCursoId = Convert.ToInt32(collection["ddlTipoCurso"].ToString()),
                CoordenadorId = Convert.ToInt32(collection["ddlCoordenador"].ToString()),
                Status = collection["editStatus"].ToString() == "" ? false : true
            };

            await ApiClientFactory.Instance.UpdateCurso(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }
    /// <summary>
    /// Ação de upload de foto do aluno
    /// </summary>
    /// <param name="collection">arquivo de upload realizado</param>
    /// <returns>retorna mensagem de upload realizado através do parametro notfy e message</returns>
    [HttpPost]
    [ClaimsAuthorize(ClaimType.Curso, Claim.Upload)]
    public async Task<ActionResult> Upload(IFormCollection collection)
    {
	    try
	    {
		    string filePath = null;

		    var curso = ApiClientFactory.Instance.GetCursoById(Convert.ToInt32(collection["cursoId"]));

		    var command = new CursoModel.CreateUpdateCursoCommand
			{
			    Id = Convert.ToInt32(collection["editCursoId"])
		    };

		    foreach (var file in collection.Files)
		    {
			    if (file.Length <= 0) continue;

			    command.NomeFoto = Path.GetFileName(collection.Files[0].FileName);

			    using var ms = new MemoryStream();
			    await file.CopyToAsync(ms);
			    var byteIMage = ms.ToArray();
			    command.ByteImage = byteIMage;
		    }

		    await ApiClientFactory.Instance.UpdateCursoFoto(command.Id, command);

		    return RedirectToAction(nameof(Index), new { notify = EnumNotify.Success, mesage = "Upload realizado com sucesso." });
	    }
	    catch (Exception e)
	    {
		    Console.Write(e.StackTrace);
		    return RedirectToAction(nameof(Index), new { notify = EnumNotify.Error, mesage = e.Message });
	    }
    }

	//[ClaimsAuthorize("Usuario", "Excluir")]
	public ActionResult Delete(int id)
    {
        try
        {
            ApiClientFactory.Instance.DeleteCurso(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }

    public Task<CursoDto> GetCursoById(int id)
    {
        var result = ApiClientFactory.Instance.GetCursoById(id);

        return Task.FromResult(result);
    } 
    #endregion
}