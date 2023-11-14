using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{

	public class SistemaSocioeconomicoController : BaseController
	{
		private readonly IOptions<SettingsModel> _appSettings;

		public SistemaSocioeconomicoController(IOptions<SettingsModel> appSettings)
		{
			_appSettings = appSettings;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}

		public IActionResult Parceiro(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			//var response = ApiClientFactory.Instance.GetParceiroAll();

			//return View(new ParceiroModel(){Parceiros = response});
			return View();
		}
		
		//[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
		public ActionResult CreateParceiro(int? crud, int? notify, string message = null)
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
				var command = new ParceiroModel.CreateUpdateParceiroCommand
				{

					Nome = collection["nome"].ToString(),
					TipoPessoa = collection["TipoPessoa"].ToString(),
					CpfCnpj = collection["cpfCnpj"].ToString(),
					Telefone = collection["Telefone"].ToString(),
					Celular = collection["Celular"].ToString(),
					Endereco = collection["Endereço"].ToString(),
					Numero = Convert.ToInt32(collection["numero"].ToString()),
					Bairro = collection["Bairro"].ToString(),
					Habilitado = collection["habilitado"].ToString() == "1"? true:false, //TODO: Verificar condicional para resgate radio button
					Status = collection["status"].ToString() == "1"? true:false,
					Email = collection["Email"].ToString(),
					TipoParceria = Convert.ToInt32(collection["TipoParceria"].ToString()), //TODO: Verificar condicional para resgate checkbox
				};

				await ApiClientFactory.Instance.CreateParceiro(command);

				return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Created });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Parceiro));
			}
		}
		//[ClaimsAuthorize("Parceiro", "Alterar")]
		//public ActionResult Edit(string id)
		//{
		//    var obj = ApiClientFactory.Instance.GetParceiroById(id);

		//    var model = new ParceiroModel() { Parceiro = obj };

		//    return View(model);
		//}
		//[ClaimsAuthorize("Usuario", "Alterar")]
		public async Task<ActionResult> Edit(string id, IFormCollection collection)
		{
			var command = new ParceiroModel.CreateUpdateParceiroCommand
			{
				Id = Convert.ToInt32(id),
				Nome = collection["nome"].ToString(),
				TipoPessoa = collection["TipoPessoa"].ToString(),
				CpfCnpj = collection["cpfCnpj"].ToString(),
				Telefone = collection["Telefone"].ToString(),
				Celular = collection["Celular"].ToString(),
				Endereco = collection["Endereço"].ToString(),
				Numero = Convert.ToInt32(collection["numero"].ToString()),
				Bairro = collection["Bairro"].ToString(),
				Habilitado = collection["habilitado"].ToString() == "1" ? true : false, //TODO: Verificar condicional para resgate radio button
				Status = collection["status"].ToString() == "1" ? true : false,
				Email = collection["Email"].ToString(),
				TipoParceria = Convert.ToInt32(collection["TipoParceria"].ToString()), //TODO: Verificar condicional para resgate checkbox
			};

			//await ApiClientFactory.Instance.UpdateParceiro(command);

			return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Updated });
		}

		//[ClaimsAuthorize("Usuario", "Excluir")]
		public ActionResult Delete(string id)
		{
			try
			{
				//ApiClientFactory.Instance.DeleteParceiro(id);
				return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Deleted });
			}
			catch
			{
				return RedirectToAction(nameof(Parceiro));
			}
		}


















		public IActionResult Estudantes()
		{
			return View();
		}
		public IActionResult SolicitacaoContato()
		{
			return View();
		}
		public IActionResult Details()
		{
			return View();
		}
	}
}
