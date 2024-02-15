using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

		        var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

		        var questionarioVocacional =
			        ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.Vocacional);
		        var questionarioQualidadeVida =
			        ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.QualidadeVida);
		        var questionarioConsumoAlimentar =
			        ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.ConsumoAlimentar);
		        var questionarioSaudeBucal =
			        ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.SaudeBucal);


		        return View(new LaudoModel()
		        {
			        QuestionarioVocacional = questionarioVocacional,
			        QuestionarioQualidadeVida = questionarioQualidadeVida,
			        QuestionarioConsumoAlimentar = questionarioConsumoAlimentar,
			        QuestionarioSaudeBucal = questionarioSaudeBucal,
					ListEstados = estados
		        });

	        }
	        catch (Exception e)
	        {
		        Console.Write(e.StackTrace);
		        return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
	            var listVocacisonal= new List<string>();

	            foreach (var item in collection)	
	            {
		            if (item.Key.Contains("nomeRespVocacional"))
		            {
			            listVocacisonal.Add(item.Value);
		            }
	            }

	            var listQualidadeDeVida = new List<string>();

	            foreach (var item in collection)
	            {
		            if (item.Key.Contains("nomeRespQualidadeVida"))
		            {
			            listQualidadeDeVida.Add(item.Value);
		            }
	            }
	            var listConsumoAlimentar = new List<string>();

	            foreach (var item in collection)
	            {
		            if (item.Key.Contains("nomeRespConsumoAlimentar"))
		            {
			            listConsumoAlimentar.Add(item.Value);
		            }
	            }
	            var listSaudeBucal = new List<string>();

	            foreach (var item in collection)
	            {
		            if (item.Key.Contains("nomeRespSaudeBucal"))
		            {
						listSaudeBucal.Add(item.Value);
		            }
	            }
	           
				var command = new LaudoModel.CreateUpdateLaudoCommand()
                {
                    ImpulsaoHorizontal = Convert.ToDecimal(collection["impulsaoHorizontal"].ToString()),
                    Flexibilidade = Convert.ToDecimal(collection["flexibilidade"].ToString()),
                    PreensaoManual = Convert.ToDecimal(collection["preensaoManual"].ToString()),
                    Velocidade = Convert.ToDecimal(collection["testeVelocidade"].ToString()),
                    AptidaoFisica = Convert.ToDecimal(collection["aptidaoFisica"].ToString()),
                    Agilidade = Convert.ToDecimal(collection["agilidade"].ToString()),
                    Abdominal = Convert.ToDecimal(collection["abdominal"].ToString()),
                    Altura = Convert.ToDecimal(collection["altura"].ToString()),
                    AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString()),
                    EnvergaduraSaude = Convert.ToInt32(collection["envergaduraSaude"].ToString()),
                    MassaCorporalSaude = Convert.ToInt32(collection["massaCorporalSaude"].ToString()),
                    AlturaSaude = Convert.ToInt32(collection["alturaSaude"].ToString()),
                    ListVocacional = listVocacisonal.ToArray(),
                    listQualidadeDeVida = listQualidadeDeVida.ToArray(),
                    listConsumoAlimentar = listConsumoAlimentar.ToArray(),
                    listSaudeBucal = listSaudeBucal.ToArray(),
                };

                await ApiClientFactory.Instance.CreateLaudo(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }
		//[ClaimsAuthorize("Usuario", "Alterar")]
		[HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
			try
			{
				var listVocacisonal = new List<string>();

				foreach (var item in collection)
				{
					if (item.Key.Contains("nomeRespVocacional"))
					{
						listVocacisonal.Add(item.Value);
					}
				}

				var listQualidadeDeVida = new List<string>();

				foreach (var item in collection)
				{
					if (item.Key.Contains("nomeRespQualidadeVida"))
					{
						listQualidadeDeVida.Add(item.Value);
					}
				}
				var listConsumoAlimentar = new List<string>();

				foreach (var item in collection)
				{
					if (item.Key.Contains("nomeRespConsumoAlimentar"))
					{
						listConsumoAlimentar.Add(item.Value);
					}
				}
				var listSaudeBucal = new List<string>();

				foreach (var item in collection)
				{
					if (item.Key.Contains("nomeRespSaudeBucal"))
					{
						listSaudeBucal.Add(item.Value);
					}
				}

				var command = new LaudoModel.CreateUpdateLaudoCommand
				{
					ImpulsaoHorizontal = Convert.ToDecimal(collection["impulsaoHorizontal"].ToString()),
					Flexibilidade = Convert.ToDecimal(collection["flexibilidade"].ToString()),
					PreensaoManual = Convert.ToDecimal(collection["preensaoManual"].ToString()),
					Velocidade = Convert.ToDecimal(collection["testeVelocidade"].ToString()),
					AptidaoFisica = Convert.ToDecimal(collection["aptidaoFisica"].ToString()),
					Agilidade = Convert.ToDecimal(collection["agilidade"].ToString()),
					Abdominal = Convert.ToDecimal(collection["abdominal"].ToString()),
					Altura = Convert.ToDecimal(collection["altura"].ToString()),
					AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString()),
					EnvergaduraSaude = Convert.ToInt32(collection["envergaduraSaude"].ToString()),
					MassaCorporalSaude = Convert.ToInt32(collection["massaCorporalSaude"].ToString()),
					AlturaSaude = Convert.ToInt32(collection["alturaSaude"].ToString()),
					ListVocacional = listVocacisonal.ToArray(),
					listQualidadeDeVida = listQualidadeDeVida.ToArray(),
					listConsumoAlimentar = listConsumoAlimentar.ToArray(),
					listSaudeBucal = listSaudeBucal.ToArray(),
				};

				await ApiClientFactory.Instance.UpdateLaudo(command.Id, command);

				return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
			}
			catch (Exception e)
			{
				return RedirectToAction(nameof(Index));
			}
        }

        public ActionResult Edit(int id, int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);
                var laudo = ApiClientFactory.Instance.GetLaudoById(id);

                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

                var questionarioVocacional =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.Vocacional);
                var questionarioQualidadeVida =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.QualidadeVida);
                var questionarioConsumoAlimentar =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.ConsumoAlimentar);
                var questionarioSaudeBucal =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.SaudeBucal);


                return View(new LaudoModel()
                {
                    QuestionarioVocacional = questionarioVocacional,
                    QuestionarioQualidadeVida = questionarioQualidadeVida,
                    QuestionarioConsumoAlimentar = questionarioConsumoAlimentar,
                    QuestionarioSaudeBucal = questionarioSaudeBucal,
                    ListEstados = estados,
					Laudo = laudo
                });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }




        }


    }
}
