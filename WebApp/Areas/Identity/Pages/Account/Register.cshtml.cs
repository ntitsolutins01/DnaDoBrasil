using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using WebApp.Areas.Identity.Models;
using WebApp.Configuration;
using WebApp.Data;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
        private readonly IHostingEnvironment _host;

        public RegisterModel(IOptions<UrlSettings> app,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, 
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext db,
            IHostingEnvironment host)
        {
            _appSettings = app;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
            _host = host;
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
            ListEstados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

            List<SelectListDto> list = new List<SelectListDto>
            {
                new() { IdNome = "PARDOS", Nome = "PARDOS" },
                new() { IdNome = "BRANCOS", Nome = "BRANCOS" },
                new() { IdNome = "NEGROS", Nome = "NEGROS" },
                new() { IdNome = "INDIGENAS", Nome = "INDIGENAS" },
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

            var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll().Where(x => x.Status), "Id", "Nome");
            ListDeficiencia = deficiencias;

        }



        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            var commandAluno = new AlunoModel.CreateUpdateDadosAlunoCommand()
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

            var alunoId = await ApiClientFactory.Instance.CreateDados(commandAluno);

            var result = ApiClientFactory.Instance.GetAlunoById((int)alunoId);

            var command = new UsuarioModel.CreateUpdateUsuarioCommand
            {
                Email = collection["email"].ToString(),
                Nome = collection["nome"].ToString(),
                CpfCnpj = collection["cpf"].ToString()
            };

            var newUser = new IdentityUser { UserName = command.Email, Email = command.Email };
            var aspNetUser = await _userManager.CreateAsync(newUser, "12345678");

            StringBuilder msg = new StringBuilder();
            if (!aspNetUser.Succeeded)
            {
                foreach (var error in aspNetUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    msg.AppendLine(error.Description);
                }

                // Se chegamos até aqui, algo falhou, exiba novamente o formulário
                //return Page();
                return RedirectToPage("Register", new { notify = (int)EnumNotify.Error, message = msg });
            }

            var perfil = ApiClientFactory.Instance.GetPerfilById((int)EnumPerfil.Aluno);

            var includedUserId = _userManager.Users.FirstOrDefault(x => x.Email == newUser.Email).Id;

            command.AspNetUserId = includedUserId;
            command.AspNetRoleId = perfil.AspNetRoleId;
            command.PerfilId = perfil.Id;

            var usu = await ApiClientFactory.Instance.CreateUsuario(command);

            if (usu != 0)
            {
                var res = await ApiClientFactory.Instance.UpdateDados(result.Id,
                    new AlunoModel.CreateUpdateDadosAlunoCommand()
                    { AspNetUserId = command.AspNetUserId, Habilitado = true, Id = result.Id, Nome = result.Nome, Cpf = result.Cpf, Email = result.Email });
            }

            SendNewUserEmail(newUser, command.Email, command.Nome);

            var autorizado = Convert.ToBoolean(collection["autorizado"].ToString());
            var utilizacaoImagem = Convert.ToBoolean(collection["utilizacaoImagem"].ToString());
            var participacao = Convert.ToBoolean(collection["participacao"].ToString());
            var copiaDoc = collection["copiaDoc"].ToString() != "";

            //var commandDependencia = new DependenciaModel.CreateUpdateDependenciaCommand()
            //{
            //    AlunoId = (int?)alunoId,
            //    AutorizacaoSaida = autorizado,
            //    AutorizacaoUsoImagemAudio = utilizacaoImagem,
            //    AutorizacaoUsoIndicadores = participacao,
            //    TermoCompromisso = true
            //};

            //await ApiClientFactory.Instance.CreateDependencia(commandDependencia);

            string returnUrl = null;
            returnUrl ??= Url.Content("~/");

            return RedirectToPage("RegisterConfirmation", new { email = command.Email, returnUrl = returnUrl });
        }

        private async Task SendNewUserEmail(IdentityUser user, string email, string nome)
        {
            string returnUrl = null;
            returnUrl ??= Url.Content("~/");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.ActionLink("ResetPassword",
                "Identity/Account", new { code, email });

            var message =
                System.IO.File.ReadAllText(Path.Combine(_host.WebRootPath, "emailtemplates/ConfirmEmail.html"));
            message = message.Replace("%NAME%", nome);
            message = message.Replace("%CALLBACK%", HtmlEncoder.Default.Encode(callbackUrl.Replace("%2FAccount", "/Account")));

            await _emailSender.SendEmailAsync(user.Email, "Primeiro acesso sistema Dna do Brasil",
                message);
        }
    }
}
