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
	public class DeficienciaController : BaseController
    {
        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetDeficienciaAll();

            return View(new DeficienciaModel(){Deficiencias = response});
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
                var command = new DeficienciaModel.CreateUpdateDeficienciaCommand
                {

                    TipoLaudo = collection["tipoLaudo"].ToString()
                };

                await ApiClientFactory.Instance.CreateDeficiencia(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Deficiencia", "Alterar")]
        //public ActionResult Edit(string id)
        //{
        //    var obj = ApiClientFactory.Instance.GetDeficienciaById(id);

        //    var model = new DeficienciaModel() { Deficiencia = obj };

        //    return View(model);
        //}

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
                var command = new DeficienciaModel.CreateUpdateDeficienciaCommand
                {

                    TipoLaudo = collection["tipoLaudo"].ToString()
                };

                //await ApiClientFactory.Instance.UpdateDeficiencia(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteDeficiencia(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
