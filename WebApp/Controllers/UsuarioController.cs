using Infraero.Relprev.CrossCutting.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using WebApp.Areas.Identity.Models;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApp.Controllers
{
	public class UsuarioController : BaseController
	{
		private readonly IOptions<SettingsModel> _appSettings;
		private readonly IEmailSender _emailSender;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IHostingEnvironment _host;

		public UsuarioController(IOptions<SettingsModel> app, IEmailSender emailSender,
			UserManager<IdentityUser> userManager, IHostingEnvironment host)
		{
			_appSettings = app;
			_emailSender = emailSender;
			_userManager = userManager;
			_host = host;
			ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
		}
		//[ClaimsAuthorize("Usuario", "Consultar")]
		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var response = ApiClientFactory.Instance.GetUsuarioAll();
			return View(response);
		}

		//[ClaimsAuthorize("Usuario", "Incluir")]
		public ActionResult Create(int? crud, int? notify, string message = null)
		{
			SetNotifyMessage(notify, message);
			SetCrudMessage(crud);
			var resultPerfil = ApiClientFactory.Instance.GetPerfilAll();

			var model = new UsuarioModel
			{
				ListPerfil = new SelectList(resultPerfil, "Id", "Nome")
			};
			return View(model);
		}

		//[ClaimsAuthorize("Usuario", "Incluir")]
		public async Task<ActionResult> Create(IFormCollection collection)
		{
			try
			{
				var command = new UsuarioModel.CreateUpdateUsuarioCommand
				{

					Email = collection["EndEmail"].ToString(),
					Nome = collection["NomUsuario"].ToString(),
					Cpf = collection["cpf"].ToString(),
					Telefone = collection["NumTelefone"].ToString(),
					PerfilId = int.Parse(collection["ddlAssunto"].ToString()),
					AspNetUserId = 0,
					AspNetRoleId = ""
				};

				var result = ApiClientFactory.Instance.GetUsuarioByCpf(command.Cpf);

				if (result != null)
				{
					return RedirectToAction(nameof(Create),
						new
						{
							notify = (int)EnumNotify.Error,
							message = "Já existe um usuário cadastrado com esse cpf."
						});

				}

				var result2 = ApiClientFactory.Instance.GetUsuarioByEmail(command.Email.Trim());

				if (result2 != null)
				{
					return RedirectToAction(nameof(Create), new { notify = (int)EnumNotify.Error, message = "Já existe usuários com o E-mail informado cadastrado na base de dados!" });

				}

				var obj = ApiClientFactory.Instance.GetPerfilById(command.PerfilId);

				ApiClientFactory.Instance.CreateUsuario(command);

				var user = await _userManager.FindByEmailAsync(command.Email);

				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "Usuário não cadastrado.");
					return View();
				}
				SendNewUserEmail(user, command.Email, command.Nome);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index));
			}
		}

		private async Task SendNewUserEmail(IdentityUser user, string email, string nomeUsuario)
		{
			var code = await _userManager.GeneratePasswordResetTokenAsync(new IdentityUser(user.Email));

			var callbackUrl = Url.ActionLink("ResetPassword",
				"Identity/Account", new { code, email });

			var message =
				System.IO.File.ReadAllText(Path.Combine(_host.WebRootPath, "emailtemplates/ConfirmEmail.html"));
			message = message.Replace("%NAME%", nomeUsuario);
			message = message.Replace("%CALLBACK%", HtmlEncoder.Default.Encode(callbackUrl.Replace("%2FAccount", "/Account")));

			await _emailSender.SendEmailAsync(user.Email, "Primeiro acesso sistema Dna",
				message);
		}
	}
}
