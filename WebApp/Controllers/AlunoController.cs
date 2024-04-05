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

namespace WebApp.Controllers
{
    [Authorize(Policy = ModuloAccess.Aluno)]
    public class AlunoController : BaseController
    {
        #region Constructor

        private readonly IOptions<UrlSettings> _appSettings;
        private readonly IHostingEnvironment _host;

        /// <summary>
        /// Construtor da página
        /// </summary>
        /// <param name="app">configurações de urls do sistema</param>
        /// <param name="host">informações da aplicação em execução</param>
        public AlunoController(IOptions<UrlSettings> app,
            IHostingEnvironment host)
        {
            _appSettings = app;
            ApplicationSettings.WebApiUrl = _appSettings.Value.WebApiBaseUrl;
            _host = host;
        }
        #endregion

        #region Crud Methods
        /// <summary>
        /// Listagem de Alunos
        /// </summary>
        /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
        /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
        /// <param name="collection">lista de filtros selecionados para pesquisa de alunos</param>
        /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Consultar)]
        public async Task<ActionResult> Index(int? crud, int? notify, IFormCollection collection, string message = null)
        {
            try
            {
                var user = User.Identity.Name;

                var usuario = ApiClientFactory.Instance.GetUsuarioByEmail(user);

                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);

                var searchFilter = new AlunosFilterDto
                {
                    FomentoId = collection["ddlFomento"].ToString(),
                    Estado = collection["ddlEstado"].ToString(),
                    MunicipioId = collection["ddlMunicipio"].ToString(),
                    LocalidadeId = collection["ddlLocalidade"].ToString(),
                    DeficienciaId = collection["ddlDeficiencia"].ToString(),
                    Etnia = collection["ddlEtnia"].ToString(),
                    Sexo = collection["ddlSexo"].ToString()
                };
                var result = await ApiClientFactory.Instance.GetAlunosByFilter(searchFilter);

                bool filtroVazio = string.IsNullOrEmpty(searchFilter.MunicipioId) ?
                        string.IsNullOrEmpty(searchFilter.FomentoId) ?
                            string.IsNullOrEmpty(searchFilter.LocalidadeId) ?
                                string.IsNullOrEmpty(searchFilter.Sexo) ?
                                    string.IsNullOrEmpty(searchFilter.DeficienciaId) ?
                                        string.IsNullOrEmpty(searchFilter.Estado) ?
                                            string.IsNullOrEmpty(searchFilter.Etnia) : false
                                        : false
                                    : false
                                : false
                            : false
                        : false;

                if (filtroVazio)
                {
                    result.Alunos = (List<AlunoIndexDto>?)result.Alunos.ToList()
                        .Where(x => x.MunicipioId == usuario.MunicipioId.ToString()).ToList();
                }

                var fomentos = new SelectList(ApiClientFactory.Instance.GetFomentoAll(), "Id", "Nome", searchFilter.FomentoId);
                var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll(), "Id", "Nome", searchFilter.DeficienciaId);
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", searchFilter.Estado);

                List<SelectListDto> listSexo = new List<SelectListDto>
                {
                    new() { IdNome = "MASCULINO", Nome = "MASCULINO" },
                    new() { IdNome = "FEMININO", Nome = "FEMININO" }
                };

                var sexos = new SelectList(listSexo, "IdNome", "Nome", searchFilter.Sexo);

                List<SelectListDto> list = new List<SelectListDto>
                {
                    new() { IdNome = "PARDO", Nome = "PARDO" },
                    new() { IdNome = "BRANCO", Nome = "BRANCO" },
                    new() { IdNome = "PRETO", Nome = "PRETO" },
                    new() { IdNome = "INDÍGENA", Nome = "INDÍGENA" },
                    new() { IdNome = "AMARELO", Nome = "AMARELO" }
                };

                var etnias = new SelectList(list, "IdNome", "Nome", searchFilter.Etnia);
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

                var model = new AlunoModel
                {
                    ListFomentos = fomentos,
                    ListEstados = estados,
                    ListDeficiencias = deficiencias,
                    ListMunicipios = municipios!,
                    ListEtnias = etnias,
                    ListLocalidades = localidades!,
                    Alunos = result.Alunos,
                    SearchFilter = searchFilter

                };
                return View(model);

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        /// <summary>
        /// Tela para inclusão de aluno
        /// </summary>
        /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
        /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
        /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Incluir)]
        public ActionResult Create(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome");
            var deficiencias = new SelectList(ApiClientFactory.Instance.GetDeficienciaAll(), "Id", "Nome");
            var modalidades = new SelectList(ApiClientFactory.Instance.GetModalidadeAll(), "Id", "Nome");
            List<SelectListDto> list = new List<SelectListDto>
            {
                new() { IdNome = "PARDO", Nome = "PARDO" },
                new() { IdNome = "BRANCO", Nome = "BRANCO" },
                new() { IdNome = "PRETO", Nome = "PRETO" },
                new() { IdNome = "INDÍGENA", Nome = "INDÍGENA" },
                new() { IdNome = "AMARELO", Nome = "AMARELO" }
            };

            var etnias = new SelectList(list, "IdNome", "Nome");

            return View(new AlunoModel()
            {
                ListEstados = estados,
                ListDeficiencias = deficiencias,
                ListModalidades = modalidades,
                ListEtnias = etnias,
            });
        }

        /// <summary>
        /// Tela para alteração de aluno
        /// </summary>
        /// <param name="id">identificador do aluno</param>
        /// <param name="crud">paramentro que indica o tipo de ação realizado</param>
        /// <param name="notify">parametro que indica o tipo de notificação realizada</param>
        /// <param name="message">mensagem apresentada nas notificações e alertas gerados na tela</param>
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Alterar)]
        public ActionResult Edit(int id, int? crud, int? notify, string message = null)
        {
            try
            {
                SetNotifyMessage(notify, message);
                SetCrudMessage(crud);
                var aluno = ApiClientFactory.Instance.GetAlunoById(id);
                var estados = new SelectList(ApiClientFactory.Instance.GetEstadosAll(), "Sigla", "Nome", aluno.Estado);
                var municipios = new SelectList(ApiClientFactory.Instance.GetMunicipiosByUf(aluno.Estado), "Id", "Nome", aluno.MunicipioId);
                var localidades = new SelectList(ApiClientFactory.Instance.GetLocalidadeByMunicipio(aluno.MunicipioId), "Id", "Nome", aluno.LocalidadeId);
                var profissionais = new SelectList(ApiClientFactory.Instance.GetProfissionaisByLocalidade(Convert.ToInt32(aluno.LocalidadeId)), "Id", "Nome", aluno.ProfissionalId);

                List<SelectListDto> list = new List<SelectListDto>
                {
                    new() { IdNome = "PARDO", Nome = "PARDO" },
                    new() { IdNome = "BRANCO", Nome = "BRANCO" },
                    new() { IdNome = "PRETO", Nome = "PRETO" },
                    new() { IdNome = "INDÍGENA", Nome = "INDÍGENA" },
                    new() { IdNome = "AMARELO", Nome = "AMARELO" }
                };

                var etnias = new SelectList(list, "IdNome", "Nome", aluno.Etnia);
                var modalidades = ApiClientFactory.Instance.GetModalidadeAll();
                var dependencia = aluno.DependenciaId == null
                    ? null
                    : ApiClientFactory.Instance.GetDependenciaById((int)aluno.DependenciaId);
                var matricula = aluno.MatriculaId == null
                    ? null
                    : ApiClientFactory.Instance.GetMatriculaById((int)aluno.MatriculaId);

                return View(new AlunoModel()
                {
                    ListEstados = estados,
                    Modalidades = aluno.Modalidades,
                    Aluno = aluno,
                    Dependecia = dependencia!,
                    Matricula = matricula!,
                    ListMunicipios = municipios,
                    ListLocalidades = localidades,
                    ListProfissionais = profissionais,
                    ListEtnias = etnias,

                });

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        /// <summary>
        /// Ação de inclusao do aluno
        /// </summary>
        /// <param name="collection">coleção de dados para inclusao de aluno</param>
        /// <returns>retorna mensagem de inclusao através do parametro crud</returns>
        [HttpPost]
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Incluir)]
        public async Task<ActionResult> CreateDados(IFormCollection collection)
        {
            try
            {
                string filePath = null;

                var status = collection["status"].ToString();
                var habilitado = collection["habilitado"].ToString();

                var command = new AlunoModel.CreateUpdateDadosAlunoCommand()
                {
                    Etnia = collection["ddlEtnia"] == "" ? null : collection["ddlEtnia"].ToString(),
                    MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    ProfissionalId = collection["ddlProfissionalAluno"] == "" ? null : Convert.ToInt32(collection["ddlProfissionalAluno"].ToString()),
                    LocalidadeId = collection["ddlLocalidade"] == "" ? null : Convert.ToInt32(collection["ddlLocalidade"].ToString()),
                    Nome = collection["nome"] == "" ? null : collection["nome"].ToString(),
                    DtNascimento = collection["DtNascimento"] == "" ? null : collection["DtNascimento"].ToString(),
                    Email = collection["email"] == "" ? null : collection["email"].ToString(),
                    Sexo = collection["ddlSexo"] == "" ? null : collection["ddlSexo"].ToString(),
                    NomeMae = collection["nomeMae"] == "" ? null : collection["nomeMae"].ToString(),
                    NomePai = collection["nomePai"] == "" ? null : collection["nomePai"].ToString(),
                    Telefone = collection["numTelefone"] == "" ? null : collection["numTelefone"].ToString(),
                    Cep = collection["cep"] == "" ? null : collection["cep"].ToString(),
                    Celular = collection["numCelular"] == "" ? null : collection["numCelular"].ToString(),
                    Cpf = collection["cpf"] == "" ? null : collection["cpf"].ToString(),
                    Endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                    Numero = collection["numero"] == "" ? null : collection["numero"].ToString(),
                    Bairro = collection["bairro"] == "" ? null : collection["bairro"].ToString(),
                    DeficienciasIds = collection["arrDeficiencias"] == "" ? null : collection["arrDeficiencias"].ToString(),
                    Habilitado = habilitado != "",
                    Status = status != "",
                    NomeFoto = filePath

                };

                foreach (var file in collection.Files)
                {
                    if (file.Length <= 0) continue;

                    command.NomeFoto = Path.GetFileName(collection.Files[0].FileName);

                    using (var ms = new MemoryStream())
                    {
                        file.CopyToAsync(ms);
                        var byteIMage = ms.ToArray();
                        command.ByteImage = byteIMage;
                    }
                }

                var alunoId = await ApiClientFactory.Instance.CreateDados(command);

                var updateCommand = command;

                updateCommand.Id = (int)alunoId;
                command.QrCode = GeraQrCode(alunoId);

                await ApiClientFactory.Instance.UpdateDados((int)alunoId, updateCommand);

                return RedirectToAction(nameof(Edit), new { id = alunoId, crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Create), new { notify = (int)EnumNotify.Error, message = e.Message });
            }
        }

        /// <summary>
        /// Ação de alteração do aluno
        /// </summary>
        /// <param name="id">identificador do aluno</param>
        /// <param name="collection">coleção de dados para alteração de aluno</param>
        /// <returns>retorna mensagem de alteração através do parametro crud</returns>
        [HttpPost]
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Alterar)]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                string filePath = null;

                var status = collection["status"].ToString();
                var habilitado = collection["habilitado"].ToString();


                var command = new AlunoModel.CreateUpdateDadosAlunoCommand
                {
                    Id = Convert.ToInt32(id),
                    Etnia = collection["ddlEtnia"] == "" ? null : collection["ddlEtnia"].ToString(),
                    MunicipioId = collection["ddlMunicipio"] == "" ? null : Convert.ToInt32(collection["ddlMunicipio"].ToString()),
                    ProfissionalId = collection["ddlProfissionalAluno"] == "" ? null : Convert.ToInt32(collection["ddlProfissionalAluno"].ToString()),
                    LocalidadeId = collection["ddlLocalidade"] == "" ? null : Convert.ToInt32(collection["ddlLocalidade"].ToString()),
                    Nome = collection["nome"] == "" ? null : collection["nome"].ToString(),
                    DtNascimento = collection["DtNascimento"] == "" ? null : collection["DtNascimento"].ToString(),
                    Email = collection["email"] == "" ? null : collection["email"].ToString(),
                    Sexo = collection["ddlSexo"] == "" ? null : collection["ddlSexo"].ToString(),
                    NomeMae = collection["nomeMae"] == "" ? null : collection["nomeMae"].ToString(),
                    NomePai = collection["nomePai"] == "" ? null : collection["nomePai"].ToString(),
                    Telefone = collection["numTelefone"] == "" ? null : collection["numTelefone"].ToString(),
                    Cep = collection["cep"] == "" ? null : collection["cep"].ToString(),
                    Celular = collection["numCelular"] == "" ? null : collection["numCelular"].ToString(),
                    Cpf = collection["cpf"] == "" ? null : collection["cpf"].ToString(),
                    Endereco = collection["endereco"] == "" ? null : collection["endereco"].ToString(),
                    Numero = collection["numero"] == "" ? null : collection["numero"].ToString(),
                    Bairro = collection["bairro"] == "" ? null : collection["bairro"].ToString(),
                    DeficienciasIds = collection["arrDeficiencias"] == "" ? null : collection["arrDeficiencias"].ToString(),
                    Habilitado = habilitado != "",
                    Status = status != ""

                };

                foreach (var file in collection.Files)
                {
                    if (file.Length <= 0) continue;

                    command.NomeFoto = Path.GetFileName(collection.Files[0].FileName);

                    using (var ms = new MemoryStream())
                    {
                        file.CopyToAsync(ms);
                        var byteIMage = ms.ToArray();
                        command.ByteImage = byteIMage;
                    }
                }

                command.QrCode = GeraQrCode(id);

                await ApiClientFactory.Instance.UpdateDados(id, command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = EnumNotify.Error, mesage = e.Message });
            }
        }

        /// <summary>
        /// Ação de upload de foto do aluno
        /// </summary>
        /// <param name="collection">arquivo de upload realizado</param>
        /// <returns>retorna mensagem de upload realizado através do parametro notfy e message</returns>
        [HttpPost]
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Upload)]
        public async Task<ActionResult> Upload(IFormCollection collection)
        {
            try
            {
                string filePath = null;

                var aluno = ApiClientFactory.Instance.GetAlunoById(Convert.ToInt32(collection["alunoId"]));

                var command = new AlunoModel.CreateUpdateDadosAlunoCommand
                {
                    Id = Convert.ToInt32(collection["alunoId"])
                };

                foreach (var file in collection.Files)
                {
                    if (file.Length <= 0) continue;

                    command.NomeFoto = Path.GetFileName(collection.Files[0].FileName);

                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var byteIMage = ms.ToArray();
                    command.ByteImage = byteIMage;
                }

                await ApiClientFactory.Instance.UpdateAlunoFoto(command.Id, command);

                return RedirectToAction(nameof(Index), new { notify = EnumNotify.Success, mesage = "Upload realizado com sucesso." });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = EnumNotify.Error, mesage = e.Message });
            }
        }

        /// <summary>
        /// Ação de alteração da deficiencia do aluno
        /// </summary>
        /// <param name="id">identificador do aluno</param>
        /// <param name="collection">coloeção de deficiencias selecionadas</param>
        /// <returns>retorna mensagem de alteração através do parametro crud</returns>
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Alterar)]
        public async Task<IActionResult> EditDeficiencia(int id, IFormCollection collection)
        {
            try
            {
                var status = collection["status"].ToString();

                var command = new DeficienciaModel.CreateUpdateDeficienciaCommand
                {
                    Nome = collection["deficiencia"].ToString(),
                    Status = status != ""
                };

                await ApiClientFactory.Instance.UpdateDeficiencia(id, command);

                return RedirectToAction(nameof(EditDeficiencia), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        /// <summary>
        /// Ação de alteração da dependencia do aluno
        /// </summary>
        /// <param name="id">identificador do aluno</param>
        /// <param name="collection">coloeção de dependencias selecionadas</param>
        /// <returns>retorna mensagem de alteração através do parametro crud</returns>
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Alterar)]
        public async Task<IActionResult> EditDependencia(int id, IFormCollection collection)
        {
            try
            {
                var command = new DependenciaModel.CreateUpdateDependenciaCommand
                {
                    Doencas = collection["Doenca"].ToString(),
                    Nacionalidade = collection["nacionalidade"].ToString(),
                    Naturalidade = collection["naturalidade"].ToString(),
                    NomeEscola = collection["escola"].ToString(),
                    TipoEscola = collection["ddlTipoEscola"].ToString(),
                    TipoEscolaridade = collection["ddlTipoEscolaridade"].ToString(),
                    Turno = collection["ddlTurno"].ToString(),
                    Serie = collection["serie"].ToString(),
                    Ano = collection["ano"].ToString(),
                    Turma = collection["turma"].ToString(),
                    TermoCompromisso = collection["rdbTermo"] == "true",
                    AutorizacaoUsoImagemAudio = collection["rdbautorizacaoimagem"] == "true",
                    AutorizacaoUsoIndicadores = collection["rdbautorizacaoindicadores"] == "true",
                    AutorizacaoSaida = collection["rdbAutorizacao"] == "true",
                    AlunoId = 2259

                };

                await ApiClientFactory.Instance.UpdateDependencia(id, command);

                return RedirectToAction(nameof(EditDependencia), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Busca de alunos por localidade
        /// </summary>
        /// <param name="id">identificador da localidade</param>
        /// <returns>retorna a lista de alunos</returns>
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Consultar)]
        public Task<JsonResult> GetAlunosByLocalidade(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Localidade não informada.");
                var resultLocal = ApiClientFactory.Instance.GetNomeAlunosAll(id);

                return Task.FromResult(Json(new SelectList(resultLocal, "Id", "Nome")));

            }
            catch (Exception ex)
            {
                return Task.FromResult(Json(ex));
            }
        }

        /// <summary>
        /// Busca de aluno por id
        /// </summary>
        /// <param name="id">identificador do aluno</param>
        /// <returns>retorna o aluno</returns>
        [ClaimsAuthorize(ClaimType.Aluno, Claim.Consultar)]
        public async Task<JsonResult> GetAlunoById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Id do Aluno não informado.");
                var result = ApiClientFactory.Instance.GetAlunoById(Convert.ToInt32(id));

                if (result.ByteImage != null)
                {
                    result.Image = GetImage(Convert.ToBase64String(result.ByteImage!));
                }

                if (result.QrCode == null)
                {
                    result.QrCode = GeraQrCode(result.Id);
                    await ApiClientFactory.Instance.UpdateDados(result.Id, new AlunoModel.CreateUpdateDadosAlunoCommand()
                    {
                        Id = result.Id,
                        QrCode = result.QrCode
                    });
                }
                return Json(result);

            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        #endregion

        #region Private Methods
        private static byte[]? GeraQrCode(long alunoId)
        {
            AlunoModel.CreateUpdateDadosAlunoCommand command;
            var text = $"http://front.hml.dnadobrasil.org.br/Identity/Account/ControlePresenca?alunoId={alunoId}";

            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);

            return BitmapToBytes(QrBitmap);
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }

            return bytes;
        }
        #endregion




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
                    Abdominal = Convert.ToBoolean(collection["abdominal"])
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
                Abdominal = Convert.ToBoolean(collection["abdominal"])
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
                    Resposta = Convert.ToString(collection["profissionalId"]),
                    AlunoId = 0
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
                Resposta = Convert.ToString(collection["profissionalId"]),
                AlunoId = 0
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
                    //ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                    //Altura = Convert.ToInt32(collection["altura"]),
                    //Massa = Convert.ToInt32(collection["massa"]),
                    //Envergadura = Convert.ToInt32(collection["envergadura"])


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
                //Altura = Convert.ToInt32(collection["altura"]),
                //Massa = Convert.ToInt32(collection["massa"]),
                //Envergadura = Convert.ToInt32(collection["envergadura"])

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
                    Descricao = collection["descricaoVoucher"].ToString(),
                    Turma = collection["turmaVoucher"].ToString(),
                    Serie = collection["serieVoucher"].ToString(),
                    AlunoId = 2260
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
                Descricao = collection["descricaoVoucher"].ToString(),
                Turma = collection["turmaVoucher"].ToString(),
                Serie = collection["serieVoucher"].ToString(),
                AlunoId = 2260

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
                    DtVencimentoParq = collection["dtVencimentoParq"].ToString(),
                    DtVencimentoAtestadoMedico = collection["dtVencimentoAtestado"].ToString(),
                    NomeResponsavel1 = collection["nomeResponsavel1"].ToString(),
                    ParentescoResponsavel1 = collection["parentesco1"].ToString(),
                    CpfResponsavel1 = collection["cpf1"].ToString(),
                    NomeResponsavel2 = collection["nomeResponsavel2"].ToString(),
                    ParentescoResponsavel2 = collection["parentesco2"].ToString(),
                    CpfResponsavel2 = collection["cpf2"].ToString(),
                    NomeResponsavel3 = collection["nomeResponsavel3"].ToString(),
                    ParentescoResponsavel3 = collection["parentesco3"].ToString(),
                    CpfResponsavel3 = collection["cpf3"].ToString(),
                    AlunoId = 2259

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
        public async Task<ActionResult> EditMatricula(int id, IFormCollection collection)
        {
            try
            {
                var command = new MatriculaModel.CreateUpdateMatriculaCommand
                {
                    Id = Convert.ToInt32(id),
                    DtVencimentoParq = collection["dtVencimentoParq"].ToString(),
                    DtVencimentoAtestadoMedico = collection["dtVencimentoAtestado"].ToString(),
                    NomeResponsavel1 = collection["nomeResponsavel1"].ToString(),
                    ParentescoResponsavel1 = collection["parentesco1"].ToString(),
                    CpfResponsavel1 = collection["cpf1"].ToString(),
                    NomeResponsavel2 = collection["nomeResponsavel2"].ToString(),
                    ParentescoResponsavel2 = collection["parentesco2"].ToString(),
                    CpfResponsavel2 = collection["cpf2"].ToString(),
                    NomeResponsavel3 = collection["nomeResponsavel3"].ToString(),
                    ParentescoResponsavel3 = collection["parentesco3"].ToString(),
                    CpfResponsavel3 = collection["cpf3"].ToString(),
                    AlunoId = 2259


                };

                await ApiClientFactory.Instance.UpdateMatricula(id, command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        

        


        public async Task<ActionResult> CreateDependencia(IFormCollection collection)
        {

            try
            {
                var command = new DependenciaModel.CreateUpdateDependenciaCommand
                {
                    Doencas = collection["Doenca"].ToString(),
                    Nacionalidade = collection["nacionalidade"].ToString(),
                    Naturalidade = collection["naturalidade"].ToString(),
                    NomeEscola = collection["escola"].ToString(),
                    TipoEscola = collection["ddlTipoEscola"].ToString(),
                    TipoEscolaridade = collection["ddlTipoEscolaridade"].ToString(),
                    Turno = collection["ddlTurno"].ToString(),
                    Serie = collection["serie"].ToString(),
                    Ano = collection["ano"].ToString(),
                    Turma = collection["turma"].ToString(),
                    TermoCompromisso = collection["rdbTermo"] == "true",
                    AutorizacaoUsoImagemAudio = collection["rdbautorizacaoimagem"] == "true",
                    AutorizacaoUsoIndicadores = collection["rdbautorizacaoindicadores"] == "true",
                    AutorizacaoSaida = collection["rdbAutorizacao"] == "true",
                    AlunoId = 2259

                };

                await ApiClientFactory.Instance.CreateDependencia(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Aluno", "Alterar")]
        [HttpPost]
        public async Task<ActionResult> CreateModalidadesAluno(IFormCollection collection)
        {
            try
            {
                var command = new ModalidadeModel.CreateUpdateModalidadeCommand
                {
                    ModalidadesIds = collection["arrModalidadeAlunos"] == "" ? null : collection["arrModalidadeAlunos"].ToString()
                };

                await ApiClientFactory.Instance.CreateModalidade(command);

                return RedirectToAction(nameof(Create), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }
        public async Task<ActionResult> EditModalidadesAluno(int id, IFormCollection collection)
        {
            try
            {
                var command = new ModalidadeModel.CreateUpdateModalidadeCommand
                {
                    Id = Convert.ToInt32(id),
                    ModalidadesIds = collection["arrModalidadeAlunos"] == "" ? null : collection["arrModalidadeAlunos"].ToString()
                };

                await ApiClientFactory.Instance.UpdateModalidade(id, command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return RedirectToAction(nameof(Index), new { notify = (int)EnumNotify.Error, message = e.Message });

            }
        }

        //[ClaimsAuthorize("Aluno", "Alterar")]
        [HttpPost]
        public async Task<ActionResult> CreateDeficiencia(IFormCollection collection)
        {
            try
            {
                var status = collection["status"].ToString();

                var command = new DeficienciaModel.CreateUpdateDeficienciaCommand
                {
                    Nome = collection["deficiencia"].ToString(),
                    Status = status != ""
                };

                await ApiClientFactory.Instance.CreateDeficiencia(command);

                return RedirectToAction(nameof(Create), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }




    }
}
