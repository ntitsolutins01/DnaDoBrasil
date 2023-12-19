using Infraero.Relprev.CrossCutting.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Areas.Identity.Models;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class ParceiroController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public ParceiroController(IOptions<UrlSettings> appSettings)
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
                var response = ApiClientFactory.Instance.GetParceiroAll();

                return View(new ParceiroModel() { Parceiros = response });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        //[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                return View();
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
                if (collection["tipoPessoa"].ToString() == "pf")
                {
                    var numero 
                }
                var command = new ParceiroModel.CreateUpdateParceiroCommand
                {
                    Nome = collection["nome"].ToString().ToUpper(),
                    Email = collection["email"].ToString(),
                    //TipoParceria = Convert.ToDateTime(collection["tipoparceria"].ToString()),
                    //TipoPessoa = Convert.ToDateTime(collection["tipopessoa"].ToString()),
                    CpfCnpj = collection["tipoPessoa"].ToString() == "pf"? collection["cpf"].ToString() : collection["cnpj"].ToString()
                    Telefone = collection["telefone"].ToString(),
                    Celular = collection["celular"].ToString(),
                    Cep = collection["cep"].ToString(),
                    Endereco = collection["Endereco"].ToString(),
                    Numero = collection["numero"].ToString(),
                    Bairro = collection["bairro"].ToString(),
                    Status = collection["status"].ToString(),
                    Habilitado = collection["habilitado"].ToString(),
                };

                await ApiClientFactory.Instance.CreateParceiro(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(IFormCollection collection)
        {
            try
            {
                var command = new ParceiroModel.CreateUpdateParceiroCommand
                {
                    Id = Convert.ToInt32(collection["editParceiroId"]),
                    Nome = collection["nome"].ToString().ToUpper(),
                    Descricao = collection["descricao"].ToString(),
                    DtIni = Convert.ToDateTime(collection["dtini"].ToString()),
                    DtFim = Convert.ToDateTime(collection["dtfim"].ToString()),
                    Status = collection["editStatus"].ToString() == "" ? false : true,
                    Anexo = collection["anexo"].ToString()
                };

                await ApiClientFactory.Instance.UpdateParceiro(command.Id, command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });
            }
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteParceiro(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ParceiroDto> GetParceiroById(int id)
        {
            var result = ApiClientFactory.Instance.GetParceiroById(id);

            return result;
        }
    }
}

