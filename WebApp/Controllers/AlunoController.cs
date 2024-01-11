using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class AlunoController : BaseController
    {
        private readonly IOptions<UrlSettings> _appSettings;

        public AlunoController(IOptions<UrlSettings> app)
        {
            _appSettings = app;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
        }

        public ActionResult Index(int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var response = ApiClientFactory.Instance.GetAlunosAll();
                var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeAll(), "Id", "Nome");
                var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll(), "Id", "Nome");

                return View(new AlunoModel()
                {
                    Alunos = response,
                    ListLocalidades = localidades,
                    ListDeficiencias = deficiencias
                });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        //public ActionResult Create()
        //{


        //    return View();
        //}

        //monta tela de create aluno
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");
            var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll(), "Id", "Nome");
            var ambientes = new SelectList(ApiClientFactory.Instance.GetAmbienteAll(), "Id", "Nome");

            return View(new AlunoModel()
            {
                ListEstados = estados,
                ListDeficiencias = deficiencias,
                ListAmbientes = ambientes
            });
        }
        public IActionResult Laudo()
        {
            return View();
        }
        public IActionResult TalentoEsportivo()
        {

            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateTalentoEsportivo(IFormCollection collection)
        {
            try
            {
                var command = new TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand
                {
                    Flexibilidade = Convert.ToInt32(collection["flexibilidade"]),
                    PreensaoManual = Convert.ToInt32(collection["preensaoManual"]),
                    Velocidade = Convert.ToInt32(collection["velocidade"]),
                    ImpulsaoHorizontal = Convert.ToInt32(collection["impulsaoHorizontal"]),
                    AptidaoFisica = Convert.ToInt32(collection["aptdaoFisica"]),
                    Agilidade = Convert.ToInt32(collection["agilidade"]),
                    Abdominal = Convert.ToInt32(collection["abdominal"])
                };

                await ApiClientFactory.Instance.CreateTalentoEsportivo(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditTalentoEsportivo(string id, IFormCollection collection)
        {
            var command = new TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand
            {
                Id = Convert.ToInt32(id),
                Flexibilidade = Convert.ToInt32(collection["flexibilidade"]),
                PreensaoManual = Convert.ToInt32(collection["preensaoManual"]),
                Velocidade = Convert.ToInt32(collection["velocidade"]),
                ImpulsaoHorizontal = Convert.ToInt32(collection["impulsaoHorizontal"]),
                AptidaoFisica = Convert.ToInt32(collection["aptdaoFisica"]),
                Agilidade = Convert.ToInt32(collection["agilidade"]),
                Abdominal = Convert.ToInt32(collection["abdominal"])
            };

            //await ApiClientFactory.Instance.UpdateTalentoEsportivo(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }
        public IActionResult Vocacional()
        {
            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateVocacional(IFormCollection collection)
        {
            try
            {
                var command = new VocacionalModel.CreateUpdateVocacionalCommand
                {
                    ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                    QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                    Resposta = Convert.ToString(collection["profissionalId"])
                };

                await ApiClientFactory.Instance.CreateVocacional(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditVocacional(string id, IFormCollection collection)
        {
            var command = new VocacionalModel.CreateUpdateVocacionalCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["profissionalId"])

            };

            //await ApiClientFactory.Instance.UpdateVocacional(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }



        public IActionResult QualidadeVida()
        {
            return View();
        }


        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateQualidadeVida(IFormCollection collection)
        {
            try
            {
                var command = new QualidadeVidaModel.CreateUpdateQualidadeVidaCommand
                {
                    ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                    QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                    Resposta = Convert.ToString(collection["profissionalId"])
                };

                await ApiClientFactory.Instance.CreateQualidadeVida(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditQualidadeVida(string id, IFormCollection collection)
        {
            var command = new QualidadeVidaModel.CreateUpdateQualidadeVidaCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["profissionalId"])

            };

            //await ApiClientFactory.Instance.UpdateQualidadeVida(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }

        public IActionResult Saude()
        {
            return View();
        }


        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateSaude(IFormCollection collection)
        {
            try
            {
                var command = new SaudeModel.CreateUpdateSaudeCommand
                {
                    ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                    Altura = Convert.ToInt32(collection["altura"]),
                    Massa = Convert.ToInt32(collection["massa"]),
                    Envergadura = Convert.ToInt32(collection["envergadura"])


                };

                await ApiClientFactory.Instance.CreateSaude(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditSaude(string id, IFormCollection collection)
        {
            var command = new SaudeModel.CreateUpdateSaudeCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                Altura = Convert.ToInt32(collection["altura"]),
                Massa = Convert.ToInt32(collection["massa"]),
                Envergadura = Convert.ToInt32(collection["envergadura"])

            };

            //await ApiClientFactory.Instance.UpdateSaude(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }
        public IActionResult ConsumoAlimentar()
        {
            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateConsumoAlimentar(IFormCollection collection)
        {
            try
            {
                var command = new ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand
                {
                    ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                    QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                    Resposta = Convert.ToString(collection["resposta"])
                };

                await ApiClientFactory.Instance.CreateConsumoAlimentar(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditConsumoAlimentar(string id, IFormCollection collection)
        {
            var command = new ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["resposta"])

            };

            //await ApiClientFactory.Instance.UpdateConsumoAlimentar(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }
        public IActionResult SaudeBucal()
        {
            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateSaudeBucal(IFormCollection collection)
        {
            try
            {
                var command = new SaudeBucalModel.CreateUpdateSaudeBucalCommand
                {
                    ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                    QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                    Resposta = Convert.ToString(collection["resposta"])
                };

                await ApiClientFactory.Instance.CreateSaudeBucal(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditSaudeBucal(string id, IFormCollection collection)
        {
            var command = new SaudeBucalModel.CreateUpdateSaudeBucalCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["resposta"])

            };

            //await ApiClientFactory.Instance.UpdateSaudeBucal(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }


        public IActionResult Dados()
        {
            return View();
        }


        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateDados(IFormCollection collection)
        {
            try
            {
                var status = collection["status"].ToString();
                var habilitado = collection["habilitado"].ToString();

                var command = new DadosModel.CreateUpdateDadosCommand
                {
                    Etnia = collection["ddlEtnia"] == "" ? null : Convert.ToInt32(collection["ddlEtnia"]),
                    MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    LocalidadeId = collection["ddlProfissionalAluno"] == "" ? null : Convert.ToInt32(collection["ddlProfissionalAluno"].ToString()),
                    Nome = collection["nome"] == "" ? null : collection["nome"].ToString(),
                    DtNascimento = collection["DtNascimento"] == "" ? null : collection["DtNascimento"].ToString(),
                    Email = collection["email"] == "" ? null : collection["email"].ToString(),
                    Sexo = collection["ddlSexo"] == "" ? null : collection["ddlSexo"].ToString(),
                    NomeMae = collection["nomeMae"] == "" ? null : Convert.ToString(collection["nomeMae"]),
                    NomePai = collection["nomePai"] == "" ? null : Convert.ToString(collection["nomePai"]),
                    Telefone = collection["numTelefone"] == "" ? null : collection["numTelefone"].ToString(),
                    Cep = collection["cep"] == "" ? null : collection["cep"].ToString(),
                    Celular = collection["numCelular"] == "" ? null : collection["numCelular"].ToString(),
                    Cpf = collection["cpf"] == "" ? null : collection["cpf"].ToString(),
                    Endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                    Numero = collection["numero"] == "" ? null : Convert.ToInt32(collection["numero"].ToString()),
                    Bairro = collection["bairro"] == "" ? null : collection["bairro"].ToString(),
                    DeficienciasIds = collection["arrDeficiencias"] == "" ? null : collection["arrDeficiencias"].ToString(),
                    Habilitado = habilitado != "",
                    Status = status != ""

                };

                await ApiClientFactory.Instance.CreateDados(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditDados(int id, IFormCollection collection)
        {
            try
            {

                var status = collection["status"].ToString();
                var habilitado = collection["habilitado"].ToString();
                var ambientesIds = collection["arrAmbientes"];


                var command = new DadosModel.CreateUpdateDadosCommand
                {
                    Id = Convert.ToInt32(id),
                    Etnia = collection["ddlEtnia"] == "" ? null : Convert.ToInt32(collection["ddlEtnia"]),
                    MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    Nome = collection["nome"] == "" ? null : collection["nome"].ToString(),
                    DtNascimento = collection["DtNascimento"] == "" ? null : collection["DtNascimento"].ToString(),
                    Email = collection["email"] == "" ? null : collection["email"].ToString(),
                    Sexo = collection["ddlSexo"] == "" ? null : collection["ddlSexo"].ToString(),
                    NomeMae = collection["nomeMae"] == "" ? null : Convert.ToString(collection["nomeMae"]),
                    NomePai = collection["nonomePaimePai"] == "" ? null : Convert.ToString(collection["nomePai"]),
                    Telefone = collection["numTelefone"] == "" ? null : collection["numTelefone"].ToString(),
                    Cep = collection["cep"] == "" ? null : collection["cep"].ToString(),
                    Celular = collection["numCelular"] == "" ? null : collection["numCelular"].ToString(),
                    Cpf = collection["cpf"] == "" ? null : collection["cpf"].ToString(),
                    Endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                    Numero = collection["numero"] == "" ? null : Convert.ToInt32(collection["numero"].ToString()),
                    Bairro = collection["bairro"] == "" ? null : collection["bairro"].ToString(),
                    DeficienciasId = collection["deficienciasId"] == "" ? null : Convert.ToInt32(collection["deficienciasId"]),
                    Habilitado = habilitado != "",
                    Status = status != "",
                    //ContratosId = collection["contratosId"] == "" ? null : Convert.ToInt32(collection["contratosId"]),
                    //RedeSocial = Convert.ToString(collection["redeSocial"]),
                    //MatriculaId = Convert.ToInt32(collection["matriculaId"]),
                    //VoucherId = Convert.ToInt32(collection["voucherId"]),
                    //DependenciaId = Convert.ToInt32(collection["dependenciaId"]),
                    //LaudosId = Convert.ToInt32(collection["laudosId"])
                    ////Url = Convert.ToString(collection["url"]),
                    //AmbientesIds = collection["arrAmbientes"] == "" ? null : collection["arrAmbientes"].ToString(),
                    //ParceiroId = collection["parceiroId"] == "" ? null : Convert.ToInt32(collection["parceiroId"]),
                    //AspNetUserId = Convert.ToInt32(collection["aspnetUserId"]),

                };

                //await ApiClientFactory.Instance.UpdateDados(command);

                return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index)));
            }
        }

        public IActionResult Voucher()
        {
            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateVoucher(IFormCollection collection)
        {
            try
            {
                var command = new VoucherModel.CreateUpdateVoucherCommand
                {
                    LocalId = Convert.ToInt32(collection["localId"]),
                    Descricao = Convert.ToString(collection["descricao"]),
                    Turma = Convert.ToString(collection["turma"]),
                    Serie = Convert.ToString(collection["serie"]),
                    AlunoId = Convert.ToInt32(collection["alunoId"])
                };

                await ApiClientFactory.Instance.CreateVoucher(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditVoucher(string id, IFormCollection collection)
        {
            var command = new VoucherModel.CreateUpdateVoucherCommand
            {
                Id = Convert.ToInt32(id),
                LocalId = Convert.ToInt32(collection["localId"]),
                Descricao = Convert.ToString(collection["descricao"]),
                Turma = Convert.ToString(collection["turma"]),
                Serie = Convert.ToString(collection["serie"]),
                AlunoId = Convert.ToInt32(collection["alunoId"])

            };

            //await ApiClientFactory.Instance.UpdateVoucher(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }


        public IActionResult Matricula()
        {
            return View();
        }

        //[ClaimsAuthorize("Usuario", "Incluir")]
        [HttpPost]
        public async Task<ActionResult> CreateMatricula(IFormCollection collection)
        {
            try
            {
                var command = new MatriculaModel.CreateUpdateMatriculaCommand
                {
                    DtVencimentoParq = Convert.ToDateTime(collection["dtVencimentoParq"].ToString()),
                    DtVencimentoAtestadoMedico = Convert.ToDateTime(collection["diVencimentoAtestadoMedico"].ToString()),
                    NomeResponsavel1 = Convert.ToString(collection["nomeResponsavel1"]),
                    ParentescoResponsavel1 = Convert.ToString(collection["parentescoResponsavel1"]),
                    CpfResponsavel1 = Convert.ToString(collection["cpfResponsavel1"]),
                    NomeResponsavel2 = Convert.ToString(collection["nomeResponsavel2"]),
                    ParentescoResponsavel2 = Convert.ToString(collection["parentescoResponsavel2"]),
                    CpfResponsavel2 = Convert.ToString(collection["cpfResponsavel2"]),
                    NomeResponsavel3 = Convert.ToString(collection["nomeResponsavel3"]),
                    ParentescoResponsavel3 = Convert.ToString(collection["parentescoResponsavel3"]),
                    CpfResponsavel3 = Convert.ToString(collection["cpfResponsavel3"]),
                    LocalId = Convert.ToInt32(collection["localId"]),
                    AlunoId = Convert.ToInt32(collection["alunoId"])

                };

                await ApiClientFactory.Instance.CreateMatricula(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public Task<ActionResult> EditMatricula(string id, IFormCollection collection)
        {
            var command = new MatriculaModel.CreateUpdateMatriculaCommand
            {
                Id = Convert.ToInt32(id),
                DtVencimentoParq = Convert.ToDateTime(collection["dtVencimentoParq"].ToString()),
                DtVencimentoAtestadoMedico = Convert.ToDateTime(collection["diVencimentoAtestadoMedico"].ToString()),
                NomeResponsavel1 = Convert.ToString(collection["nomeResponsavel1"]),
                ParentescoResponsavel1 = Convert.ToString(collection["parentescoResponsavel1"]),
                CpfResponsavel1 = Convert.ToString(collection["cpfResponsavel1"]),
                NomeResponsavel2 = Convert.ToString(collection["nomeResponsavel2"]),
                ParentescoResponsavel2 = Convert.ToString(collection["parentescoResponsavel2"]),
                CpfResponsavel2 = Convert.ToString(collection["cpfResponsavel2"]),
                NomeResponsavel3 = Convert.ToString(collection["nomeResponsavel3"]),
                ParentescoResponsavel3 = Convert.ToString(collection["parentescoResponsavel3"]),
                CpfResponsavel3 = Convert.ToString(collection["cpfResponsavel3"]),
                LocalId = Convert.ToInt32(collection["localId"]),
                AlunoId = Convert.ToInt32(collection["alunoId"])

            };

            //await ApiClientFactory.Instance.UpdateMatricula(command);

            return Task.FromResult<ActionResult>(RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated }));
        }


        public Task<JsonResult> GetAlunosByLocalidade(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Localidade não informado.");
                var resultLocal = ApiClientFactory.Instance.GetAlunosByLocalidade(Convert.ToInt32(id));

                return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Nome")));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex));
            }
        }

        public IActionResult CreateDependencia()
        {
            throw new NotImplementedException();
        }

        //[ClaimsAuthorize("Aluno", "Alterar")]
        [HttpPost]
        public async Task<ActionResult> CreateAmbientesAluno(IFormCollection collection)
        {
            try
            {
                var ambiente = collection["ddlAmbienteAluno"].ToString();

                var command = new AmbienteModel.CreateUpdateAmbienteCommand
                {
                    Nome = ambiente
                };

                await ApiClientFactory.Instance.CreateAmbiente(command);

                return RedirectToAction(nameof(Create), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        public IActionResult CreateDeficiencia()
        {
            throw new NotImplementedException();
        }
    }
}
