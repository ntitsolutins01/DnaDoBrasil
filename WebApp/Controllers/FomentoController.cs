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

        public ActionResult Edit(int id, int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var fomento = ApiClientFactory.Instance.GetFomentoById(id);

            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", fomento.Sigla);
			var municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(fomento.Sigla), "Id", "Nome", fomento.MunicipioId);
			var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeByMunicipio(fomento.MunicipioId.ToString()), "Id", "Nome", fomento.LocalidadeId);
			var linhasAcoes = new SelectList(ApiClientFactory.Instance.GetLinhasAcoesAll(), "Id", "Nome", fomento.LinhaAcoes);

            var model = new FomentoModel
            {
                ListEstados = estados,
                ListLinhasAcoes = linhasAcoes,
                ListMunicipios = municipios,
                ListLocalidades = localidades,
                Fomento = fomento

            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            var command = new FomentoModel.CreateUpdateFomentoCommand
            {
                Id = id,
                Codigo = collection["codigo"].ToString(),
                Nome = collection["Nome"].ToString(),
                DtIni = collection["dtIni"].ToString(),
                DtFim = collection["dtFim"].ToString(),
                LinhaAcoes = collection["ddlLinhaAcao"].ToString(),
                LocalidadeId = Convert.ToInt32(collection["ddlLocalidade"].ToString()),
                MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                Status = collection["status"].ToString() == "" ? false : true
            };

            await ApiClientFactory.Instance.UpdateFomento(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

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
