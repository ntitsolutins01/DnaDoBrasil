using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;
using WebApp.Authorization;
using Claim = WebApp.Identity.Claim;
using WebApp.Identity;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Core.Types;
using WebApp.Dto;

namespace WebApp.Controllers
{
    [Authorize(Policy = ModuloAccess.Laudo)]
    public class LaudoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public LaudoController(IOptions<UrlSettings> appSettings)
        {
            _appSettings = appSettings;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        [ClaimsAuthorize(ClaimType.Laudo, Claim.Consultar)]
        public IActionResult Index(int? crud, int? notify, IFormCollection collection, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var searchFilter = new LaudosFilterDto()
                {
                    FomentoId = collection["ddlFomento"].ToString(),
                    Estado = collection["ddlEstado"].ToString(),
                    MunicipioId = collection["ddlMunicipio"].ToString(),
                    LocalidadeId = collection["ddlLocalidade"].ToString(),
                    TipoLaudoId = collection["ddlTipoLaudo"].ToString(),
                    AlunoId = collection["ddlAluno"].ToString(),
                    PageNumber = 1,
                    PageSize = 10
                };

                //var response = ApiClientFactory.Instance.GetLaudosByFilter(searchFilter);
                var response = ApiClientFactory.Instance.GetLaudosByFilter(searchFilter);

                var fomentos = new SelectList(ApiClientFactory.Instance.GetFomentoAll(), "Id", "Nome", searchFilter.FomentoId);
                var tiposLaudos = new SelectList(ApiClientFactory.Instance.GetTiposLaudoAll(), "Id", "Nome", searchFilter.TipoLaudoId);
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", searchFilter.Estado);

                SelectList municipios = null;

                if (!string.IsNullOrEmpty(searchFilter.Estado))
                {
                    municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(searchFilter.Estado), "Id", "Nome", searchFilter.MunicipioId);
                }
                SelectList localidades = null;

                if (!string.IsNullOrEmpty(searchFilter.LocalidadeId))
                {
                    localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeByMunicipio(searchFilter.MunicipioId), "Id", "Nome", searchFilter.LocalidadeId);
                }
                var model = new LaudoModel()
                {
                    Laudos = response.Result.Laudos,
                    ListFomentos = fomentos,
                    ListEstados = estados,
                    ListTiposLaudos = tiposLaudos,
                    ListMunicipios = municipios!,
                    ListLocalidades = localidades!,
                };

                return View(model);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        [ClaimsAuthorize(ClaimType.Laudo, Claim.Detalhar)]
        public ActionResult Details(int id)
        {
            var laudo = ApiClientFactory.Instance.GetLaudoByAluno(id);
            var aluno = ApiClientFactory.Instance.GetAlunoById(id);
            var profissional = ApiClientFactory.Instance.GetProfissionalById(Convert.ToInt32(aluno.ProfissionalId));
            var talentoEsportivo = laudo.TalentoEsportivoId == null ? null : ApiClientFactory.Instance.GetTalentoEsportivoByAluno((int)laudo.AlunoId!);
            var encaminhamentoImc = laudo.SaudeId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoBySaudeId(Convert.ToInt32(laudo.SaudeId));
            var qualidadeDeVida = laudo.QualidadeDeVidaId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoByQualidadeDeVidaId((int)laudo.QualidadeDeVidaId);
            var vocacional = laudo.VocacionalId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoByVocacional();
            var encaminhamentoConsumoAlimentar = laudo.ConsumoAlimentarId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoById((int)laudo.ConsumoAlimentarId);
            var encaminhamentoSaudeBucal = laudo.SaudeBucalId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoById((int)laudo.SaudeBucalId);
            var desempenho = ApiClientFactory.Instance.GetDesempenhoByAluno(Convert.ToInt32(laudo.AlunoId));

            var model = new LaudoModel()
            {
                Laudo = laudo,
                Aluno = aluno,
                Profissional = profissional,
                TalentoEsportivo = talentoEsportivo,
                EncaminhamentoImc = encaminhamentoImc,
                QualidadeDeVida = qualidadeDeVida,
                Vocacional = vocacional,
                EncaminhamentoSaudeBucal = encaminhamentoSaudeBucal,
                EncaminhamentoConsumoAlimentar = encaminhamentoConsumoAlimentar,
                Desempenho = desempenho
            };
            return View(model);
        }

        //[ClaimsAuthorize(ClaimType.Laudo, Claim.Ver)]
        public ActionResult Report(int id)
        {
            var laudo = ApiClientFactory.Instance.GetLaudoByAluno(id);
            var talentoEsportivo = laudo.TalentoEsportivoId == null ? null : ApiClientFactory.Instance.GetTalentoEsportivoByAluno((int)laudo.AlunoId!);
            var encaminhamentoImc = laudo.SaudeId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoBySaudeId(Convert.ToInt32(laudo.SaudeId));
            var qualidadeDeVida = laudo.QualidadeDeVidaId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoByQualidadeDeVidaId((int)laudo.QualidadeDeVidaId);
            var vocacional = laudo.VocacionalId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoByVocacional();
            var encaminhamentoConsumoAlimentar = laudo.ConsumoAlimentarId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoById((int)laudo.ConsumoAlimentarId);
            var encaminhamentoSaudeBucal = laudo.SaudeBucalId == null ? null : ApiClientFactory.Instance.GetEncaminhamentoById((int)laudo.SaudeBucalId);
            var desempenho = ApiClientFactory.Instance.GetDesempenhoByAluno(Convert.ToInt32(laudo.AlunoId));

            var model = new LaudoModel()
            {
                Laudo = laudo,
                TalentoEsportivo = talentoEsportivo,
                EncaminhamentoImc = encaminhamentoImc,
                QualidadeDeVida = qualidadeDeVida,
                Vocacional = vocacional,
                EncaminhamentoSaudeBucal = encaminhamentoSaudeBucal,
                EncaminhamentoConsumoAlimentar = encaminhamentoConsumoAlimentar,
                Desempenho = desempenho
            };
            return View(model);
        }

        [ClaimsAuthorize(ClaimType.Laudo, Claim.Incluir)]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");

                var questionarioVocacional =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.Vocacional).OrderBy(o=>o.Questao).ToList();
                var questionarioQualidadeVida =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.QualidadeVida).OrderBy(o => o.Questao).ToList();
                var questionarioConsumoAlimentar =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.ConsumoAlimentar).OrderBy(o => o.Questao).ToList();
                var questionarioSaudeBucal =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.SaudeBucal).OrderBy(o => o.Questao).ToList();


                return View(new LaudoModel()
                {
                    QuestionarioVocacional = questionarioVocacional,
                    QuestionarioQualidadeVida = questionarioQualidadeVida,
                    QuestionarioConsumoAlimentar = questionarioConsumoAlimentar,
                    QuestionarioSaudeBucal = questionarioSaudeBucal,
                    ListEstados = estados
                });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimType.Laudo, Claim.Incluir)]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                if (string.IsNullOrEmpty(collection["ddlAluno"].ToString()))
                {
                    return RedirectToAction(nameof(Create), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor Informar o Aluno." });
                }

                var command = new LaudoModel.CreateUpdateLaudoCommand
                {
                    AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString())
                };

                var listVocacional = (from item in collection where item.Key.Contains("nomeRespVocacional") select item.Value).Select(v => (string)v).ToList();

                var listQualidadeDeVida = (from item in collection where item.Key.Contains("nomeRespQualidadeVida") select item.Value).Select(qv => (string)qv).ToList();

                var listConsumoAlimentar = (from item in collection where item.Key.Contains("nomeRespConsumoAlimentar") select item.Value).Select(ca => (string)ca).ToList();

                var listSaudeBucal = (from item in collection where item.Key.Contains("nomeRespSaudeBucal") select item.Value).Select(sb => (string)sb).ToList();

                if (listVocacional.Any())
                {
                    var totalRespVocacional = ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.Vocacional).Count;

                    if (listVocacional.Count != totalRespVocacional)
                    {
                        return RedirectToAction(nameof(Create), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor responder todas as perguntas do questionário vocacional." });
                    }

                    command.VocacionalId = (int)await ApiClientFactory.Instance.CreateVocacional(
                        new VocacionalModel.CreateUpdateVocacionalCommand()
                        {
                            Respostas = string.Join(",",listVocacional),
                            ProfissionalId = Convert.ToInt32(collection["ddlProfissional"].ToString()),
                            AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString()),
                            StatusVocacional = listVocacional.Count == totalRespVocacional ? "F" : "A"
                        });
                }

                if (listQualidadeDeVida.Any())
                {
                    var totalRespQualidadeDeVida = ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.QualidadeVida).Count;

                    if (listQualidadeDeVida.Count != totalRespQualidadeDeVida)
                    {
                        return RedirectToAction(nameof(Create), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor responder todas as perguntas do questionário de qualidade de vida." });
                    }

                    command.QualidadeDeVidaId = (int)await ApiClientFactory.Instance.CreateQualidadeVida(
                        new QualidadeVidaModel.CreateUpdateQualidadeVidaCommand()
                        {
                            Respostas = string.Join(",", listQualidadeDeVida),
                            ProfissionalId = Convert.ToInt32(collection["ddlProfissional"].ToString()),
                            AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString()),
                            StatusQualidadeDeVida = listQualidadeDeVida.Count == totalRespQualidadeDeVida ? "F" : "A"
                        });
                }

                if (listConsumoAlimentar.Any())
                {
                    var totalRespConsumoAlimentar = ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.ConsumoAlimentar).Count;

                    if (listConsumoAlimentar.Count != totalRespConsumoAlimentar)
                    {
                        return RedirectToAction(nameof(Create), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor responder todas as perguntas do questionário de consumo alimentar." });
                    }

                    command.ConsumoAlimentarId = (int)await ApiClientFactory.Instance.CreateConsumoAlimentar(
                        new ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand()
                        {
                            Respostas = string.Join(",", listConsumoAlimentar),
                            ProfissionalId = Convert.ToInt32(collection["ddlProfissional"].ToString()),
                            AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString()),
                            StatusConsumoAlimentar = listConsumoAlimentar.Count == totalRespConsumoAlimentar ? "F" : "A"
                        });
                }

                if (listSaudeBucal.Any())
                {
                    var totalRespSaudeBucal = ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.SaudeBucal).Count;

                    if (listSaudeBucal.Count != totalRespSaudeBucal)
                    {
                        return RedirectToAction(nameof(Create), new { notify = (int)EnumNotify.Error, message = "Erro ao executar esta ação. Favor responder todas as perguntas do questionário de saúde bucal." });
                    }

                    command.SaudeBucalId = (int)await ApiClientFactory.Instance.CreateSaudeBucal(
                        new SaudeBucalModel.CreateUpdateSaudeBucalCommand()
                        {
                            Respostas = string.Join(",", listSaudeBucal),
                            ProfissionalId = Convert.ToInt32(collection["ddlProfissional"].ToString()),
                            AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString()),
                            StatusSaudeBucal = listSaudeBucal.Count == totalRespSaudeBucal ? "F" : "A"
                        });
                }

                var statusSaude = String.Empty;
                var listPropSaude = collection.Where(item => item.Key.Contains("Saude"));
                foreach (var propSaude in listPropSaude)
                {
                    statusSaude = propSaude.Value == "" ? "A" : "F";
                }

                var commandSaude = new SaudeModel.CreateUpdateSaudeCommand()
                {
                    ProfissionalId = collection["ddlProfissional"] == "" ? null : Convert.ToInt32(collection["ddlProfissional"].ToString()),
                    AlunoId = collection["ddlAluno"] == "" ? null : Convert.ToInt32(collection["ddlAluno"].ToString()),
                    EnvergaduraSaude = collection["envergaduraSaude"] == "" ? null : Convert.ToInt32(collection["envergaduraSaude"].ToString()),
                    MassaCorporalSaude = collection["massaCorporalSaude"] == "" ? null : Convert.ToInt32(collection["massaCorporalSaude"].ToString()),
                    AlturaSaude = collection["alturaSaude"] == "" ? null : Convert.ToInt32(collection["alturaSaude"].ToString()),
                    StatusSaude = statusSaude
                };

                command.SaudeId = (int)await ApiClientFactory.Instance.CreateSaude(commandSaude);

                var statusTalentoEsportivo = String.Empty;
                var listPropTalentoEsportivo = collection.Where(item => item.Key.Contains("TalentoEsportivo"));
                foreach (var propTalentoEsportivo in listPropTalentoEsportivo)
                {
                    statusTalentoEsportivo = propTalentoEsportivo.Value == "" ? "A" : "F";
                }

                var commandTalentoEsportivo = new TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand()
                {
                    ProfissionalId = collection["ddlProfissional"] == "" ? null : Convert.ToInt32(collection["ddlProfissional"].ToString()),
                    AlunoId = collection["ddlAluno"] == "" ? null : Convert.ToInt32(collection["ddlAluno"].ToString()),
                    Altura = collection["altura"] == "" ? null : Convert.ToDecimal(collection["altura"].ToString()),
                    MassaCorporal = collection["massaCorporalSaude"] == "" ? null : Convert.ToInt32(collection["massaCorporalSaude"].ToString()),
                    PreensaoManual = collection["preensaoManual"] == "" ? null : Convert.ToDecimal(collection["preensaoManual"].ToString()),
                    Flexibilidade = collection["flexibilidade"] == "" ? null : Convert.ToDecimal(collection["flexibilidade"].ToString()),
                    ImpulsaoHorizontal = collection["impulsaoHorizontal"] == "" ? null : Convert.ToDecimal(collection["impulsaoHorizontal"].ToString()),
                    Velocidade = collection["testeVelocidade"] == "" ? null : Convert.ToDecimal(collection["testeVelocidade"].ToString()),
                    AptidaoFisica = collection["aptidaoFisica"] == "" ? null : Convert.ToDecimal(collection["aptidaoFisica"].ToString()),
                    Agilidade = collection["agilidade"] == "" ? null : Convert.ToDecimal(collection["agilidade"].ToString()),
                    Abdominal = Convert.ToBoolean(collection["rdbAbdominal"]),
                    StatusTalentosEsportivos = statusTalentoEsportivo
                };

                command.TalentoEsportivoId = (int)await ApiClientFactory.Instance.CreateTalentoEsportivo(commandTalentoEsportivo);

                await ApiClientFactory.Instance.CreateLaudo(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = $"Erro ao executar esta ação. Favor entrar em contato com o administrador do sistema.{e.Message}" });
            }
        }
        
        [HttpPost]
        [ClaimsAuthorize(ClaimType.Laudo, Claim.Alterar)]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var listVocacisonal = new List<string>();

                foreach (var item in collection)
                {
                    if (item.Key.Contains("nomeRespVocacional"))
                    {
                        listVocacisonal.Add(item.Value);
                    }
                }

                var listQualidadeDeVida = new List<string>();

                foreach (var item in collection)
                {
                    if (item.Key.Contains("nomeRespQualidadeVida"))
                    {
                        listQualidadeDeVida.Add(item.Value);
                    }
                }
                var listConsumoAlimentar = new List<string>();

                foreach (var item in collection)
                {
                    if (item.Key.Contains("nomeRespConsumoAlimentar"))
                    {
                        listConsumoAlimentar.Add(item.Value);
                    }
                }
                var listSaudeBucal = new List<string>();

                foreach (var item in collection)
                {
                    if (item.Key.Contains("nomeRespSaudeBucal"))
                    {
                        listSaudeBucal.Add(item.Value);
                    }
                }

                var command = new LaudoModel.CreateUpdateLaudoCommand
                {
                    //ImpulsaoHorizontal = Convert.ToDecimal(collection["impulsaoHorizontal"].ToString()),
                    //Flexibilidade = Convert.ToDecimal(collection["flexibilidade"].ToString()),
                    //PreensaoManual = Convert.ToDecimal(collection["preensaoManual"].ToString()),
                    //Velocidade = Convert.ToDecimal(collection["testeVelocidade"].ToString()),
                    //AptidaoFisica = Convert.ToDecimal(collection["aptidaoFisica"].ToString()),
                    //Agilidade = Convert.ToDecimal(collection["agilidade"].ToString()),
                    //Abdominal = Convert.ToBoolean(collection["abdominal"].ToString()),
                    //Altura = Convert.ToDecimal(collection["altura"].ToString()),
                    AlunoId = Convert.ToInt32(collection["ddlAluno"].ToString()),
                };

                await ApiClientFactory.Instance.UpdateLaudo(command.Id, command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [ClaimsAuthorize(ClaimType.Laudo, Claim.Alterar)]
        public ActionResult Edit(int id, int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var questionarioVocacional =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.Vocacional).OrderBy(o => o.Questao).ToList();
                var questionarioQualidadeVida =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.QualidadeVida).OrderBy(o => o.Questao).ToList();
                var questionarioConsumoAlimentar =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.ConsumoAlimentar).OrderBy(o => o.Questao).ToList();
                var questionarioSaudeBucal =
                    ApiClientFactory.Instance.GetQuestionarioByTipoLaudo((int)EnumTipoLaudo.SaudeBucal).OrderBy(o => o.Questao).ToList();

                var laudo = ApiClientFactory.Instance.GetLaudoById(id);

                var aluno = ApiClientFactory.Instance.GetAlunoById((int)laudo.AlunoId);

                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", aluno.Estado);

                var municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(aluno.Estado!), "Id", "Nome", aluno.MunicipioId);

                var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeByMunicipio(aluno.MunicipioId.ToString()), "Id", "Nome", aluno.LocalidadeId);

                var profissionais = new SelectList(ApiClientFactory.Instance.GetProfissionaisByLocalidade(Convert.ToInt32(aluno.LocalidadeId)), "Id", "Nome", aluno.ProfissionalId);

                var alunos = new SelectList(ApiClientFactory.Instance.GetAlunosByLocalidade(Convert.ToInt32(aluno.LocalidadeId)), "Id", "Nome", aluno.Id);

                var saude = new SaudeDto();

                if (laudo.SaudeId != null)
                {
                    saude = ApiClientFactory.Instance.GetSaudeById((int)laudo.SaudeId);
                }

                var talentoEsportivo = new TalentoEsportivoDto();

                if (laudo.TalentoEsportivoId != null)
                {
                    talentoEsportivo = ApiClientFactory.Instance.GetTalentoEsportivoById((int)laudo.TalentoEsportivoId);
                }


                return View(new LaudoModel()
                {
                    QuestionarioVocacional = questionarioVocacional,
                    QuestionarioQualidadeVida = questionarioQualidadeVida,
                    QuestionarioConsumoAlimentar = questionarioConsumoAlimentar,
                    QuestionarioSaudeBucal = questionarioSaudeBucal,

                    ListEstados = estados,
                    Aluno = aluno,
                    ListMunicipios = municipios,
                    ListLocalidades = localidades,
                    ListProfissionais = profissionais,
                    ListAlunos = alunos,
                    Saude = saude,
                    TalentoEsportivo = talentoEsportivo
                });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }
    }
}
