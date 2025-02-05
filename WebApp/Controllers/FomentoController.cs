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
    public class FomentoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public FomentoController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {

            ViewBag.Status = true;
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetFomentoAll();
            var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeAll(), "Id", "Nome");
            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

            return View(new FomentoModel()
            {
                Fomentos = response,
                ListLocalidades = localidades,
                ListEstados = estados
            });
        }

        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");
            var linhasAcoes = new SelectList(ApiClientFactory.Instance.GetLinhasAcoesAll(), "Id", "Nome");

            var model = new FomentoModel
            {
                ListEstados = estados,
                ListLinhasAcoes = linhasAcoes

            };
            return View(model);
        }
        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new FomentoModel.CreateUpdateFomentoCommand
                {
                    LocalidadeId = Convert.ToInt32(collection["ddlLocalidade"].ToString()),
                    MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    Codigo = collection["codigo"].ToString(),
                    DtIni = collection["dtIni"].ToString(),
                    DtFim = collection["dtFim"].ToString(),
					Nome = collection["Nome"].ToString(),
					LinhaAcoes = collection["ddlLinhaAcao"].ToString()
				};

                await ApiClientFactory.Instance.CreateFomento(command);

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
            var command = new FomentoModel.CreateUpdateFomentoCommand
            {
                Id = Convert.ToInt32(collection["editFomentoId"]),
                Codigo = collection["codigo"].ToString(),
                Nome = collection["Nome"].ToString(),
                DtIni = collection["dtIni"].ToString(),
                DtFim = collection["dtFim"].ToString(),
				Status = collection["editStatus"].ToString() == "" ? false : true
			};

            await ApiClientFactory.Instance.UpdateFomento(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteFomento(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public Task<FomentoDto> GetFomentoById(int id)
        {
            var result = ApiClientFactory.Instance.GetFomentoById(id);
            
            return Task.FromResult(result);
        }


        public Task<JsonResult> GetMunicipioIdByFomento(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Fomento não informado.");
                var fomento = ApiClientFactory.Instance.GetFomentoById(Convert.ToInt32(id));

                return Task.FromResult(Json(fomento.MunicipioId));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex));
            }
        }
    }
}
