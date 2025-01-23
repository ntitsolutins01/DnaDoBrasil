using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        #region Parametros

        private readonly IOptions<UrlSettings> _appSettings;
        private readonly IWebHostEnvironment _host;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Constructor

        /// <summary>
        /// Contrutor da página
        /// </summary>
        /// <param name="appSettings">Configurações da aplicação</param>
        /// <param name="host">Informação do ambiente em que a aplicação está rodando</param>
        public CertificadoController(IOptions<UrlSettings> appSettings, IWebHostEnvironment host)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
            _host = host;
        }
        #endregion

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
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);
                var tipoCurso = new SelectList(ApiClientFactory.Instance.GetTipoCursosAll(), "Id", "Nome");

                return View(new CertificadoModel()
                {
                    ListTipoCursos = tipoCurso
                });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new CertificadoModel.CreateUpdateCertificadoCommand
                {
                    CursoId = Convert.ToInt32(collection["ddlCurso"].ToString()),
                    HtmlFrente = collection["HtmlFrente"].ToString(),
                    HtmlVerso = collection["HtmlVerso"].ToString(),
                    Status = collection["Status"].ToString().ToLower() == "on"
                };

                // Imagem da Frente
                string? filePathFrente;
                string? fileNameFrente;
                string extension0 = ".jpg";
                string newFileName0 = Path.ChangeExtension(
                    Guid.NewGuid().ToString(),
                    extension0
                );

                foreach (var file in collection.Files)
                {
                    if (file.Length <= 0) continue;
                    fileNameFrente = Path.GetFileName(collection.Files[0].FileName);
                    filePathFrente = Path.Combine(_host.WebRootPath, $"Certificados\\{newFileName0}");

                    if (!Directory.Exists(Path.Combine(_host.WebRootPath, $"Certificados")))
                        Directory.CreateDirectory(Path.Combine(_host.WebRootPath, $"Certificados"));

                    command.ImagemFrente = fileNameFrente;
                    command.NomeImagemFrente = filePathFrente;

                    using Stream fileStream = new FileStream(filePathFrente, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }

                // Imagem do verso
                string? filePathVerso;
                string? fileNameVerso;
                string extension1 = ".jpg";
                string newFileName1 = Path.ChangeExtension(
                    Guid.NewGuid().ToString(),
                    extension1
                );

                foreach (var file in collection.Files)
                {
                    if (file.Length <= 0) continue;
                    fileNameVerso = Path.GetFileName(collection.Files[0].FileName);
                    filePathVerso = Path.Combine(_host.WebRootPath, $"Certificados\\{newFileName1}");

                    if (!Directory.Exists(Path.Combine(_host.WebRootPath, $"Certificados")))
                        Directory.CreateDirectory(Path.Combine(_host.WebRootPath, $"Certificados"));

                    command.ImagemVerso = fileNameVerso;
                    command.NomeImagemVerso = filePathVerso;

                    using Stream fileStream = new FileStream(filePathVerso, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }

                await ApiClientFactory.Instance.CreateCertificado(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });
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
                ImagemFrente = collection["ImagemFrente"].ToString(),
                ImagemVerso = collection["ImagemVerso"].ToString(),
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

        public JsonResult GetCursosByTipoCursoId(int id)
        {
            var cursos = ApiClientFactory.Instance.GetCursosAllByTipoCursoId(id);
            return Json(cursos);
        }
    }
}
