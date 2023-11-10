using Microsoft.AspNetCore.Mvc;
using WebApp.Enumerators;
using WebApp.Factory;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class AlunoController : BaseController
    {
        public ActionResult Index(int? crud, int? notify, string message = null)
        {
            SetNotifyMessage(notify, message);
            SetCrudMessage(crud);

            //var deficiencias = ApiClientFactory.Instance.GetDeficienciasAll();

            //return View(new AlunoModel { Deficiencias = deficiencias });
            return View();
        }
        public IActionResult Create()
		{
			return View();
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
        public async Task<ActionResult> EditTalentoEsportivo(string id, IFormCollection collection)
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

            await ApiClientFactory.Instance.UpdateTalentoEsportivo(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
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
        public async Task<ActionResult> EditVocacional(string id, IFormCollection collection)
        {
            var command = new VocacionalModel.CreateUpdateVocacionalCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["profissionalId"])

            };

            await ApiClientFactory.Instance.UpdateVocacional(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
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
        public async Task<ActionResult> EditQualidadeVida(string id, IFormCollection collection)
        {
            var command = new QualidadeVidaModel.CreateUpdateQualidadeVidaCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["profissionalId"])

            };

            await ApiClientFactory.Instance.UpdateQualidadeVida(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
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
        public async Task<ActionResult> EditSaude(string id, IFormCollection collection)
        {
            var command = new SaudeModel.CreateUpdateSaudeCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                Altura = Convert.ToInt32(collection["altura"]),
                Massa = Convert.ToInt32(collection["massa"]),
                Envergadura = Convert.ToInt32(collection["envergadura"])

            };

            await ApiClientFactory.Instance.UpdateSaude(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
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
        public async Task<ActionResult> EditConsumoAlimentar(string id, IFormCollection collection)
        {
            var command = new ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["resposta"])

            };

            await ApiClientFactory.Instance.UpdateConsumoAlimentar(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
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
        public async Task<ActionResult> EditSaudeBucal(string id, IFormCollection collection)
        {
            var command = new SaudeBucalModel.CreateUpdateSaudeBucalCommand
            {
                Id = Convert.ToInt32(id),
                ProfissionalId = Convert.ToInt32(collection["profissionalId"]),
                QuestionarioId = Convert.ToInt32(collection["questionarioId"]),
                Resposta = Convert.ToString(collection["resposta"])

            };

            await ApiClientFactory.Instance.UpdateSaudeBucal(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
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
                var command = new DadosModel.CreateUpdateDadosCommand
                {
                    AspNetUserId = Convert.ToInt32(collection["aspnetUserId"]),
                    MunicipioId = Convert.ToInt32(collection["municipioId"]),
                    Nome = Convert.ToString(collection["nome"]),
                    Email = Convert.ToString(collection["email"]),
                    Sexo = Convert.ToString(collection["sexo"]),
                    DtNascimento = Convert.ToDateTime(collection["dtNascimento"].ToString()),
                    NomeMae = Convert.ToString(collection["nomeMae"]),
                    NomePai = Convert.ToString(collection["nomePai"]),
                    Cpf = Convert.ToString(collection["cpf"]),
                    Telefone = Convert.ToString(collection["telefone"]),
                    Celular = Convert.ToString(collection["celular"]),
                    Cep = Convert.ToString(collection["cep"]),
                    Endereco = Convert.ToString(collection["endereco"]),
                    Numero = Convert.ToString(collection["numero"]),
                    Bairro = Convert.ToString(collection["bairro"]),
                    RedeSocial = Convert.ToString(collection["redeSocial"]),
                    Url = Convert.ToString(collection["url"]),
                    Status = Convert.ToBoolean(collection["status"]),
                    Habilitado = Convert.ToBoolean(collection["status"]),
                    DeficienciasId = Convert.ToInt32(collection["deficienciasId"]),
                    AmbientesId = Convert.ToInt32(collection["ambientesId"]),
                    ParceiroId = Convert.ToInt32(collection["parceiroId"]),
                    Etnia = Convert.ToInt32(collection["etnia"]),
                    ContratosId = Convert.ToInt32(collection["contratosId"]),
                    MatriculaId = Convert.ToInt32(collection["matriculaId"]),
                    VoucherId = Convert.ToInt32(collection["voucherId"]),
                    DependenciaId = Convert.ToInt32(collection["dependenciaId"]),
                    LaudosId = Convert.ToInt32(collection["laudosId"])

                };

                await ApiClientFactory.Instance.CreateDados(command);

                return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Created });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //[ClaimsAuthorize("Usuario", "Alterar")]
        public async Task<ActionResult> EditDados(string id, IFormCollection collection)
        {
            var command = new DadosModel.CreateUpdateDadosCommand
            {
                Id = Convert.ToInt32(id),
                AspNetUserId = Convert.ToInt32(collection["aspnetUserId"]),
                MunicipioId = Convert.ToInt32(collection["municipioId"]),
                Nome = Convert.ToString(collection["nome"]),
                Email = Convert.ToString(collection["email"]),
                Sexo = Convert.ToString(collection["sexo"]),
                DtNascimento = Convert.ToDateTime(collection["dtNascimento"].ToString()),
                NomeMae = Convert.ToString(collection["nomeMae"]),
                NomePai = Convert.ToString(collection["nomePai"]),
                Cpf = Convert.ToString(collection["cpf"]),
                Telefone = Convert.ToString(collection["telefone"]),
                Celular = Convert.ToString(collection["celular"]),
                Cep = Convert.ToString(collection["cep"]),
                Endereco = Convert.ToString(collection["endereco"]),
                Numero = Convert.ToString(collection["numero"]),
                Bairro = Convert.ToString(collection["bairro"]),
                RedeSocial = Convert.ToString(collection["redeSocial"]),
                Url = Convert.ToString(collection["url"]),
                Status = Convert.ToBoolean(collection["status"]),
                Habilitado = Convert.ToBoolean(collection["status"]),
                DeficienciasId = Convert.ToInt32(collection["deficienciasId"]),
                AmbientesId = Convert.ToInt32(collection["ambientesId"]),
                ParceiroId = Convert.ToInt32(collection["parceiroId"]),
                Etnia = Convert.ToInt32(collection["etnia"]),
                ContratosId = Convert.ToInt32(collection["contratosId"]),
                MatriculaId = Convert.ToInt32(collection["matriculaId"]),
                VoucherId = Convert.ToInt32(collection["voucherId"]),
                DependenciaId = Convert.ToInt32(collection["dependenciaId"]),
                LaudosId = Convert.ToInt32(collection["laudosId"])

            };

            await ApiClientFactory.Instance.UpdateDados(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
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
        public async Task<ActionResult> EditVoucher(string id, IFormCollection collection)
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

            await ApiClientFactory.Instance.UpdateVoucher(command);

            return RedirectToAction(nameof(Index), new { crud = (int)EnumCrud.Updated });
        }



    }
}
