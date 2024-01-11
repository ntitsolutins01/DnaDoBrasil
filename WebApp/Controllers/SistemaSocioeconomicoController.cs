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
                var status = collection["status"].ToString();
                var habilitado = collection["habilitado"].ToString();

                var command = new ParceiroModel.CreateUpdateParceiroCommand
                {

                    Nome =  collection["nome"].ToString(),
                    TipoPessoa =  collection["tipoPessoa"].ToString(),
                    CpfCnpj = collection["tipoPessoa"] == "pf" ? collection["cpf"].ToString() : collection["cnpj"].ToString(),
                    Telefone = collection["numTelefone"] == "" ? null : collection["numTelefone"].ToString(),
                    Celular = collection["numCelular"] == "" ? null : collection["numCelular"].ToString(),
                    Cep = collection["cep"] == "" ? null : collection["cep"].ToString(),
                    Endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                    Numero = collection["numero"] == "" ? 0 : Convert.ToInt32(collection["numero"].ToString()),
                    Bairro = collection["bairro"] == "" ? null : collection["bairro"].ToString(),
                    MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    Habilitado = habilitado != "",
                    Status = status != "",
                    Email = collection["email"].ToString(),
                    TipoParceria = Convert.ToInt32(collection["TipoParceria"].ToString()),

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
                {
                    var parceiro = ApiClientFactory.Instance.GetParceiroById(id);
                    var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

                    return View(new ParceiroModel() { ListEstados = estados, Parceiro = parceiro });
                }

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }
        //[ClaimsAuthorize("Usuario", "Alterar")]
        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
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
                    Email = collection["email"].ToString(),
                    TipoParceria = Convert.ToInt32(collection["TipoParceria"].ToString()),
                };

                await ApiClientFactory.Instance.UpdateParceiro(command.Id, command);

                return RedirectToAction(nameof(EditParceiro), new { crud = (int)EnumCrud.Updated });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Parceiro), new { notify = (int)EnumNotify.Error, message = e.Message });

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

        public Task<ParceiroDto> GetParceiroById(int id)
        {
            var result = ApiClientFactory.Instance.GetParceiroById(id);

            return Task.FromResult(result);
        }

        [HttpPost]
        public async Task<ActionResult> Habilitar(IFormCollection collection)
        {
            try
            {
                var parceiroId = collection["habilitarParceiroId"].ToString();

                var result = ApiClientFactory.Instance.GetParceiroById(Convert.ToInt32(parceiroId));

                if (result.Email != null && result.Email.Equals(collection["email"].ToString().Trim()))
                {
                    return RedirectToAction(nameof(Parceiro),
                        new
                        {
                            notify = (int)EnumNotify.Error,
                            message = "Já existe um parceiro cadastrado com esse email."
                        });
                }

                var result2 = ApiClientFactory.Instance.GetUsuarioByEmail(collection["email"].ToString().Trim());

                if (result2 != null)
                {
                    return RedirectToAction(nameof(Create),
                        new
                        {
                            notify = (int)EnumNotify.Error,
                            message = "Já existe parceiro com o E-mail cadastrado na base de dados!"
                        });
                }

                var command = new UsuarioModel.CreateUpdateUsuarioCommand
                {
                    Email = collection["email"].ToString(),
                    Nome = collection["nome"].ToString()
                };

                var newUser = new IdentityUser { UserName = command.Email, Email = command.Email };
                await _userManager.CreateAsync(newUser, "12345678");

                command.PerfilId = result2.PerfilId;
                var perfil = ApiClientFactory.Instance.GetPerfilById(command.PerfilId);

                var includedUserId = _userManager.Users.FirstOrDefault(x => x.Email == newUser.Email).Id;

                command.AspNetUserId = includedUserId;
                command.AspNetRoleId = perfil.AspNetRoleId;

                ApiClientFactory.Instance.CreateUsuario(command);

                SendNewUserEmail(newUser, command.Email, command.Nome);

                return RedirectToAction(nameof(Parceiro), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Parceiro),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = "Erro ao criar usuário. Favor entrar em contato com o administrador do sistema."
                    });
            }
        }

        private async Task SendNewUserEmail(IdentityUser user, string email, string nome)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(new IdentityUser(user.Email));

            var callbackUrl = Url.ActionLink("ResetPassword",
                "Identity/Account", new { code, email });

            var message =
                System.IO.File.ReadAllText(Path.Combine(_host.WebRootPath, "emailtemplates/ConfirmEmail.html"));
            message = message.Replace("%NAME%", nome);
            message = message.Replace("%CALLBACK%", HtmlEncoder.Default.Encode(callbackUrl.Replace("%2FAccount", "/Account")));

            await _emailSender.SendEmailAsync(user.Email, "Primeiro acesso sistema Dna Brasil",
                message);
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
