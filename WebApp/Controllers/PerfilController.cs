using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Plugins;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class PerfilController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;


        private readonly IOptions<SettingsModel> _appSettings;

        public PerfilController(ApplicationDbContext db,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager, IOptions<SettingsModel> appSettings)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        //[ClaimsAuthorize("Perfil", "Consultar")]
        public ActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var response = ApiClientFactory.Instance.GetPerfilAll();
            return View(new PerfilModel { Perfis = response });
        }

        //[ClaimsAuthorize("Perfil", "Incluir")]
        public ActionResult Create()
        {
            var responseModulos = ApiClientFactory.Instance.GetModuloAll();

            var model = new PerfilModel { Modulos = responseModulos };
            return View(model);
        }

        //[ClaimsAuthorize("Perfil", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                ListDictionary list = new ListDictionary();
                var responseModulos = ApiClientFactory.Instance.GetModuloAll();

                foreach (var modulo in responseModulos)
                {
                    var funcionalidades = modulo.Funcionalidades.FirstOrDefault().Nome.Split(',');
                    var listValue = funcionalidades
                        .Where(func => collection[modulo.Nome + func].ToString() == "on").ToList();
                    list.Add(modulo.Nome, string.Join(",", listValue));
                }

                var command = new PerfilModel.CreateCommand()
                {
                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
                    Claims = list
                };

                var adminRole = await _roleManager.FindByNameAsync(command.Nome);

                if (adminRole != null) return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
                adminRole = new IdentityRole(command.Nome);
                await _roleManager.CreateAsync(adminRole);
                foreach (var addClaim in from DictionaryEntry claim in command.Claims
                         select new Claim(claim.Key.ToString()!, claim.Value!.ToString()!))
                {
                    await _roleManager.AddClaimAsync(adminRole, addClaim);
                }
                await ApiClientFactory.Instance.CreatePerfil(command);
                
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
