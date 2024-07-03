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

public class EventoController : BaseController
{
    #region Constructor
    private readonly IOptions<UrlSettings> _appSettings;

    /// <summary>
    /// Construtor da página
    /// </summary>
    /// <param name="app">configurações de urls do sistema</param>
    /// <param name="host">informações da aplicação em execução</param>
    public EventoController(IOptions<UrlSettings> appSettings)
    {
        _appSettings = appSettings;
        ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
    }
    #endregion

    #region Crud Methods

    /// <summary>
    /// Listagem de Evento
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Consultar)]
    public IActionResult Index(int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var response = ApiClientFactory.Instance.GetEventosAll();

		return View(new EventoModel() { Eventos = response });
    }

    /// <summary>
    /// Tela para inclusão de Evento
    /// </summary>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Incluir)]
    public ActionResult Create(int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");



            return View(new EventoModel()
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
    /// Ação de inclusão do Evento
    /// </summary>
    /// <param name="collection">coleção de dados para inclusao de Evento</param>
    /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Incluir)]
    [HttpPost]
    public async Task<ActionResult> Create(IFormCollection collection)
    {
        try
        {
            var command = new EventoModel.CreateUpdateEventoCommand
            {
                LocalidadeId = Convert.ToInt32(collection["ddlLocalidade"].ToString()),
                Titulo = collection["titulo"].ToString(),
                Descricao = collection["descricao"].ToString(),
                DataEvento = collection["dtEvento"].ToString(),
            };

            await ApiClientFactory.Instance.CreateEvento(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }


    /// <summary>
    /// Ação de alteração do Evento
    /// </summary>
    /// <param name="collection">coleção de dados para alteração de Evento</param>
    /// <returns>retorna mensagem de alteração através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Alterar)]
    public async Task<ActionResult> Edit(IFormCollection collection)
    {
        try
        {
            var command = new EventoModel.CreateUpdateEventoCommand
            {
                Id = Convert.ToInt32(collection["editEventoId"]),
				Titulo = collection["titulo"].ToString(),
				Descricao = collection["descricao"].ToString(),
				DataEvento = collection["dtEvento"].ToString(),
				Status = collection["editStatus"].ToString() == "" ? false : true
            };

            await ApiClientFactory.Instance.UpdateEvento(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
        }
    }

    /// <summary>
    /// Ação de exclusão do Evento
    /// </summary>
    /// <param name="id">identificador do Evento</param>
    /// <returns>retorna mensagem de exclusão através do parametro crud</returns>
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Excluir)]
    public ActionResult Delete(int id)
    {
        try
        {
            ApiClientFactory.Instance.DeleteEvento(id);
            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }
    #endregion

    #region Crud Controle Presenca Evento Methods

    /// <summary>
    /// Listagem de Controle de Presença de Evento
    /// </summary>
    /// <param name="eventoId">Id do Evento</param>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Consultar)]
    public IActionResult IndexControlePresenca(int eventoId, int? crud, int? notify, string message = null)
    {
        SetNotifyMessage(notify, message);
        SetCrudMessage(crud);
        var listControlePresenca = ApiClientFactory.Instance.GetControlesPresencasByEventoId(eventoId);
        var evento = ApiClientFactory.Instance.GetEventoById(eventoId);

        return View(new EventoModel() { ControlesPresencas = listControlePresenca, Evento = evento});
    }

    /// <summary>
    /// Tela para inclusão de Controle de Presença do Evento
    /// </summary>
    /// <param name="eventoId">Id do Evento </param>
    /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
    /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
    /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Incluir)]
    public ActionResult CreateControlePresenca(int eventoId, int? crud, int? notify, string message = null)
    {
        try
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var evento = ApiClientFactory.Instance.GetEventoById(eventoId);
            var convidado = ApiClientFactory.Instance.GetAlunosByFilter(new AlunosFilterDto()
                { LocalidadeId = evento.LocalidadeId, Nome = "Convidado" });

            return View(new EventoModel()
            {
                Evento = evento,
                Convidado = convidado.Result?.Alunos!.FirstOrDefault()
            });
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
            return RedirectToAction(nameof(IndexControlePresenca), new { eventoId = eventoId, notify = (int)EnumNotify.Error, message = e.Message });

        }
    }

    /// <summary>
    /// Ação para inclusão de Controle de Presença do Evento
    /// </summary>
    /// <param name="collection">coleção de dados para inclusao de Controle de Presença do Evento</param>
    /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
    [HttpPost]
    [ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Alterar)]
    public async Task<ActionResult> CreateControlePresenca(IFormCollection collection)
    {
	    try
	    {
		    var command = new ControlePresencaModel.CreateUpdateControlePresencaCommand()
		    {
			    EventoId = Convert.ToInt32(collection["eventoId"]),
				MunicipioId = collection["municipioId"] == "" ? null : Convert.ToInt32(collection["municipioId"].ToString()).ToString(),
				LocalidadeId = collection["localidadeId"] == "" ? null : Convert.ToInt32(collection["localidadeId"].ToString()),
				Controle = collection["controle"].ToString(),
				Justificativa = collection["convidado"].ToString(),
				AlunoId = collection["convidadoId"] == "" ? null : Convert.ToInt32(collection["convidadoId"].ToString()).ToString(),
			};

		    await ApiClientFactory.Instance.CreateControlePresenca(command);

		    return RedirectToAction(nameof(IndexControlePresenca), new { eventoId = command.EventoId, crud = (int)EnumCrud.Created });
	    }
	    catch (Exception e)
	    {
		    return RedirectToAction(nameof(IndexControlePresenca), new { eventoId = Convert.ToInt32(collection["eventoId"]), notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
	    }
    }

	/// <summary>
	/// Ação de alteração do Controle de Presença do Evento
	/// </summary>
	/// <param name="collection">coleção de dados para alteração de controle de presença do Evento</param>
	/// <returns>retorna mensagem de alteração através do parametro crud</returns>
	[ClaimsAuthorize(ClaimType.Evento, Identity.Claim.Alterar)]
	public async Task<ActionResult> EditControlePresenca(IFormCollection collection)
	{
		try
		{
			var command = new ControlePresencaModel.CreateUpdateControlePresencaCommand()
			{
				Id = Convert.ToInt32(collection["editControlePresencaId"]),
				Controle = collection["controle"].ToString(),
				Justificativa = collection["convidado"].ToString(),
				Status = collection["editStatus"].ToString() == "" ? false : true
			};

			await ApiClientFactory.Instance.UpdateControlePresenca(command.Id, command);

			return RedirectToAction(nameof(IndexControlePresenca), new { eventoId = command.EventoId, crud = (int)EnumCrud.Updated });
		}
		catch (Exception e)
		{
			return RedirectToAction(nameof(IndexControlePresenca), new { eventoId = Convert.ToInt32(collection["eventoId"]), notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema." });
		}
	}
	#endregion

	#region Get Methods
	/// <summary>
	/// Busca um evento por Id
	/// </summary>
	/// <param name="id">Id do evento</param>
	/// <returns>Entidade evento</returns>
	public Task<EventoDto> GetEventoById(int id)
    {
        var result = ApiClientFactory.Instance.GetEventoById(id);

        return Task.FromResult(result);
    }
    #endregion
}