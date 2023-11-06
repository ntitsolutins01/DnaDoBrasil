using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
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
            //SetNotifyMessage(notify, message);
            //SetCrudMessage(crud);
            //var response = ApiClientFactory.Instance.GetGridUsuario();
            //return View(response);
            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var resultPerfil = ApiClientFactory.Instance.GetPerfilAll();

            var model = new UsuarioModel
            {
                ListPerfil = new SelectList(resultPerfil, "CodPerfil", "NomPerfil")
            };
            return View(model);
        }
    }
}
