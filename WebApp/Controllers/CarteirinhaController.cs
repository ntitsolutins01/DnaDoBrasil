using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Authorization;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Identity;
using WebApp.Models;
using WebApp.Utility;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using QRCoder;
using Claim = WebApp.Identity.Claim;
using NuGet.Protocol.Core.Types;

namespace WebApp.Controllers
{
    public class CarteirinhaController : Controller
    {
        [HttpGet]
        public IActionResult ImprimirCarteirinha(int id)
        {
            var model = new AlunoModel
            {
                Aluno = new Dto.AlunoDto { Id = id }
            };

            return View("ImprimirCarteirinha", model);
        }
    }
}