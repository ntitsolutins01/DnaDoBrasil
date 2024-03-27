using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class FuncionalidadeController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public FuncionalidadeController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetFuncionalidadesAll();

            return View(new FuncionalidadeModel() { Funcionalidades = response });
        }

        //[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);
                var modulos = new SelectList(ApiClientFactory.Instance.GetModulosAll(), "Id", "Nome");

                return View(new ModuloModel() { ListModulos = modulos });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new FuncionalidadeModel.CreateUpdateFuncionalidadeCommand
                {
                    ModuloId = Convert.ToInt32(collection["ddlModulo"].ToString()),
                    Nome = collection["nome"].ToString()
                };

                await ApiClientFactory.Instance.CreateFuncionalidade(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema. {e.Message}" });
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(IFormCollection collection)
        {
            try
            {
                var command = new FuncionalidadeModel.CreateUpdateFuncionalidadeCommand
                {
                    Id = Convert.ToInt32(collection["editFuncionalidadeId"]),
                    Nome = collection["nome"].ToString()
                };

                await ApiClientFactory.Instance.UpdateFuncionalidade(command.Id, command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema. {e.Message}" });
            }
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteFuncionalidade(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public Task<FuncionalidadeDto> GetFuncionalidadeById(int id)
        {
            var result = ApiClientFactory.Instance.GetFuncionalidadeById(id);

            return Task.FromResult(result);
        }
    }
}
