using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceAlunos = "Alunos";

        #region Main Methods

        public Task<long> CreateDados(AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}"));
            return Post(requestUrl, command);
        }

        public Task<bool> UpdateDados(int id, AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteDados(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods


        public List<AlunoDto> GetAlunosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}"));
            return Get<List<AlunoDto>>(requestUrl);
        }
        public AlunoDto GetAlunoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/Aluno/{id}"));
            return Get<AlunoDto>(requestUrl);
        }
        public List<AlunoDto> GetAlunosByLocalidade(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/Localidade/{id}"));
            return Get<List<AlunoDto>>(requestUrl);
        }
        public List<SelectListDto> GetNomeAlunosAll(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/NomeAlunos/{id}"));
            return Get<List<SelectListDto>>(requestUrl);
        }
        public Task<AlunosFilterDto?> GetAlunosByFilter(AlunosFilterDto searchFilter)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAlunos}/Filter"));
            return GetFiltro(requestUrl, searchFilter);
        }

        #endregion

    }
}