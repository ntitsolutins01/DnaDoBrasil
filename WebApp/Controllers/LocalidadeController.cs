﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class LocalidadeController : BaseController
    {
	    private readonly IOptions<UrlSettings> _appSettings;

	    public LocalidadeController(IOptions<UrlSettings> appSettings)
	    {
		    _appSettings = appSettings;
		    ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
	    }

		public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetLocalidadeAll();

            return View(new LocalidadeModel() { Localidades = response});
		}

        //[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

            return View(new LocalidadeModel() { ListEstados = estados });
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new LocalidadeModel.CreateUpdateLocalidadeCommand
                {
                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
					MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString())
                };

                await ApiClientFactory.Instance.CreateLocalidade(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(IFormCollection collection)
        {
            var command = new LocalidadeModel.CreateUpdateLocalidadeCommand
            {
                Id = Convert.ToInt32(collection["editLocalidadeId"]),
                Nome = collection["nome"].ToString(),
                Descricao = collection["descricao"].ToString(),
                MunicipioId = Convert.ToInt32(collection["editMunicipioId"].ToString()),
                Status = collection["editStatus"].ToString() == "" ? false : true
			};

            await ApiClientFactory.Instance.UpdateLocalidade(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteLocalidade(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação {e.Message}" });
            }
        }

        public Task<LocalidadeDto> GetLocalidadeById(int id)
        {
            var result = ApiClientFactory.Instance.GetLocalidadeById(id);

            return Task.FromResult(result);
        }

        public Task<JsonResult> GetLocalidadeByMunicipio(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Município não informado.");
                var resultLocal = ApiClientFactory.Instance.GetLocalidadeByMunicipio(id);

                return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Nome")));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex));
            }
        }

        public Task<JsonResult> GetLocalidadeByFomento(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Município não informado.");
                var resultLocal = ApiClientFactory.Instance.GetLocalidadeByFomento(Convert.ToInt32(id));

                return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Nome")));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex));
            }
        }

        public Task<JsonResult> GetProfissionaisByLocalidade(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Localidadee não informada.");
                var resultLocal = ApiClientFactory.Instance.GetProfissionaisByLocalidade(Convert.ToInt32(id));

                return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Nome")));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex));
            }
        }
    }
}
