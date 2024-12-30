using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class CertificadoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public CertificadoController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            ViewBag.Status = true;
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetCertificadosAll() ?? new List<CertificadoDto>();

            return View(new CertificadoModel()
            {
                Certificados = response
            });
        }

        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            return View(new CertificadoModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new CertificadoModel.CreateUpdateCertificadoCommand
                {
                    CursoId = Convert.ToInt32(collection["CursoId"].ToString()),
                    ImagemFrente = Convert.FromBase64String(collection["ImagemFrente"].ToString()),
                    ImagemVerso = string.IsNullOrEmpty(collection["ImagemVerso"]) ? null : Convert.FromBase64String(collection["ImagemVerso"].ToString()),
                    HtmlFrente = collection["HtmlFrente"].ToString(),
                    HtmlVerso = collection["HtmlVerso"].ToString(),
                    Status = collection["Status"].ToString() == "" ? false : true
                };

                await ApiClientFactory.Instance.CreateCertificado(command);

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

            var certificado = ApiClientFactory.Instance.GetCertificadoById(id);

            var model = new CertificadoModel
            {
                Certificado = certificado
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            var command = new CertificadoModel.CreateUpdateCertificadoCommand
            {
                Id = id,
                CursoId = Convert.ToInt32(collection["CursoId"].ToString()),
                ImagemFrente = Convert.FromBase64String(collection["ImagemFrente"].ToString()),
                ImagemVerso = string.IsNullOrEmpty(collection["ImagemVerso"]) ? null : Convert.FromBase64String(collection["ImagemVerso"].ToString()),
                HtmlFrente = collection["HtmlFrente"].ToString(),
                HtmlVerso = collection["HtmlVerso"].ToString(),
                Status = collection["Status"].ToString() == "" ? false : true
            };

            await ApiClientFactory.Instance.UpdateCertificado(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteCertificado(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public Task<CertificadoDto> GetCertificadoById(int id)
        {
            var result = ApiClientFactory.Instance.GetCertificadoById(id);

            return Task.FromResult(result);
        }
    }
}


