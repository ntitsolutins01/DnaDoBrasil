﻿using Infraero.Relprev.CrossCutting.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Identity.Models;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class ContratoController : BaseController
	{
		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			//SetNotifyMessage(notify, message);
			//SetCrudMessage(crud);
			//var response = ApiClientFactory.Instance.GetContratoAll();

			//return View(new ContratoModel(){Contratos = response});
			return View();
		}

		//[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
		public ActionResult Create(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);

			return View();
		}

		//[ClaimsAuthorize("Usuario", "Incluir")]
		[HttpPost]
		public async Task<ActionResult> Create(IFormCollection collection)
		{
			try
			{
				var command = new ContratoModel.CreateUpdateContratoCommand
				{
					Nome = collection["nome"].ToString().ToUpper(),
					Descricao = collection["descricao"].ToString(),
					DtIni = Convert.ToDateTime(collection["dtini"].ToString()),
					DtFim = Convert.ToDateTime(collection["dtfim"].ToString()),
					Anexo = collection["anexo"].ToString()
				};

				await ApiClientFactory.Instance.CreateContrato(command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index));
			}
		}

		//[ClaimsAuthorize("Usuario", "Alterar")]
		public async Task<ActionResult> Edit(string id, IFormCollection collection)
		{
			var command = new ContratoModel.CreateUpdateContratoCommand
			{
				Id = Convert.ToInt32(id),
				Nome = collection["nome"].ToString(), 
				Descricao = collection["descricao"].ToString(),
				DtIni = Convert.ToDateTime(collection["dtini"].ToString()),
				DtFim = Convert.ToDateTime(collection["dtfim"].ToString()),
				Anexo = collection["anexo"].ToString()
			};

			await ApiClientFactory.Instance.UpdateContrato(command);

			return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(string id)
		{
			try
			{
				ApiClientFactory.Instance.DeleteContrato(id);
				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
