using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class LaudoController : BaseController
    {
	    private readonly IOptions<UrlSettings> _appSettings;

	    public LaudoController(IOptions<UrlSettings> appSettings)
	    {
		    _appSettings = appSettings;
		    ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
	    }
		public IActionResult Index(int? crud, int? notify, string message = null)
		{
			try
			{
				SetNotifyMessage(notify, message);
				SetCrudMessage(crud);
				var response = ApiClientFactory.Instance.GetLaudoAll();

				var model = new LaudoModel()
				{
					Laudos = response
				};

				return View(model);
			}
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
				return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

			}
		}

		public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
	        try
	        {
		        SetNotifyMessage(notify, message);
		        SetCrudMessage(crud);

		        var questionarioVocacional =
			        ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.Vocacional);

		        return View(new LaudoModel() { QuestionarioVocacional = questionarioVocacional });

	        }
	        catch (Exception e)
	        {
		        Console.Write(e.StackTrace);
		        return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

	        }
        }


	}
}
