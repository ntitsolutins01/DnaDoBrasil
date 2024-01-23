using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceDados = "Alunos";

        #region Main Methods

        public Task<long> CreateDados(AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}"));
            return Post(requestUrl, command);
        }

        public Task<bool> UpdateDados(int id, AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteDados(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods


        public List<AlunoDto> GetAlunosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}"));
            return Get<List<AlunoDto>>(requestUrl);
        }
        public AlunoDto GetAlunoById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}/{id}"));
            return Get<AlunoDto>(requestUrl);
        }
        public List<AlunoDto> GetAlunosByLocalidade(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}/Localidade/{id}"));
            return Get<List<AlunoDto>>(requestUrl);
        }


        #endregion
    
    }
}