using System.Text.Encodings.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;
using WebApp.Views;
using System.Security.Claims;

namespace WebApp.Controllers
{

    public class SistemaSocioeconomicoController : BaseController
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _host;

        public SistemaSocioeconomicoController(
            IOptions<UrlSettings> appSettings,
            IEmailSender emailSender,
            UserManager<IdentityUser> userManager, 
            IHostingEnvironment host, 
            RoleManager<IdentityRole> roleManager)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _host = host;
            _roleManager = roleManager;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Parceiro(int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var response = ApiClientFactory.Instance.GetParceiroAll();
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

                return View(new ParceiroModel() { Parceiros = response, ListEstados = estados });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema. {e.Message}"  });

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


                var tiposParcerias = new SelectList(ApiClientFactory.Instance.GetTipoParceriaAll().Where(x => x.Parceria == 1), "Id", "Nome");

                return View(new ParceiroModel() { ListEstados = estados, ListTiposParcerias = tiposParcerias });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema. {e.Message}" });

            }
        }
        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var status = collection["status"].ToString();
                var habilitado = collection["habilitado"].ToString();

                var command = new ParceiroModel.CreateUpdateParceiroCommand
                {

                    Nome =  collection["nome"].ToString(),
                    TipoPessoa =  collection["tipoPessoa"].ToString(),
                    CpfCnpj = collection["tipoPessoa"].ToString() == "pf" ? collection["cpf"].ToString() : collection["cnpj"].ToString(),
                    Telefone = collection["numTelefone"].ToString() == "" ? null : collection["numTelefone"].ToString(),
                    Celular = collection["numCelular"].ToString() == "" ? null : collection["numCelular"].ToString(),
                    Cep = collection["cep"].ToString() == "" ? null : collection["cep"].ToString(),
                    Endereco = collection["endereco"].ToString() == "" ? null : collection["endereco"].ToString(),
                    Numero = collection["numero"].ToString() == "" ? 0 : Convert.ToInt32(collection["numero"].ToString()),
                    Bairro = collection["bairro"].ToString() == "" ? null : collection["bairro"].ToString(),
                    MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    Habilitado = habilitado != "",
                    Status = status != "",
                    Email = collection["email"].ToString(),
                    TipoParceriaId = Convert.ToInt32(collection["ddlTipoParceria"].ToString()),

                };

                await ApiClientFactory.Instance.CreateParceiro(command);

                return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema. {e.Message}" });

            }
        }
        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> EditParceiro(int id)
        {
            try
            {
                ParceiroModel model = new ParceiroModel();
                {
                    var parceiro = await ApiClientFactory.Instance.GetParceiroById(id);
                    var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", parceiro.Uf);
                    var municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(parceiro.Uf), "Id", "Nome", parceiro.MunicipioId);

                    return View(new ParceiroModel() { ListEstados = estados, Parceiro = parceiro, ListMunicipios = municipios});
                }

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema. {e.Message}" });

            }
        }
        //[ClaimsAuthorize("Usuario", "Alterar")]
        [HttpPost]
        public async Task<ActionResult> EditParceiro(int id, IFormCollection collection)
        {
            try
            {
                var status = collection["status"].ToString();
                var habilitado = collection["habilitado"].ToString();

                var command = new ParceiroModel.CreateUpdateParceiroCommand
                {
                    Id = id,
                    Nome = collection["nome"].ToString(),
                    TipoPessoa = collection["tipoPessoa"].ToString(),
                    CpfCnpj = collection["tipoPessoa"] == "pj" ? collection["cpf"].ToString() : collection["cnpj"].ToString(),
                    Telefone = collection["numTelefone"] == "" ? null : collection["numTelefone"].ToString(),
                    Celular = collection["numCelular"] == "" ? null : collection["numCelular"].ToString(),
                    Cep = collection["cep"] == "" ? null : collection["cep"].ToString(),
                    Endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                    Numero = collection["numero"] == "" ? null : Convert.ToInt32(collection["numero"].ToString()),
                    Bairro = collection["bairro"] == "" ? null : collection["bairro"].ToString(),
                    MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    Habilitado = habilitado != "",
                    Status = status != "",
                    Email = collection["email"].ToString()
                };

                await ApiClientFactory.Instance.UpdateParceiro(command.Id, command);

                return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Updated });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema. {e.Message}" });

            }
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult DeleteParceiro(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteParceiro(id);
                return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Parceiro));
            }
        }

        public async Task<ParceiroDto> GetParceiroById(int id)
        {
            var result = await ApiClientFactory.Instance.GetParceiroById(id);

            return result;
        }


        public async Task<JsonResult> GetTiposParceriasByParceria(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Parceria não informado.");
                var resultLocal = ApiClientFactory.Instance.GetTipoParceriaAll()
                    .Where(x => x.Parceria == Convert.ToInt32(id) && x.Status == true);

                return Json(new SelectList(resultLocal, "Id", "Nome"));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Habilitar(IFormCollection collection)
        {
            try
            {
                var parceiroId = collection["habilitarParceiroId"].ToString();

                var result = await ApiClientFactory.Instance.GetParceiroById(Convert.ToInt32(parceiroId));

                if (result.Habilitado)
                {
                    return RedirectToAction(nameof(Parceiro),
                        new
                        {
                            notify = (int)EnumNotify.Error,
                            message = "Já existe parceiro/usuário habilitado com o E-mail cadastrado na base de dados!"
                        });
                }

                var result2 = ApiClientFactory.Instance.GetUsuarioByEmail(collection["email"].ToString().Trim());

                if (result2 != null)
                {
                    return RedirectToAction(nameof(Parceiro),
                        new
                        {
                            notify = (int)EnumNotify.Error,
                            message = "Já existe parceiro/usuário habilitado com o E-mail cadastrado na base de dados!"
                        });
                }

                var command = new UsuarioModel.CreateUpdateUsuarioCommand
                {
                    Email = collection["email"].ToString(),
                    Nome = collection["nome"].ToString(),
                    CpfCnpj = collection["cpfCnpj"].ToString()
                };

                var newUser = new IdentityUser { UserName = command.Email, Email = command.Email };
                var aspNetUser = await _userManager.CreateAsync(newUser, "12345678");

                if (aspNetUser.Succeeded)
                {
                    var perfil = ApiClientFactory.Instance.GetPerfilById((int)EnumPerfil.Parceiro);

                    var includedUserId = _userManager.Users.FirstOrDefault(x => x.Email == newUser.Email).Id;

                    command.AspNetUserId = includedUserId;
                    command.AspNetRoleId = perfil.AspNetRoleId;
                    command.PerfilId = perfil.Id;
                    command.Status = true;
                    command.MunicipioId = (int)result.MunicipioId;
                    command.TipoPessoa = result.TipoPessoa;
                    command.CpfCnpj = result.CpfCnpj;

                    var usu = await ApiClientFactory.Instance.CreateUsuario(command);

                    if (usu != 0)
                    {
                        var res = await ApiClientFactory.Instance.UpdateParceiro(result.Id,
                            new ParceiroModel.CreateUpdateParceiroCommand()
                                { AspNetUserId = command.AspNetUserId, Habilitado = true, Id = result.Id, Nome = result.Nome, CpfCnpj = result.CpfCnpj, Email = result.Email });
                    }

                    SendNewUserEmail(newUser, command.Email, command.Nome);

                    return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Created });
                }
                else
                {
                    return RedirectToAction(nameof(Parceiro),
                        new
                        {
                            notify = (int)EnumNotify.Error,
                            message = "Erro ao habilitar usuário. Favor entrar em contato com o administrador do sistema."
                        });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Parceiro),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = "Erro ao habilitar usuário. Favor entrar em contato com o administrador do sistema."
                    });
            }
        }


        private async Task SendNewUserEmail(IdentityUser user, string email, string nome)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.ActionLink("ResetPassword",
                "Identity/Account", new { code, email });

            var message =
                System.IO.File.ReadAllText(Path.Combine(_host.WebRootPath, "emailtemplates/ConfirmEmail.html"));
            message = message.Replace("%NAME%", nome);
            message = message.Replace("%CALLBACK%", HtmlEncoder.Default.Encode(callbackUrl.Replace("%2FAccount", "/Account")));

            await _emailSender.SendEmailAsync(user.Email, "Primeiro acesso sistema Dna do Brasil",
                message);
        }
















        public IActionResult Estudantes()
        {
            var response = ApiClientFactory.Instance.GetLaudoAll();

            return View();
        }
        public async Task<IActionResult> SolicitacaoContato()
        {
            try
            {
                var municipioId = ApiClientFactory.Instance.GetParceiroByAspNetUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)).MunicipioId;

                var alunos = await ApiClientFactory.Instance.GetAlunosByFilter(new AlunosFilterDto()
                    { MunicipioId = municipioId.ToString() });

                var model = new ParceiroModel() { Alunos = alunos!.Alunos! };

                return View(model);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
