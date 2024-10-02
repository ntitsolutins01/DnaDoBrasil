using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Factory;
using WebApp.Identity;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    [Authorize(Policy = ModuloAccess.DashboardEad)]
    public class DashboardEadController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;


        public DashboardEadController(ILogger<DashboardEadController> logger, IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
            
        }
        public async Task<IActionResult> Index(IFormCollection collection)
        {
            var usu = User?.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            var email = User.FindFirstValue(ClaimTypes.Email);
            var token = HttpContext.Request.Cookies["token"];
            //ApplicationSettings.Token = HttpContext.Request.Cookies["token"];

            return View();
        }
    }
}
