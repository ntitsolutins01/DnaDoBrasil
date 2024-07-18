using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApp.Areas.Identity.Models;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;
using WebApp.Configuration;
using WebApp.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        serverDbContextOptionsBuilder =>
        {
            var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
            serverDbContextOptionsBuilder.CommandTimeout(minutes);
            serverDbContextOptionsBuilder.EnableRetryOnFailure();
        }));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.Name = "DnaCookieLogin";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.LoginPath = "/Identity/Account/Login";
    options.SlidingExpiration = true;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Identity/Account/Login", "");
});

builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("DnaSettings"));
builder.Services.Configure<ParametersModel>(builder.Configuration.GetSection("DnaParameters"));
builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));
builder.Services.Configure<SmtpClientSettings>(builder.Configuration.GetSection("SmtpClient"));


var config = builder.Configuration.GetSection("DnaParameters").Get<ParametersModel>();

//define a quantidade de tempo que um token gerado permanece v�lido. PS: O padr�o � 1 dia.
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromMinutes(config.TokenTime));

// Registra o servi�o de e-mail. Configurado em appsettings.json

builder.Services.AddTransient<IEmailSender, EmailService>();

builder.Services.AddAuthorization(o =>
{

    o.AddPolicy(ModuloAccess.ConfiguracaoSistema, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.ConfiguracaoEad, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.ControleAcesso, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.Dashboard, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.ControlePresenca, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.Nota, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.Profissional, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.SistemaSocioeconomico, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Parceiro) ||
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.PlanoAula, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.Aluno, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.Laudo, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Parceiro) ||
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.Curso, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Administrador)));

    o.AddPolicy(ModuloAccess.Evento, policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Profissional) ||
            context.User.IsInRole(UserRoles.Gestor) ||
            context.User.IsInRole(UserRoles.Administrador)));
});

//// Cria um grupo de pol�ticas de administradores para requisitos de seguran�a de alto n�vel
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(ModuloAccess.Dashboard, policy =>
//        policy.RequireAssertion(context =>
//            context.User.IsInRole(UserRoles.Administrador)
//        || context.User.IsInRole(UserRoles.Aluno)));

////options.AddPolicy(ModuloAccess.Relatos, policy =>
////    policy.RequireAssertion(context =>
////        context.User.IsInRole(UserRoles.Administrator)
////        || context.User.IsInRole(UserRoles.UsuarioPublico)));

////options.AddPolicy(ModuloAccess.AtribuirResponsavelRelato, policy =>
////    policy.RequireAssertion(context =>
////        context.User.IsInRole(UserRoles.Administrator)
////        || context.User.IsInRole(UserRoles.GestorSgsoSite)
////        || context.User.IsInRole(UserRoles.ResponsavelTecnico)
////        || context.User.IsInRole(UserRoles.UsuarioPublico)));

////options.AddPolicy(ModuloAccess.ConfigurarAmbiente, policy =>
////    policy.RequireAssertion(context =>
////        context.User.IsInRole(UserRoles.Administrator)
////        || context.User.IsInRole(UserRoles.GestorSgsoSite)));

//});

var app = builder.Build();

// Configura��o the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
