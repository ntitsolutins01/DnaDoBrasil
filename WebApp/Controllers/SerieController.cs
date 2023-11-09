using Infraero.Relprev.CrossCutting.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Identity.Models;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class SerieController : BaseController
    {
        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetSerieAll();

            return View(new SerieModel(){Series = response});
        }

        //[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new SerieModel.CreateUpdateSerieCommand
                {

                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
                    IdadeInicial = Convert.ToInt32(collection["idadeInicial"]),
                    IdadeFinal = Convert.ToInt32(collection["idadeFinal"]),
                    ScoreTotal = Convert.ToInt32(collection["scoreTotal"].ToString())
    };

                await ApiClientFactory.Instance.CreateSerie(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Serie", "Alterar")]
        //public ActionResult Edit(string id)
        //{
        //    var obj = ApiClientFactory.Instance.GetSerieById(id);

        //    var model = new SerieModel() { Serie = obj };

        //    return View(model);
        //}

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
                var command = new SerieModel.CreateUpdateSerieCommand
                {
                    Id = Convert.ToInt32(id),
                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
                    IdadeInicial = Convert.ToInt32(collection["idadeInicial"]),
                    IdadeFinal = Convert.ToInt32(collection["idadeFinal"]),
                    ScoreTotal = Convert.ToInt32(collection["scoreTotal"].ToString())
                };

                await ApiClientFactory.Instance.UpdateSerie(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteSerie(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
