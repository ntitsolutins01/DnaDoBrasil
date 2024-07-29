using Infraero.Relprev.CrossCutting.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Plugins;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Claims;
using WebApp.Areas.Identity.Models;
using WebApp.Configuration;
using WebApp.Data;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.Controllers
{
    public class PerfilController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptions<UrlSettings> _appSettings;

        public PerfilController(ApplicationDbContext db,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager, IOptions<UrlSettings> appSettings)
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
            var responseModulos = ApiClientFactory.Instance.GetModulosAll();

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
                var responseModulos = ApiClientFactory.Instance.GetModulosAll();

                foreach (var modulo in responseModulos)
                {
                    var funcionalidades = modulo.Funcionalidades.FirstOrDefault().Nome.Split(',');
                    var listValue = funcionalidades
                        .Where(func => collection[modulo.Nome + func].ToString() == "on").ToList();
                    list.Add(modulo.Nome, string.Join(",", listValue));
                }

                var command = new PerfilModel.CreateUpdateCommand()
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

                command.AspNetRoleId = adminRole.Id;

                await ApiClientFactory.Instance.CreatePerfil(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        //[ClaimsAuthorize("Perfil", "Alterar")]
        public ActionResult Edit(int id)
        {
            var perfil = ApiClientFactory.Instance.GetPerfilById(id);

            if (id != null)
            {
                var obj = GetPerfilByAspNetRoleId(perfil.AspNetRoleId);
                var responseModulos = ApiClientFactory.Instance.GetModulosAll();

                var listClaim = new List<Claim>();

                foreach (DictionaryEntry item in obj.Claims)
                {
                    listClaim.Add(new Claim(item.Key.ToString(), item.Value.ToString()));
                }

                var model = new PerfilModel { Perfil = perfil, Modulos = responseModulos, Claims = listClaim };
                return View(model);
            }

            return RedirectToAction(nameof(Index),
                new
                {
                    notify = 2,
                    message = "Erro ao alterar o perfil. Favor entrar em contato com o administrador do sistema."
                });
        }

        //[ClaimsAuthorize("Perfil", "Alterar")]
        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                ListDictionary list = new ListDictionary();
                var responseModulos = ApiClientFactory.Instance.GetModulosAll();
                var perfil = ApiClientFactory.Instance.GetPerfilById(id);

                foreach (var modulo in responseModulos)
                {
                    var ListFuncionalidades = modulo.Funcionalidades.FirstOrDefault().Nome.Split(',');
                    var listValue = ListFuncionalidades
                        .Where(func => collection[modulo.Nome + func].ToString() == "on").ToList();
                    list.Add(modulo.Nome, string.Join(",", listValue));
                }

                var command = new PerfilModel.CreateUpdateCommand
                {
                    Id = perfil.Id,
                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
                    Claims = list,
                    AspNetRoleId = perfil.AspNetRoleId
                };

                var adminRole = await _roleManager.FindByIdAsync(perfil.AspNetRoleId);

                if (adminRole != null)
                {
                    adminRole.Name = command.Nome;
                    var resRole = await _roleManager.UpdateAsync(adminRole);

                    var role = _roleManager.Roles.Single(x => x.Id == command.AspNetRoleId);
                    var claims = _roleManager.GetClaimsAsync(role).Result;

                    foreach (var claim in claims)
                    {
                        var res = await _roleManager.RemoveClaimAsync(adminRole, claim);
                    }

                    foreach (DictionaryEntry claim in command.Claims)
                    {
                        var addClaim = new Claim(claim.Key.ToString(), claim.Value.ToString());
                        var res = await _roleManager.AddClaimAsync(adminRole, addClaim);
                    }

                    await ApiClientFactory.Instance.UpdatePerfil(id, command);

                    return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
                }
                else
                {
                    return RedirectToAction(nameof(Index),
                        new
                        {
                            notify = 2,
                            message = "Erro ao alterar o perfil. Favor entrar em contato com o administrador do sistema."
                        });
                }
            }
            catch
            {
                return View();
            }
        }

        //[ClaimsAuthorize("Perfil", "Excluir")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var perfil = ApiClientFactory.Instance.GetPerfilById(id);

                var obj = _db.Roles.Where(x => x.Id == perfil.AspNetRoleId).FirstOrDefault();

                var result = await _roleManager.DeleteAsync(obj);

                ApiClientFactory.Instance.DeletePerfil(id);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });

                //if (!result.Succeeded)
                //{
                //    return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = "Existe  usuário vinculado a esse perfil." });
                //}
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private PerfilDto GetPerfilByAspNetRoleId(string aspNetRoleId)
        {
            var entity = _db.Roles.Where(x => x.Id == aspNetRoleId).Select(item => new PerfilDto { Nome = item.Name, AspNetRoleId = item.Id }).FirstOrDefault();
            var role = _roleManager.Roles.Single(x => x.Id == entity.AspNetRoleId);
            var claims = _roleManager.GetClaimsAsync(role).Result;
            ListDictionary list = new ListDictionary();
            foreach (var claim in claims)
            {
                list.Add(claim.Type, claim.Value);
            }

            entity.Claims = list;

            return entity;
        }
    }
}
