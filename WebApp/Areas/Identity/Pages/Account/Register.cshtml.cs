using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Areas.Identity.Models;
using WebApp.Configuration;
using WebApp.Data;
using WebApp.Dto;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IOptions<UrlSettings> _appSettings;

        public RegisterModel(IOptions<UrlSettings> app,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
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

        public string ReturnUrl { get; set; }

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

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ListEstados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

            List<SelectListDto> list = new List<SelectListDto>
            {
                new() { IdNome = "PARDOS", Nome = "PARDOS" },
                new() { IdNome = "BRANCOS", Nome = "BRANCOS" },
                new() { IdNome = "NEGROS", Nome = "NEGROS" },
                new() { IdNome = "INDÍGENAS", Nome = "INDÍGENAS" },
                new() { IdNome = "AMARELOS", Nome = "AMARELOS" }
            };

            var etnias = new SelectList(list, "IdNome", "Nome");
            ListEtnias = etnias;

            List<SelectListDto> listArea = new List<SelectListDto>
            {
                new() { Id = 1, Nome = "Atividades Esportivas e Detecção de Talentos" },
                new() { Id = 2, Nome = "Preparatório para Vestibular e Aula de Reforço" },
                new() { Id = 3, Nome = "Temática com os Direitos Humanos" },
                new() { Id = 4, Nome = "Oficinas Profissionalizantes" },
                new() { Id = 5, Nome = "Atividade de Arte e Cultura" }
            };

            var areas = new SelectList(listArea, "Id", "Nome");
            ListAreas = areas;

            var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll(), "Id", "Nome");
            ListDeficiencia = deficiencias;

        }

        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            string returnUrl = null;
            returnUrl ??= Url.Content("~/");


            var command = new AlunoModel.CreateUpdateDadosAlunoCommand()
            {
                Etnia = collection["ddlEtnia"] == "" ? null : collection["ddlEtnia"].ToString(),
                MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                LocalidadeId = collection["ddlLocalidade"] == "" ? null : Convert.ToInt32(collection["ddlLocalidade"].ToString()),
                Nome = collection["nome"] == "" ? null : collection["nome"].ToString(),
                DtNascimento = collection["dtNasc"] == "" ? null : collection["DtNascimento"].ToString(),
                Email = collection["email"] == "" ? null : collection["email"].ToString(),
                EstadoId = collection["ddlEstado"] == "" ? null : collection["ddlEstado"].ToString(),
                endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                Sexo = collection["ddlSexo"] == "" ? null : collection["ddlSexo"].ToString(),
                
            };


            if (!ModelState.IsValid) return Page();
            var user = new IdentityUser { UserName = command.Email, Email = command.Email, EmailConfirmed = true, PhoneNumberConfirmed = true};
            var result = await _userManager.CreateAsync(user, "12345678");
            if (!result.Succeeded)
            {
	            foreach (var error in result.Errors)
	            {
		            ModelState.AddModelError(string.Empty, error.Description);
	            }

				// Se chegamos até aqui, algo falhou, exiba novamente o formulário
				return Page();
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
            //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
            //}
            //else
            //{
            //    await _signInManager.SignInAsync(user, isPersistent: false);
            //    return LocalRedirect(returnUrl);
            //}

            // Setup database roles
            //var userCount = _db.Users.Count();
            var roleCount = _db.Roles.Count();

            // NOTE: This is not necessarily best practice at all.
            // First registered user will add the default roles, rather than doing this in a migration.
            // First registered user will be given all roles.
            if (roleCount == 0)
            {
	            await _roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Aluno });

	            // set this registering user as admin/everything
	            await _userManager.AddToRolesAsync(user,
		            new[] { UserRoles.Aluno });
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Aluno);

            await _signInManager.SignInAsync(user, isPersistent: false);

            var idAluno = await ApiClientFactory.Instance.CreateDados(command);

            var autorizado = collection["autorizado"].ToString();
            var utilizacaoImagem = collection["utilizacaoImagem"].ToString();
            var participacao = collection["participacao"].ToString();

            var commandDependencia = new DependenciaModel.CreateUpdateDependenciaCommand()
            {
                AlunoId = (int?)idAluno,
                AutorizacaoSaida = autorizado != "",
                AutorizacaoUsoImagemAudio = utilizacaoImagem != "",
                AutorizacaoUsoIndicadores = participacao != ""

            };

            await ApiClientFactory.Instance.CreateDependencia(commandDependencia);

            return RedirectToPage("RegisterConfirmation", new { email = command.Email, returnUrl = returnUrl });
        }
    }
}
