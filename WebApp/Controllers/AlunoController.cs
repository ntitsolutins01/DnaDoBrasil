using Microsoft.AspNetCore.Mvc;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class AlunoController : BaseController
    {
        public ActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            //var deficiencias = ApiClientFactory.Instance.GetDeficienciasAll();

            //return View(new AlunoModel { Deficiencias = deficiencias });
            return View();
        }
        public IActionResult Create()
		{
			return View();
		}
		public IActionResult Laudo()
		{
			return View();
		}
		public IActionResult TalentoEsportivo()
		{

			return View();
		}

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateTalentoEsportivo(IFormCollection collection)
        {
            try
            {
                var command = new TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand
                {
                    Flexibilidade = Convert.ToInt32(collection["flexibilidade"]),
                    PreensaoManual = Convert.ToInt32(collection["preensaoManual"]),
                    Velocidade = Convert.ToInt32(collection["velocidade"]),
                    ImpulsaoHorizontal = Convert.ToInt32(collection["impulsaoHorizontal"]),
                    AptidaoFisica = Convert.ToInt32(collection["aptdaoFisica"]),
                    Agilidade = Convert.ToInt32(collection["agilidade"]),
                    Abdominal = Convert.ToInt32(collection["abdominal"])
                };

                await ApiClientFactory.Instance.CreateTalentoEsportivo(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> EditTalentoEsportivo(string id, IFormCollection collection)
        {
            var command = new TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand
            {
                Id = Convert.ToInt32(id),
                Flexibilidade = Convert.ToInt32(collection["flexibilidade"]),
                PreensaoManual = Convert.ToInt32(collection["preensaoManual"]),
                Velocidade = Convert.ToInt32(collection["velocidade"]),
                ImpulsaoHorizontal = Convert.ToInt32(collection["impulsaoHorizontal"]),
                AptidaoFisica = Convert.ToInt32(collection["aptdaoFisica"]),
                Agilidade = Convert.ToInt32(collection["agilidade"]),
                Abdominal = Convert.ToInt32(collection["abdominal"])
        };

            await ApiClientFactory.Instance.UpdateTalentoEsportivo(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }
        public IActionResult Vocacional()
		{
			return View();
		}

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateVocacional(IFormCollection collection)
        {
            try
            {
                var command = new VocacionalModel.CreateUpdateVocacionalCommand
                {
                    ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                    QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                    Resposta = Convert.ToString(collection["profissionalId"])
                };

                await ApiClientFactory.Instance.CreateVocacional(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> EditVocacional(string id, IFormCollection collection)
        {
            var command = new VocacionalModel.CreateUpdateVocacionalCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["profissionalId"])

            };

            await ApiClientFactory.Instance.UpdateVocacional(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }



        public IActionResult QualidadeVida()
		{
			return View();
		}
		public IActionResult Saude()
		{
			return View();
		}
		public IActionResult ConsumoAlimentar()
		{
			return View();
		}
		public IActionResult SaudeBucal()
		{
			return View();
		}
	}
}
