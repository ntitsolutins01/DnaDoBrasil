using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Dto;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class TesteLaudoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public TesteLaudoController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public IActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);
            var response = ApiClientFactory.Instance.GetTesteLaudoAll();

            return View(new TesteLaudoModel() { TesteLaudos = response });
        }

        //[ClaimsAuthorize("ConfiguracaoSistema", "Incluir")]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var command = new TesteLaudoModel.CreateUpdateTesteLaudoCommand
                {
                    Nome = collection["nome"].ToString(),
                    Vo2MaxIni = Convert.ToInt32(collection["vo2MaxIni"].ToString()),
                    Vo2MaxFim = Convert.ToInt32(collection["vo2MaxFim"].ToString()),
                    VinteMetrosIni = Convert.ToInt32(collection["vinteMetrosIni"].ToString()),
                    VinteMetrosFim = Convert.ToInt32(collection["vinteMetrosFim"].ToString()),
                    ShutlleRunIni = Convert.ToInt32(collection["shutlleRunIni"].ToString()),
                    ShutlleRunFim = Convert.ToInt32(collection["shutlleRunFim"].ToString()),
                    FlexibilidadeIni = Convert.ToInt32(collection["flexibilidadeIni"].ToString()),
                    FlexibilidadeFim = Convert.ToInt32(collection["flexibilidadeFim"].ToString()),
                    PreensaoManualIni = Convert.ToInt32(collection["preensaoManualIni"].ToString()),
                    PreensaoManualFim = Convert.ToInt32(collection["preensaoManualFim"].ToString()),
                    AbdominalPranchaIni = Convert.ToInt32(collection["abdominalPranchaIni"].ToString()),
                    AbdominalPranchaFim = Convert.ToInt32(collection["abdominalPranchaFim"].ToString()),
                    ImpulsaoIni = Convert.ToInt32(collection["impulsaoIni"].ToString()),
                    ImpulsaoFim = Convert.ToInt32(collection["impulsaoFim"].ToString()),
                    EnvergaduraIni = Convert.ToInt32(collection["envergaduraIni"].ToString()),
                    EnvergaduraFim = Convert.ToInt32(collection["envergaduraFim"].ToString()),
                    PesoIni = Convert.ToInt32(collection["pesoIni"].ToString()),
                    PesoFim = Convert.ToInt32(collection["pesoFim"].ToString()),
                    AlturaIni = Convert.ToInt32(collection["alturaIni"].ToString()),
                    AlturaFim = Convert.ToInt32(collection["alturaFim"].ToString()),
                    Status = true
                };

                await ApiClientFactory.Instance.CreateTesteLaudo(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> Edit(IFormCollection collection)
        {
            var command = new TesteLaudoModel.CreateUpdateTesteLaudoCommand
            {
                Id = Convert.ToInt32(collection["editTesteLaudoId"]),
                Nome = collection["nome"].ToString(),
                Vo2MaxIni = Convert.ToInt32(collection["vo2MaxIni"].ToString()),
                Vo2MaxFim = Convert.ToInt32(collection["vo2MaxFim"].ToString()),
                VinteMetrosIni = Convert.ToInt32(collection["vinteMetrosIni"].ToString()),
                VinteMetrosFim = Convert.ToInt32(collection["vinteMetrosFim"].ToString()),
                ShutlleRunIni = Convert.ToInt32(collection["shutlleRunIni"].ToString()),
                ShutlleRunFim = Convert.ToInt32(collection["shutlleRunFim"].ToString()),
                FlexibilidadeIni = Convert.ToInt32(collection["flexibilidadeIni"].ToString()),
                FlexibilidadeFim = Convert.ToInt32(collection["flexibilidadeFim"].ToString()),
                PreensaoManualIni = Convert.ToInt32(collection["preensaoManualIni"].ToString()),
                PreensaoManualFim = Convert.ToInt32(collection["preensaoManualFim"].ToString()),
                AbdominalPranchaIni = Convert.ToInt32(collection["abdominalPranchaIni"].ToString()),
                AbdominalPranchaFim = Convert.ToInt32(collection["abdominalPranchaFim"].ToString()),
                ImpulsaoIni = Convert.ToInt32(collection["impulsaoIni"].ToString()),
                ImpulsaoFim = Convert.ToInt32(collection["impulsaoFim"].ToString()),
                EnvergaduraIni = Convert.ToInt32(collection["envergaduraIni"].ToString()),
                EnvergaduraFim = Convert.ToInt32(collection["envergaduraFim"].ToString()),
                PesoIni = Convert.ToInt32(collection["pesoIni"].ToString()),
                PesoFim = Convert.ToInt32(collection["pesoFim"].ToString()),
                AlturaIni = Convert.ToInt32(collection["alturaIni"].ToString()),
                AlturaFim = Convert.ToInt32(collection["alturaFim"].ToString()),
                Status = collection["editStatus"].ToString() == "" ? false : true
            };

            await ApiClientFactory.Instance.UpdateTesteLaudo(command.Id, command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }

        //[ClaimsAuthorize("Usuario", "Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiClientFactory.Instance.DeleteTesteLaudo(id);
                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Deleted });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public Task<TesteLaudoDto> GetTesteLaudoById(int id)
        {
            var result = ApiClientFactory.Instance.GetTesteLaudoById(id);

            return Task.FromResult(result);
        }
    }
}
