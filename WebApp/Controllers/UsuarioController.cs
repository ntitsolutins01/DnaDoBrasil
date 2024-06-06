using Infraero.Relprev.CrossCutting.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using WebApp.Areas.Identity.Models;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using WebApp.Configuration;

namespace WebApp.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _host;

        public UsuarioController(IOptions<UrlSettings> app, IEmailSender emailSender,
            UserManager<IdentityUser> userManager, IHostingEnvironment host, RoleManager<IdentityRole> roleManager)
        {
            _appSettings = app;
            _emailSender = emailSender;
            _userManager = userManager;
            _host = host;
            _roleManager = roleManager;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }
        //[ClaimsAuthorize("Usuario", "Consultar")]
        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetUsuarioAll(); var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeAll(), "Id", "Nome");
            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

            return View(new UsuarioModel()
            {
                Usuarios = response,
                ListEstados = estados
            });
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var resultPerfil = ApiClientFactory.Instance.GetPerfilAll();
            var estados = ApiClientFactory.Instance.GetEstadosAll();

            var model = new UsuarioModel
            {
                ListEstados = new SelectList(estados, "Sigla", "Nome"),
                ListPerfis = new SelectList(resultPerfil, "Id", "Nome")
            };
            return View(model);
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var cpf = collection["cpf"].ToString();

                var result = ApiClientFactory.Instance.GetUsuarioByCpf(cpf); //Regex.Replace(cpf, "[^0-9a-zA-Z]+", "")

                if (result != null)
                {
                    return RedirectToAction(nameof(Create),
                        new
                        {
                            notify = (int)EnumNotify.Error,
                            message = "Já existe um usuário cadastrado com esse cpf."
                        });
                }

                var result2 = ApiClientFactory.Instance.GetUsuarioByEmail(collection["email"].ToString().Trim());

                if (result2 != null)
                {
                    return RedirectToAction(nameof(Create),
                        new
                        {
                            notify = (int)EnumNotify.Error,
                            message = "Já existe usuários com o E-mail informado cadastrado na base de dados!"
                        });
                }

                var command = new UsuarioModel.CreateUpdateUsuarioCommand
                {
                    Email = collection["email"].ToString(),
                    Nome = collection["nome"].ToString(),
                    CpfCnpj = collection["cpf"].ToString(),
                    TipoPessoa = collection["tipoPessoa"].ToString(),
                    MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                };


                command.PerfilId = int.Parse(collection["ddlPerfil"].ToString());
                var perfil = ApiClientFactory.Instance.GetPerfilById(command.PerfilId);

                var newUser = new IdentityUser { UserName = command.Email, Email = command.Email };
                var userCreated = await _userManager.CreateAsync(newUser, "12345678");

                if (userCreated.Succeeded)
                {
                    var userRole = _roleManager.Roles.FirstOrDefault(x => x.Id == perfil.AspNetRoleId).Name;

                    command.AspNetUserId = newUser.Id;
                    command.AspNetRoleId = perfil.AspNetRoleId;
                    command.PerfilId = perfil.Id;

                    var usuarioId = await ApiClientFactory.Instance.CreateUsuario(command);

                    if (usuarioId != 0)
                    {
                        await _userManager.AddToRoleAsync(newUser, userRole);
                    }

                    SendNewUserEmail(newUser, command.Email, command.Nome);

                    return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
                }

                return RedirectToAction(nameof(Index),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = $"Erro ao criar usuário. Favor entrar em contato com o administrador do sistema."
                    });

            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = "Erro ao criar usuário. Favor entrar em contato com o administrador do sistema."
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

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public ActionResult Edit(string id)
        {
            try
            {

                UsuarioModel model = new UsuarioModel();

                var obj = ApiClientFactory.Instance.GetUsuarioById(id) ?? throw new ArgumentNullException("Usuário não encontrado.");

                var resultPerfil = ApiClientFactory.Instance.GetPerfilAll();

                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", obj.Uf);
                var municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(obj.Uf!), "Id", "Nome", obj.MunicipioId);
                
                model = new UsuarioModel
                {
                    ListPerfis = new SelectList(resultPerfil, "Id", "Nome", obj.Perfil.Id),
                    Usuario = obj,
                    ListMunicipios = municipios,
                    ListEstados = estados
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = $"Erro ao alterar usuário. Favor entrar em contato com o administrador do sistema. {ex.Message}"
                    });
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var perfil = ApiClientFactory.Instance.GetPerfilById(Convert.ToInt32(collection["ddlPerfil"].ToString()));

                var includedUserId = ""; //_userManager.Users.FirstOrDefault(x => x.Email == newUser.Email).Id;


                var command = new UsuarioModel.CreateUpdateUsuarioCommand
                {
                    Email = collection["email"].ToString(),
                    Nome = collection["nome"].ToString(),
                    TipoPessoa = collection["tipoPessoa"].ToString(),
                    CpfCnpj = collection["cpf"].ToString() == "" ? collection["cnpj"].ToString() : collection["cpf"].ToString(),
                    PerfilId = perfil.Id,
                    AspNetUserId = userId,
                    AspNetRoleId = perfil.AspNetRoleId,
                    MunicipioId = Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                };

                await ApiClientFactory.Instance.UpdateUsuario(id, command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = $"Erro ao alterar usuário. Favor entrar em contato com o administrador do sistema. {ex.Message}"
                    });
            }
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var usuario = ApiClientFactory.Instance.GetUsuarioById(id.ToString()) ?? throw new ArgumentNullException("Usuário não encontrado.");

                var user = _userManager.Users.FirstOrDefault(x => x.Email == usuario.Email);

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRoleAsync(user, _roleManager.Roles.FirstOrDefault(x => x.Id == usuario.AspNetRoleId).Name);

                    ApiClientFactory.Instance.DeleteUsuario(id);

                    return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
                }

                StringBuilder str = new StringBuilder();

                // Handle failure
                foreach (var error in result.Errors)
                {
                    str.AppendLine(error.Description);
                }

                return RedirectToAction(nameof(Index),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = $"Erro ao criar usuário. Favor entrar em contato com o administrador do sistema. {str}"
                    });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index),
                    new
                    {
                        notify = (int)EnumNotify.Error,
                        message = $"Erro ao excluir usuário. Favor entrar em contato com o administrador do sistema. {ex.Message}"
                    });
            }
        }

        //[ClaimsAuthorize("Usuario", "Consultar")]
        public Task<JsonResult> GetUsuarioByEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) throw new Exception("Email não informado.");
                var result = ApiClientFactory.Instance.GetUsuarioByEmail(email);

                if (result == null)
                {
                    return Task.FromResult(Json(true));
                }

                return Task.FromResult(Json(false));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex.Message));
            }

        }

        public Task<JsonResult> GetUsuarioByCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf)) throw new Exception("Cpf não informado.");
                var result = ApiClientFactory.Instance.GetUsuarioByCpf(Regex.Replace(cpf, "[^0-9a-zA-Z]+", ""));

                if (result == null)
                {
                    return Task.FromResult(Json(true));
                }

                return Task.FromResult(Json(false));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex.Message));
            }
        }
    }
}
