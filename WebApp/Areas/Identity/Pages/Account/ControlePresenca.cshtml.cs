using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Areas.Identity.Models;
using WebApp.Configuration;
using WebApp.Data;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ControlePresencaModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ControlePresencaModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IOptions<UrlSettings> _appSettings;

        public ControlePresencaModel(IOptions<UrlSettings> app,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ControlePresencaModel> logger,
            IEmailSender emailSender, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _appSettings = app;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }
        [BindProperty]
        public LoginInput Login { get; set; }
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class LoginInput : IValidatableObject
        {
            [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "O e-mail informado deve atender um formato padrão válido.")]
            public string Email { get; set; }

            [DataType(DataType.Password)] public string Password { get; set; }

            [Display(Name = "Remember me?")] public bool RememberMe { get; set; }

            public bool Submitted { get; set; } = false;
            public string Justificativa { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                var results = new List<ValidationResult>();

                if (Submitted)
                {
                    if (string.IsNullOrEmpty(Email))
                        results.Add(new ValidationResult("Your email address is required", new[] { "Email" }));

                    if (string.IsNullOrEmpty(Password))
                        results.Add(new ValidationResult("Your password is required", new[] { "Password" }));
                }

                return results;
            }
        }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public int LocalidadeId { get; set; }
        public int EtniaId { get; set; }
        public int AreaId { get; set; }
        public int DeficienciaId { get; set; }
        public SelectList ListEstados { get; set; }
        public SelectList ListMunicipios { get; set; }
        public SelectList ListLocalidades { get; set; }
        public SelectList ListEtnias { get; set; }
        public SelectList ListAreas { get; set; }
        public SelectList ListDeficiencia { get; set; }
        public SelectList ListEficiencia { get; set; }
        public int NotifyMessage { get; set; }
        public string Notify { get; set; }

        public async Task OnGetAsync(int? notify, string message = null, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            switch (notify)
            {
                case (int)EnumNotify.Success:
                    NotifyMessage = (int)EnumNotify.Success;
                    Notify = message;
                    break;
                case (int)EnumNotify.Error:
                    NotifyMessage = (int)EnumNotify.Error;
                    Notify = message;
                    break;
                case (int)EnumNotify.Warning:
                    NotifyMessage = (int)EnumNotify.Warning;
                    Notify = message;
                    break;
                default:
                    NotifyMessage = -1;
                    Notify = "null";
                    break;
            }

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        }



        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            string returnUrl = null;
            returnUrl ??= Url.Content("~/");


            var command = new AlunoModel.CreateUpdateDadosAlunoCommand()
            {
                MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                LocalidadeId = collection["ddlLocalidade"] == "" ? null : Convert.ToInt32(collection["ddlLocalidade"].ToString()),
                Endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                AreasDesejadas = collection["ddlAreaDesejada"] == "" ? null : collection["ddlAreaDesejada"].ToString(),
                Nome = collection["nome"] == "" ? null : collection["nome"].ToString(),
                Cpf = collection["cpf"] == "" ? null : collection["cpf"].ToString(),
                Sexo = collection["ddlSexo"] == "" ? null : collection["ddlSexo"].ToString(),
                DtNascimento = collection["dtNasc"] == "" ? null : collection["dtNasc"].ToString(),
                Email = collection["email"] == "" ? null : collection["email"].ToString(),
                Etnia = collection["ddlEtnia"] == "" ? null : collection["ddlEtnia"].ToString(),
                NomeMae = collection["nomeMae"] == "" ? null : collection["nomeMae"].ToString(),
                NomeResponsavel = collection["nomeResp"] == "" ? null : collection["nomeResp"].ToString(),
                DeficienciasIds = collection["ddlDeficiencia"] == "" ? null : collection["ddlDeficiencia"].ToString(),
                Habilitado = true,
                Status = true
            };


            if (!ModelState.IsValid) return Page();
            var user = new IdentityUser { UserName = command.Email, Email = command.Email, EmailConfirmed = true, PhoneNumberConfirmed = true};
            var result = await _userManager.CreateAsync(user, "12345678");
            StringBuilder msg = new StringBuilder();
            if (!result.Succeeded)
            {
	            foreach (var error in result.Errors)
	            {
		            ModelState.AddModelError(string.Empty, error.Description);
                    msg.AppendLine(error.Description);
                }

                // Se chegamos até aqui, algo falhou, exiba novamente o formulário
                //return Page();
                return RedirectToPage("ControlePresenca", new { notify = (int)EnumNotify.Error, message = msg });
            }

            _logger.LogInformation($"O usuário {user.UserName} criou uma nova conta com senha.");

            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //var callbackUrl = Url.Page(
            //    "/Account/ConfirmEmail",
            //    pageHandler: null,
            //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
            //    protocol: Request.Scheme);

            //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            //if (_userManager.Options.SignIn.RequireConfirmedAccount)
            //{
            //    return RedirectToPage("ControlePresencaConfirmation", new { email = Input.Email, returnUrl = returnUrl });
            //}
            //else
            //{
            //    await _signInManager.SignInAsync(user, isPersistent: false);
            //    return LocalRedirect(returnUrl);
            //}

            // Setup database roles
            //var userCount = _db.Users.Count();
            //var roleCount = _db.Roles.Count();

            // NOTE: This is not necessarily best practice at all.
            // First registered user will add the default roles, rather than doing this in a migration.
            // First registered user will be given all roles.
            //if (roleCount == 0)
            //{
	           // await _roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Aluno });

	           // // set this registering user as admin/everything
	           // await _userManager.AddToRolesAsync(user,
		          //  new[] { UserRoles.Aluno });
            //}

            await _roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Aluno });

            // set this registering user as admin/everything
            await _userManager.AddToRolesAsync(user,
                new[] { UserRoles.Aluno });

            await _userManager.AddToRoleAsync(user, UserRoles.Aluno);

            await _signInManager.SignInAsync(user, isPersistent: false);

            var idAluno = await ApiClientFactory.Instance.CreateDados(command);

            var autorizado = Convert.ToBoolean(collection["autorizado"].ToString());
            var utilizacaoImagem = Convert.ToBoolean(collection["utilizacaoImagem"].ToString());
            var participacao = Convert.ToBoolean(collection["participacao"].ToString());
            var copiaDoc = collection["copiaDoc"].ToString() != "";

            var commandDependencia = new DependenciaModel.CreateUpdateDependenciaCommand()
            {
                AlunoId = (int?)idAluno,
                AutorizacaoSaida = autorizado,
                AutorizacaoUsoImagemAudio = utilizacaoImagem,
                AutorizacaoUsoIndicadores = participacao,
                TermoCompromisso = true


            };

            await ApiClientFactory.Instance.CreateDependencia(commandDependencia);

            return RedirectToPage("ControlePresencaConfirmation", new { email = command.Email, returnUrl = returnUrl });
        }
    }
}
