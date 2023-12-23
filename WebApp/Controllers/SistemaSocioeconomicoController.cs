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

    public class SistemaSocioeconomicoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public SistemaSocioeconomicoController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Parceiro(int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud); var response = ApiClientFactory.Instance.GetParceiroAll();
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

                return View(new ParceiroModel() { Parceiros = response, ListEstados = estados });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(CreateParceiro), new { notify = (int)EnumNotify.Error, message = e.Message });

            }

        }

        //[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
        public ActionResult CreateParceiro(int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

                return View(new ParceiroModel() { ListEstados = estados });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }
        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new ParceiroModel.CreateUpdateParceiroCommand
                {

                    Nome = collection["nome"].ToString(),
                    TipoPessoa = collection["TipoPessoa"].ToString(),
                    CpfCnpj = collection["cpfCnpj"].ToString(),
                    Telefone = collection["Telefone"].ToString(),
                    Celular = collection["Celular"].ToString(),
                    Endereco = collection["Endereço"].ToString(),
                    Numero = Convert.ToInt32(collection["numero"].ToString()),
                    Bairro = collection["Bairro"].ToString(),
                    Habilitado = collection["habilitado"].ToString() == "1" ? true : false, //TODO: Verificar condicional para resgate radio button
                    Status = collection["status"].ToString() == "1" ? true : false,
                    Email = collection["Email"].ToString(),
                    TipoParceria = Convert.ToInt32(collection["TipoParceria"].ToString()), //TODO: Verificar condicional para resgate checkbox
                                                                                           //Habilitado = collection["habilitado"].ToString()
                };

                await ApiClientFactory.Instance.CreateParceiro(command);

                return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }
        //[ClaimsAuthorize("Usuario", "Alterar")]
        public ActionResult EditParceiro(int id)
        {
            try
            {
                ParceiroModel model = new ParceiroModel();

                var obj = ApiClientFactory.Instance.GetParceiroById(id);

                if (obj != null)
                {
                    var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", obj.EstadoId);
                    var municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(obj.Uf), "Id", "Nome", obj.MunicipioId);

                    model = new ParceiroModel()
                    {
                        ListEstados = estados,
                        ListMunicipio = municipios,
                        Parceiro = obj
                    };

                    return View(model);
                }

                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = "Parceiro não encontrado" });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }
        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            var command = new ParceiroModel.CreateUpdateParceiroCommand
            {
                Id = Convert.ToInt32(id),
                Nome = collection["nome"].ToString(),
                TipoPessoa = collection["TipoPessoa"].ToString(),
                CpfCnpj = collection["cpfCnpj"].ToString(),
                Telefone = collection["Telefone"].ToString(),
                Celular = collection["Celular"].ToString(),
                Endereco = collection["Endereço"].ToString(),
                Numero = Convert.ToInt32(collection["numero"].ToString()),
                Bairro = collection["Bairro"].ToString(),
                Habilitado = collection["habilitado"].ToString() == "1" ? true : false, //TODO: Verificar condicional para resgate radio button
                Status = collection["status"].ToString() == "1" ? true : false,
                Email = collection["Email"].ToString(),
                TipoParceria = Convert.ToInt32(collection["TipoParceria"].ToString()), //TODO: Verificar condicional para resgate checkbox
            };

            //await ApiClientFactory.Instance.UpdateParceiro(command);

            return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                //ApiClientFactory.Instance.DeleteParceiro(id);
                return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Parceiro));
            }
        }

        public async Task<ParceiroDto> GetParceiroById(int id)
        {
            var result = ApiClientFactory.Instance.GetParceiroById(id);

            return result;
        }

















        public IActionResult Estudantes()
        {
            return View();
        }
        public IActionResult SolicitacaoContato()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
