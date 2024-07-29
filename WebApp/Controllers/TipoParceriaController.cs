using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class TipoParceriaController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public TipoParceriaController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetTipoParceriaAll();

            return View(new TipoParceriaModel() { TipoParcerias = response });
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
                var command = new TipoParceriaModel.CreateUpdateTipoParceriaCommand
                {
                    Nome = collection["tipoParceria"].ToString(),
                    Parceria = Convert.ToInt32(collection["ddlParceria"].ToString()),
                    Status = true
                };

                await ApiClientFactory.Instance.CreateTipoParceria(command);

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
            var command = new TipoParceriaModel.CreateUpdateTipoParceriaCommand
            {
                Id = Convert.ToInt32(collection["editTipoParceriaId"]),
                Nome = collection["nome"].ToString(),
                //Parceria = Convert.ToInt32(collection["ddlParceria"].ToString()),
                Status = collection["editStatus"].ToString() == "" ? false : true
            };

            await ApiClientFactory.Instance.UpdateTipoParceria(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteTipoParceria(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public Task<TipoParceriaDto> GetTipoParceriaById(int id)
        {
            var result = ApiClientFactory.Instance.GetTipoParceriaById(id);

            return Task.FromResult(result);
        }
    }

    
}
