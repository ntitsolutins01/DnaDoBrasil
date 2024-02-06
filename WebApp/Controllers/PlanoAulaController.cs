using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApp.Controllers
{
	public class PlanoAulaController : BaseController
	{

		private readonly IOptions<UrlSettings> _appSettings;
		private readonly IHostingEnvironment _host;

		public PlanoAulaController(IOptions<UrlSettings> appSettings,
			IHostingEnvironment host)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
			_host = host;
		}

		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetPlanosAulasAll();
			var modalidades = new SelectList(ApiClientFactory.Instance.GetAmbienteAll(), "Nome", "Nome");

			return View(new PlanoAulaModel() { PlanosAulas = response, ListModalidades = modalidades });
		}

		//[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
		public ActionResult Create(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);

			var modalidades = new SelectList(ApiClientFactory.Instance.GetAmbienteAll(), "Nome", "Nome");

			return View(new PlanoAulaModel() { ListModalidades = modalidades });
		}

		//[ClaimsAuthorize("Usuario", "Incluir")]
		[HttpPost]
		public async Task<ActionResult> Create(IFormCollection collection)
		{
			try
			{

				string filePath = null;

				foreach (var file in collection.Files)
				{
					if (file.Length <= 0) continue;
					var fileName = Path.GetFileName(collection.Files[0].FileName);
					filePath = Path.Combine(_host.WebRootPath, $"PlanosAulas/{fileName}");

					if (!Directory.Exists(Path.Combine(_host.WebRootPath, $"PlanosAulas")))
						Directory.CreateDirectory(Path.Combine(_host.WebRootPath, $"PlanosAulas"));

					using Stream fileStream = new FileStream(filePath, FileMode.Create);
					await file.CopyToAsync(fileStream);
				}

				var command = new PlanoAulaModel.CreateUpdatePlanoAulaCommand
				{
					Nome = collection["ddlPlanoAula"].ToString(),
					TipoEscolaridade = collection["ddlTipoEscolaridade"].ToString(),
					Modalidade = collection["ddlModalidade"].ToString(),
					Url = filePath
				};

				await ApiClientFactory.Instance.CreatePlanoAula(command);

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
			try
			{
				string filePath = null;

				foreach (var file in collection.Files)
				{
					if (file.Length <= 0) continue;
					var fileName = Path.GetFileName(collection.Files[0].FileName);
					filePath = Path.Combine(_host.WebRootPath, $"PlanosAulas/{fileName}");

					await using Stream fileStream = new FileStream(filePath, FileMode.Create);
					await file.CopyToAsync(fileStream);
				}

				var command = new PlanoAulaModel.CreateUpdatePlanoAulaCommand
				{
					Id = Convert.ToInt32(collection["editPlanoAulaId"]),
					Nome = collection["ddlPlanoAula"].ToString(),
					TipoEscolaridade = collection["ddlTipoEscolaridade"].ToString(),
					Modalidade = collection["ddlModalidade"].ToString(),
					Url = filePath,
					NomeArquivo = collection.Files[0].FileName
				};

				await ApiClientFactory.Instance.UpdatePlanoAula(command.Id, command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index));
			}
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(int id)
		{
			try
			{
                var result =  ApiClientFactory.Instance.GetPlanoAulaById(id);

                if (result != null)	
                {
                    ApiClientFactory.Instance.DeletePlanoAula(id);
                    System.IO.File.Delete(result.Url);
                }

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public Task<PlanoAulaDto> GetPlanoAulaById(int id)
		{
			var result = ApiClientFactory.Instance.GetPlanoAulaById(id);

			return Task.FromResult(result);
		}

		public ActionResult Download(int id)
		{
			var file = ApiClientFactory.Instance.GetPlanoAulaById(id);
			if (!System.IO.File.Exists(file.Url))
			{
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Warning, message = "Arquivo não encontrado." });
			}

			var fileBytes = System.IO.File.ReadAllBytes(file.Url);
			var response = new FileContentResult(fileBytes, "application/octet-stream")
			{
				FileDownloadName = file.NomeArquivo
			};
			return response;
		}
	}
}
